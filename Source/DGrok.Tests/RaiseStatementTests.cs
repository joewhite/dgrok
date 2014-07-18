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
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: (none)",
                "  AtSemikeywordNode: (none)",
                "  AddressNode: (none)"));
        }
        public void TestRaiseVariable()
        {
            Assert.That("raise E", ParsesAs(
                "RaiseStatementNode",
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: Identifier |E|",
                "  AtSemikeywordNode: (none)",
                "  AddressNode: (none)"));
        }
        public void TestRaiseExceptionCreate()
        {
            Assert.That("raise Exception.Create('Foo')", ParsesAs(
                "RaiseStatementNode",
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: ParameterizedNode",
                "    LeftNode: BinaryOperationNode",
                "      LeftNode: Identifier |Exception|",
                "      OperatorNode: Dot |.|",
                "      RightNode: Identifier |Create|",
                "    OpenDelimiterNode: OpenParenthesis |(|",
                "    ParameterListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: StringLiteral |'Foo'|",
                "        DelimiterNode: (none)",
                "    CloseDelimiterNode: CloseParenthesis |)|",
                "  AtSemikeywordNode: (none)",
                "  AddressNode: (none)"));
        }
        public void TestRaiseAtAddress()
        {
            Assert.That("raise E at Address", ParsesAs(
                "RaiseStatementNode",
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: Identifier |E|",
                "  AtSemikeywordNode: AtSemikeyword |at|",
                "  AddressNode: Identifier |Address|"));
        }
    }
}
