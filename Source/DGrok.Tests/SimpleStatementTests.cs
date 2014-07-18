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
