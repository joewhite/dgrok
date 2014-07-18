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
using NUnitLite.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class ForStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ForStatement; }
        }

        public void TestSimple()
        {
            Assert.That("for I := 1 to 42 do", ParsesAs(
                "ForStatementNode",
                "  For: ForKeyword |for|",
                "  LoopVariable: Identifier |I|",
                "  ColonEquals: ColonEquals |:=|",
                "  StartingValue: Number |1|",
                "  Direction: ToKeyword |to|",
                "  EndingValue: Number |42|",
                "  Do: DoKeyword |do|",
                "  Statement: (none)"));
        }
        public void TestDownTo()
        {
            Assert.That("for I := 1 downto 42 do", ParsesAs(
                "ForStatementNode",
                "  For: ForKeyword |for|",
                "  LoopVariable: Identifier |I|",
                "  ColonEquals: ColonEquals |:=|",
                "  StartingValue: Number |1|",
                "  Direction: DownToKeyword |downto|",
                "  EndingValue: Number |42|",
                "  Do: DoKeyword |do|",
                "  Statement: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("for I := 1 to 42 do Foo", ParsesAs(
                "ForStatementNode",
                "  For: ForKeyword |for|",
                "  LoopVariable: Identifier |I|",
                "  ColonEquals: ColonEquals |:=|",
                "  StartingValue: Number |1|",
                "  Direction: ToKeyword |to|",
                "  EndingValue: Number |42|",
                "  Do: DoKeyword |do|",
                "  Statement: Identifier |Foo|"));
        }
        public void TestForIn()
        {
            Assert.That("for Obj in List do", ParsesAs(
                "ForInStatementNode",
                "  For: ForKeyword |for|",
                "  LoopVariable: Identifier |Obj|",
                "  In: InKeyword |in|",
                "  Expression: Identifier |List|",
                "  Do: DoKeyword |do|",
                "  Statement: (none)"));
        }
        public void TestForInWithStatement()
        {
            Assert.That("for Obj in List do Foo", ParsesAs(
                "ForInStatementNode",
                "  For: ForKeyword |for|",
                "  LoopVariable: Identifier |Obj|",
                "  In: InKeyword |in|",
                "  Expression: Identifier |List|",
                "  Do: DoKeyword |do|",
                "  Statement: Identifier |Foo|"));
        }
    }
}
