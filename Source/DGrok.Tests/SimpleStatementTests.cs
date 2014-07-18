// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class SimpleStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SimpleStatement; }
        }

        [Test]
        public void BareInherited()
        {
            Assert.That("inherited", ParsesAs("InheritedKeyword |inherited|"));
        }
        [Test]
        public void InheritedExpression()
        {
            Assert.That("inherited Foo", ParsesAs(
                "UnaryOperationNode",
                "  OperatorNode: InheritedKeyword |inherited|",
                "  OperandNode: Identifier |Foo|"));
        }
        [Test]
        public void Assignment()
        {
            Assert.That("Foo := 42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Foo|",
                "  OperatorNode: ColonEquals |:=|",
                "  RightNode: Number |42|"));
        }
        [Test]
        public void Goto()
        {
            Assert.That("goto 42", ParsesAs(
                "GotoStatementNode",
                "  GotoKeywordNode: GotoKeyword |goto|",
                "  LabelIdNode: Number |42|"));
        }
        [Test]
        public void Block()
        {
            Assert.That("begin end", ParsesAs(
                "BlockNode",
                "  BeginKeywordNode: BeginKeyword |begin|",
                "  StatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void IfStatement()
        {
            Assert.That("if Foo then Bar", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: Identifier |Bar|",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        [Test]
        public void Case()
        {
            Assert.That("case Foo of 1: end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Repeat()
        {
            Assert.That("repeat until Doomsday", ParsesAs(
                "RepeatStatementNode",
                "  RepeatKeywordNode: RepeatKeyword |repeat|",
                "  StatementListNode: ListNode",
                "  UntilKeywordNode: UntilKeyword |until|",
                "  ConditionNode: Identifier |Doomsday|"));
        }
        [Test]
        public void While()
        {
            Assert.That("while Foo do Bar", ParsesAs(
                "WhileStatementNode",
                "  WhileKeywordNode: WhileKeyword |while|",
                "  ConditionNode: Identifier |Foo|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Bar|"));
        }
        [Test]
        public void For()
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
        [Test]
        public void With()
        {
            Assert.That("with Foo do", ParsesAs(
                "WithStatementNode",
                "  WithKeywordNode: WithKeyword |with|",
                "  ExpressionListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        [Test]
        public void ForIn()
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
        [Test]
        public void TryExcept()
        {
            Assert.That("try except end", ParsesAs(
                "TryExceptNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  ExceptKeywordNode: ExceptKeyword |except|",
                "  ExceptionItemListNode: ListNode",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void TryFinally()
        {
            Assert.That("try finally end", ParsesAs(
                "TryFinallyNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  FinallyKeywordNode: FinallyKeyword |finally|",
                "  FinallyStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Raise()
        {
            Assert.That("raise E", ParsesAs(
                "RaiseStatementNode",
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: Identifier |E|",
                "  AtSemikeywordNode: (none)",
                "  AddressNode: (none)"));
        }
    }
}
