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
                "  Operator: InheritedKeyword |inherited|",
                "  Operand: Identifier |Foo|"));
        }
        public void TestAssignment()
        {
            Assert.That("Foo := 42", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |Foo|",
                "  Operator: ColonEquals |:=|",
                "  Right: Number |42|"));
        }
        public void TestGoto()
        {
            Assert.That("goto 42", ParsesAs(
                "GotoStatementNode",
                "  Goto: GotoKeyword |goto|",
                "  LabelId: Number |42|"));
        }
        public void TestBlock()
        {
            Assert.That("begin end", ParsesAs(
                "BlockNode",
                "  Begin: BeginKeyword |begin|",
                "  StatementList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestIfStatement()
        {
            Assert.That("if Foo then Bar", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: Identifier |Bar|",
                "  Else: (none)",
                "  ElseStatement: (none)"));
        }
        public void TestCase()
        {
            Assert.That("case Foo of 1: end", ParsesAs(
                "CaseStatementNode",
                "  Case: CaseKeyword |case|",
                "  Expression: Identifier |Foo|",
                "  Of: OfKeyword |of|",
                "  SelectorList: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      Values: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Number |1|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Statement: (none)",
                "      Semicolon: (none)",
                "  Else: (none)",
                "  ElseStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestRepeat()
        {
            Assert.That("repeat until Doomsday", ParsesAs(
                "RepeatStatementNode",
                "  Repeat: RepeatKeyword |repeat|",
                "  StatementList: ListNode",
                "  Until: UntilKeyword |until|",
                "  Condition: Identifier |Doomsday|"));
        }
        public void TestWhile()
        {
            Assert.That("while Foo do Bar", ParsesAs(
                "WhileStatementNode",
                "  While: WhileKeyword |while|",
                "  Condition: Identifier |Foo|",
                "  Do: DoKeyword |do|",
                "  Statement: Identifier |Bar|"));
        }
        public void TestFor()
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
        public void TestWith()
        {
            Assert.That("with Foo do", ParsesAs(
                "WithStatementNode",
                "  With: WithKeyword |with|",
                "  ExpressionList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Do: DoKeyword |do|",
                "  Statement: (none)"));
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
        public void TestTryExcept()
        {
            Assert.That("try except end", ParsesAs(
                "TryExceptNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Except: ExceptKeyword |except|",
                "  ExceptionItemList: ListNode",
                "  Else: (none)",
                "  ElseStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestTryFinally()
        {
            Assert.That("try finally end", ParsesAs(
                "TryFinallyNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Finally: FinallyKeyword |finally|",
                "  FinallyStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestRaise()
        {
            Assert.That("raise E", ParsesAs(
                "RaiseStatementNode",
                "  Raise: RaiseKeyword |raise|",
                "  Exception: Identifier |E|",
                "  At: (none)",
                "  Address: (none)"));
        }
    }
}
