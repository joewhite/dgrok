// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnitLite.Constraints;
using NUnitLite.Framework;

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

        public void TestPassThrough()
        {
            Assert.That("Foo", LexesAndFiltersAs(
                "Identifier |Foo|"));
        }
        public void TestSingleLineCommentIsIgnored()
        {
            Assert.That("// Foo", LexesAndFiltersAs());
        }
        public void TestCurlyBraceCommentIsIgnored()
        {
            Assert.That("{ Foo }", LexesAndFiltersAs());
        }
        public void TestParenStarCommentIsIgnored()
        {
            Assert.That("(* Foo *)", LexesAndFiltersAs());
        }
        public void TestParserUsesFilter()
        {
            Parser parser = ParserTestCase.CreateParser("// Foo");
            Assert.That(parser.AtEof, Is.True);
        }
        public void TestSingleLetterCompilerDirectivesAreIgnored()
        {
            Assert.That("{$R+}", LexesAndFiltersAs());
            Assert.That("{$A8}", LexesAndFiltersAs());
        }
        public void TestCPlusPlusBuilderCompilerDirectivesAreIgnored()
        {
            Assert.That("{$EXTERNALSYM Foo}", LexesAndFiltersAs());
            Assert.That("{$HPPEMIT '#pragma Foo'}", LexesAndFiltersAs());
            Assert.That("{$NODEFINE Foo}", LexesAndFiltersAs());
            Assert.That("{$NOINCLUDE Foo}", LexesAndFiltersAs());
        }
        public void TestIfDefTrue()
        {
            Assert.That("0{$IFDEF TRUE}1{$ENDIF}2", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |2|"));
        }
        public void TestIfDefFalse()
        {
            Assert.That("0{$IFDEF FALSE}1{$ENDIF}2", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|"));
        }
        public void TestIfDefTrueTrue()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF TRUE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |2|",
                "Number |3|",
                "Number |4|"));
        }
        public void TestIfDefTrueFalse()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF FALSE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |3|",
                "Number |4|"));
        }
        public void TestIfDefFalseTrue()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF TRUE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |4|"));
        }
        public void TestIfDefFalseFalse()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF FALSE}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |4|"));
        }
        public void TestIfDefFalseUnknown()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF UNKNOWN}2{$ENDIF}3{$ENDIF}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |4|"));
        }
        public void TestIfEnd()
        {
            Assert.That("0{$IF False}1{$IFEND}2", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|"));
        }
        public void TestIfDefTrueWithElse()
        {
            Assert.That("0{$IFDEF TRUE}1{$ELSE}2{$ENDIF}3", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |3|"));
        }
        public void TestIfDefFalseWithElse()
        {
            Assert.That("0{$IFDEF FALSE}1{$ELSE}2{$ENDIF}3", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|",
                "Number |3|"));
        }
        public void TestIfDefTrueTrueWithElses()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF TRUE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |2|",
                "Number |4|",
                "Number |6|"));
        }
        public void TestIfDefTrueFalseWithElses()
        {
            Assert.That("0{$IFDEF TRUE}1{$IFDEF FALSE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |3|",
                "Number |4|",
                "Number |6|"));
        }
        public void TestIfDefFalseTrueWithElses()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF TRUE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |5|",
                "Number |6|"));
        }
        public void TestIfDefFalseFalseWithElses()
        {
            Assert.That("0{$IFDEF FALSE}1{$IFDEF FALSE}2{$ELSE}3{$ENDIF}4{$ELSE}5{$ENDIF}6", LexesAndFiltersAs(
                "Number |0|",
                "Number |5|",
                "Number |6|"));
        }
        public void TestIfTrueElseIfTrue()
        {
            Assert.That("0{$IF True}1{$ELSEIF True}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |4|"));
        }
        public void TestIfTrueElseIfFalse()
        {
            Assert.That("0{$IF True}1{$ELSEIF False}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |1|",
                "Number |4|"));
        }
        public void TestIfFalseElseIfTrue()
        {
            Assert.That("0{$IF False}1{$ELSEIF True}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |2|",
                "Number |4|"));
        }
        public void TestIfFalseElseIfFalse()
        {
            Assert.That("0{$IF False}1{$ELSEIF False}2{$ELSE}3{$IFEND}4", LexesAndFiltersAs(
                "Number |0|",
                "Number |3|",
                "Number |4|"));
        }
        public void TestIPlusIsNotTreatedAsInclude()
        {
            Assert.That("{$I+}", LexesAndFiltersAs());
        }
        public void TestIMinusIsNotTreatedAsInclude()
        {
            Assert.That("{$I-}", LexesAndFiltersAs());
        }
        public void TestInclude()
        {
            _fileLoader.Files["bar.inc"] = "Bar";
            Assert.That("Foo {$INCLUDE bar.inc} Baz", LexesAndFiltersAs(
                "Identifier |Foo|",
                "Identifier |Bar|",
                "Identifier |Baz|"));
        }
        public void TestDefine()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That("{$DEFINE FOO} {$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs("Identifier |Foo|"));
        }
        public void TestUndefine()
        {
            _defines.DefineSymbol("FOO");
            Assert.That("{$UNDEF FOO} {$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs(""));
        }
        public void TestDefineScopeDoesNotExtendToOtherFiles()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That("{$DEFINE FOO}", LexesAndFiltersAs(""));
            Assert.That("{$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs(""));
        }
        public void TestDefineScopeDoesBubbleUpFromIncludeFiles()
        {
            _defines.UndefineSymbol("FOO");
            _fileLoader.Files["defines.inc"] = "{$DEFINE FOO}";
            Assert.That("{$I defines.inc} {$IFDEF FOO} Foo {$ENDIF}", LexesAndFiltersAs(
                "Identifier |Foo|"));
        }
        public void TestDefineIgnoredInFalseIf()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That("{$IF False}{$DEFINE FOO}{$IFEND} {$IFDEF FOO}Foo{$ENDIF}", LexesAndFiltersAs(""));
        }
        public void TestUndefineIgnoredInFalseIf()
        {
            _defines.DefineSymbol("FOO");
            Assert.That("{$IF False}{$UNDEF FOO}{$IFEND} {$IFDEF FOO}Foo{$ENDIF}", LexesAndFiltersAs(
                "Identifier |Foo|"));
        }
        [ExpectedException(typeof(LexException))]
        public void TestThrowOnUnrecognizedDirective()
        {
            Lexer lexer = new Lexer("{$FOO}", "");
            TokenFilter filter = new TokenFilter(lexer.Tokens, _defines, _fileLoader);
            new List<Token>(filter.Tokens);
        }
        public void TestUnrecognizedIsIgnoredInFalseIf()
        {
            Assert.That("{$IF False}{$FOO}{$IFEND}", LexesAndFiltersAs(""));
        }
    }
}
