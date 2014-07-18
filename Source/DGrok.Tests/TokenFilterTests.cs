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
        private Constraint LexesAndFiltersAs(params string[] expected)
        {
            return new LexesAsConstraint(expected, delegate(IEnumerable<Token> tokens)
            {
                CompilerDefines defines = CompilerDefines.CreateEmpty();
                defines.DefineSymbol("TRUE");
                defines.UndefineSymbol("FALSE");
                defines.DefineDirectiveAsTrue("IF True");
                defines.DefineDirectiveAsFalse("IF False");
                TokenFilter filter = new TokenFilter(tokens, defines);
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
            Parser parser = Parser.FromText("// Foo", CompilerDefines.CreateEmpty());
            Assert.That(parser.AtEof, Is.True);
        }
        public void TestSingleLetterCompilerDirectivesAreIgnored()
        {
            Assert.That("{$I+}", LexesAndFiltersAs());
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
    }
}
