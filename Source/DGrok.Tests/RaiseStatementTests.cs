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
    public class RaiseStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RaiseStatement; }
        }

        [Test]
        public void BareRaise()
        {
            Assert.That("raise", ParsesAs(
                "RaiseStatementNode",
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: (none)",
                "  AtSemikeywordNode: (none)",
                "  AddressNode: (none)"));
        }
        [Test]
        public void RaiseVariable()
        {
            Assert.That("raise E", ParsesAs(
                "RaiseStatementNode",
                "  RaiseKeywordNode: RaiseKeyword |raise|",
                "  ExceptionNode: Identifier |E|",
                "  AtSemikeywordNode: (none)",
                "  AddressNode: (none)"));
        }
        [Test]
        public void RaiseExceptionCreate()
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
        [Test]
        public void RaiseAtAddress()
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
