// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class TokenFilterTests
    {
        private CompilerDefines _defines;
        private MemoryFileLoader _fileLoader;

        [SetUp]
        public void SetUp()
        {
            _fileLoader = new MemoryFileLoader();
            _defines = CompilerDefines.CreateEmpty();
            _defines.DefineSymbol("TRUE");
            _defines.UndefineSymbol("FALSE");
            _defines.DefineDirectiveAsTrue("IF True");
            _defines.DefineDirectiveAsFalse("IF False");
        }

        private Constraint LexesAndFiltersAs(params string[] expected)
        {
            return new LexesAsConstraint(expected, delegate(IEnumerable<Token> tokens)
            {
                TokenFilter filter = new TokenFilter(tokens, _defines, _fileLoader);
                return filter.Tokens;
            });
        }

        [Test]
        public void PassThrough()
        {
            Assert.That("Foo", LexesAndFiltersAs(
                "Identifier |Foo|"));
        }
        [Test]
        public void SingleLineCommentIsIgnored()
        {
            Assert.That("// Foo", LexesAndFiltersAs());
        }
        [Test]
        public void CurlyBraceCommentIsIgnored()
        {
            Assert.That("{ Foo }", LexesAndFiltersAs());
        }
        [Test]
        public void ParenStarCommentIsIgnored()
        {
            Assert.That("(* Foo *)", LexesAndFiltersAs());
        }
        [Test]
        public void ParserUsesFilter()
        {
            Parser parser = ParserTestCase.CreateParser("// Foo");
            Assert.That(parser.AtEof, Is.True);
        }
        [Test]
        public void SingleLetterCompilerDirectivesAreIgnored()
        {
            Assert.That("{$R+}", LexesAndFiltersAs());
            Assert.That("{$A8}", LexesAndFiltersAs());
        }
        [Test]
        public void CPlusPlusBuilderCompilerDirectivesAreIgnored()
        {
            Assert.That("{$EXTERNALSYM Foo}", LexesAndFiltersAs());
            Assert.That("{$HPPEMIT '#pragma Foo'}", LexesAndFiltersAs());
            Assert.That("{$NODEFINE Foo}", LexesAndFiltersAs());
            Assert.That("{$NOINCLUDE Foo}", LexesAndFiltersAs());
        }
        [Test]
        public void IfDefTrue()
        {
            Assert.That("0{$IFDEF TRUE}1{$ENDIF}2", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |2|"));
        }
        [Test]
        public void IfDefFalse()
        {
            Assert.That("0{$IFDEF FALSE}1{$ENDIF}2", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|"));
        }
        [Test]
        public void IfDefTrueTrue()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF TRUE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |2|",
                "Number |3|",
                "Number |4|"));
        }
        [Test]
        public void IfDefTrueFalse()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF FALSE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |3|",
                "Number |4|"));
        }
        [Test]
        public void IfDefFalseTrue()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF TRUE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |4|"));
        }
        [Test]
        public void IfDefFalseFalse()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF FALSE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |4|"));
        }
        [Test]
        public void IfDefFalseUnknown()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF UNKNOWN}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |4|"));
        }
        [Test]
        public void IfEnd()
        {
            Assert.That("0{$IF False}1{$IFEND}2", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|"));
        }
        [Test]
        public void IfDefTrueWithElse()
        {
            Assert.That("0{$IFDEF TRUE}1{$ELSE}2{$ENDIF}3", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |3|"));
        }
        [Test]
        public void IfDefFalseWithElse()
        {
            Assert.That("0{$IFDEF FALSE}1{$ELSE}2{$ENDIF}3", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|",
                "Number |3|"));
        }
        [Test]
        public void IfDefTrueTrueWithElses()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF TRUE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |2|",
                "Number |4|",
                "Number |6|"));
        }
        [Test]
        public void IfDefTrueFalseWithElses()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF FALSE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |3|",
                "Number |4|",
                "Number |6|"));
        }
        [Test]
        public void IfDefFalseTrueWithElses()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF TRUE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |5|",
                "Number |6|"));
        }
        [Test]
        public void IfDefFalseFalseWithElses()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF FALSE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |5|",
                "Number |6|"));
        }
        [Test]
        public void IfTrueElseIfTrue()
        {
            Assert.That("0{$IF True}1{$ELSEIF True}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |4|"));
        }
        [Test]
        public void IfTrueElseIfFalse()
        {
            Assert.That("0{$IF True}1{$ELSEIF False}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |4|"));
        }
        [Test]
        public void IfFalseElseIfTrue()
        {
            Assert.That("0{$IF False}1{$ELSEIF True}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|",
                "Number |4|"));
        }
        [Test]
        public void IfFalseElseIfFalse()
        {
            Assert.That("0{$IF False}1{$ELSEIF False}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |3|",
                "Number |4|"));
        }
        [Test]
        public void IPlusIsNotTreatedAsInclude()
        {
            Assert.That("{$I+}", LexesAndFiltersAs());
        }
        [Test]
        public void IMinusIsNotTreatedAsInclude()
        {
            Assert.That("{$I-}", LexesAndFiltersAs());
        }
        [Test]
        public void Include()
        {
            _fileLoader.Files["bar.inc"] = "Bar";
            Assert.That("Foo {$INCLUDE bar.inc} Baz", LexesAndFiltersAs(
                "Identifier |Foo|",
                "Identifier |Bar|",
                "Identifier |Baz|"));
        }
        [Test]
        public void Define()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That("{$DEFINE FOO} {$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs("Identifier |Foo|"));
        }
        [Test]
        public void Undefine()
        {
            _defines.DefineSymbol("FOO");
            Assert.That("{$UNDEF FOO} {$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs(""));
        }
        [Test]
        public void DefineScopeDoesNotExtendToOtherFiles()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That("{$DEFINE FOO}", LexesAndFiltersAs(""));
            Assert.That("{$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs(""));
        }
        [Test]
        public void DefineScopeDoesBubbleUpFromIncludeFiles()
        {
            _defines.UndefineSymbol("FOO");
            _fileLoader.Files["defines.inc"] = "{$DEFINE FOO}";
            Assert.That("{$I defines.inc} {$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs(
                "Identifier |Foo|"));
        }
        [Test]
        public void DefineIgnoredInFalseIf()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That("{$IF False}{$DEFINE FOO}{$IFEND} {$IFDEF FOO}Foo{$ENDIF}", LexesAndFiltersAs(""));
        }
        [Test]
        public void UndefineIgnoredInFalseIf()
        {
            _defines.DefineSymbol("FOO");
            Assert.That("{$IF False}{$UNDEF FOO}{$IFEND} {$IFDEF FOO}Foo{$ENDIF}", LexesAndFiltersAs(
                "Identifier |Foo|"));
        }
        [Test, ExpectedException(typeof(LexException))]
        public void ThrowOnUnrecognizedDirective()
        {
            Lexer lexer = new Lexer("{$FOO}", "");
            TokenFilter filter = new TokenFilter(lexer.Tokens, _defines, _fileLoader);
            new List<Token>(filter.Tokens);
        }
        [Test]
        public void UnrecognizedIsIgnoredInFalseIf()
        {
            Assert.That("{$IF False}{$FOO}{$IFEND}", LexesAndFiltersAs(""));
        }
    }
}
