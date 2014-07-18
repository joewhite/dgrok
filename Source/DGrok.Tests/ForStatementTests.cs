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
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |I|",
                "  ColonEqualsNode: ColonEquals |:=|",
                "  StartingValueNode: Number |1|",
                "  DirectionNode: ToKeyword |to|",
                "  EndingValueNode: Number |42|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        public void TestDownTo()
        {
            Assert.That("for I := 1 downto 42 do", ParsesAs(
                "ForStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |I|",
                "  ColonEqualsNode: ColonEquals |:=|",
                "  StartingValueNode: Number |1|",
                "  DirectionNode: DownToKeyword |downto|",
                "  EndingValueNode: Number |42|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("for I := 1 to 42 do Foo", ParsesAs(
                "ForStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |I|",
                "  ColonEqualsNode: ColonEquals |:=|",
                "  StartingValueNode: Number |1|",
                "  DirectionNode: ToKeyword |to|",
                "  EndingValueNode: Number |42|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Foo|"));
        }
        public void TestForIn()
        {
            Assert.That("for Obj in List do", ParsesAs(
                "ForInStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |Obj|",
                "  InKeywordNode: InKeyword |in|",
                "  ExpressionNode: Identifier |List|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        public void TestForInWithStatement()
        {
            Assert.That("for Obj in List do Foo", ParsesAs(
                "ForInStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |Obj|",
                "  InKeywordNode: InKeyword |in|",
                "  ExpressionNode: Identifier |List|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Foo|"));
        }
    }
}
