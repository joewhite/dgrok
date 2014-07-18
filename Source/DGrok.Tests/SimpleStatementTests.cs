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
