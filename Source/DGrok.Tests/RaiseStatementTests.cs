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
    public class RaiseStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RaiseStatement; }
        }

        public void TestBareRaise()
        {
            Assert.That("raise", ParsesAs(
                "RaiseStatementNode",
                "  Raise: RaiseKeyword |raise|",
                "  Exception: (none)",
                "  At: (none)",
                "  Address: (none)"));
        }
        public void TestRaiseVariable()
        {
            Assert.That("raise E", ParsesAs(
                "RaiseStatementNode",
                "  Raise: RaiseKeyword |raise|",
                "  Exception: Identifier |E|",
                "  At: (none)",
                "  Address: (none)"));
        }
        public void TestRaiseExceptionCreate()
        {
            Assert.That("raise Exception.Create('Foo')", ParsesAs(
                "RaiseStatementNode",
                "  Raise: RaiseKeyword |raise|",
                "  Exception: ParameterizedNode",
                "    Left: BinaryOperationNode",
                "      Left: Identifier |Exception|",
                "      Operator: Dot |.|",
                "      Right: Identifier |Create|",
                "    OpenDelimiter: OpenParenthesis |(|",
                "    ParameterList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: StringLiteral |'Foo'|",
                "        Delimiter: (none)",
                "    CloseDelimiter: CloseParenthesis |)|",
                "  At: (none)",
                "  Address: (none)"));
        }
        public void TestRaiseAtAddress()
        {
            Assert.That("raise E at Address", ParsesAs(
                "RaiseStatementNode",
                "  Raise: RaiseKeyword |raise|",
                "  Exception: Identifier |E|",
                "  At: AtSemikeyword |at|",
                "  Address: Identifier |Address|"));
        }
    }
}
