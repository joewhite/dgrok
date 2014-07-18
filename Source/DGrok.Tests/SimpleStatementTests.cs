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
    public class SimpleStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SimpleStatement; }
        }

        public void TestBareInherited()
        {
            Assert.That("inherited", ParsesAs("InheritedKeyword |inherited|"));
        }
        public void TestInheritedExpression()
        {
            Assert.That("inherited Foo", ParsesAs(
                "UnaryOperationNode",
                "  OperatorNode: InheritedKeyword |inherited|",
                "  OperandNode: Identifier |Foo|"));
        }
        public void TestAssignment()
        {
            Assert.That("Foo := 42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Foo|",
                "  OperatorNode: ColonEquals |:=|",
                "  RightNode: Number |42|"));
        }
        public void TestGoto()
        {
            Assert.That("goto 42", ParsesAs(
                "GotoStatementNode",
                "  GotoKeywordNode: GotoKeyword |goto|",
                "  LabelIdNode: Number |42|"));
        }
        public void TestBlock()
        {
            Assert.That("begin end", ParsesAs(
                "BlockNode",
                "  BeginKeywordNode: BeginKeyword |begin|",
                "  StatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestIfStatement()
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
        public void TestCase()
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
        public void TestRepeat()
        {
            Assert.That("repeat until Doomsday", ParsesAs(
                "RepeatStatementNode",
                "  RepeatKeywordNode: RepeatKeyword |repeat|",
                "  StatementListNode: ListNode",
                "  UntilKeywordNode: UntilKeyword |until|",
                "  ConditionNode: Identifier |Doomsday|"));
        }
        public void TestWhile()
        {
            Assert.That("while Foo do Bar", ParsesAs(
                "WhileStatementNode",
                "  WhileKeywordNode: WhileKeyword |while|",
                "  ConditionNode: Identifier |Foo|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Bar|"));
        }
        public void TestFor()
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
        public void TestWith()
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
        public void TestTryExcept()
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
        public void TestTryFinally()
        {
            Assert.That("try finally end", ParsesAs(
                "TryFinallyNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  FinallyKeywordNode: FinallyKeyword |finally|",
                "  FinallyStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestRaise()
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
