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
    public class ExpressionListTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExpressionList; }
        }

        [Test]
        public void Numbers()
        {
            Assert.That("24, 42", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Number |24|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Number |42|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void StringLiterals()
        {
            Assert.That("'foo', 'bar'", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: StringLiteral |'foo'|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: StringLiteral |'bar'|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void Identifiers()
        {
            Assert.That("Foo, Bar", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Foo|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Identifier |Bar|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void Semikeywords()
        {
            Assert.That("Absolute, Index", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Absolute|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Identifier |Index|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void Nils()
        {
            Assert.That("nil, nil", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: NilKeyword |nil|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: NilKeyword |nil|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void ParenthesizedExpressions()
        {
            Assert.That("(24), (42)", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: ParenthesizedExpressionNode",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      ExpressionNode: Number |24|",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: ParenthesizedExpressionNode",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      ExpressionNode: Number |42|",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void SetLiterals()
        {
            Assert.That("[], []", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: SetLiteralNode",
                "      OpenBracketNode: OpenBracket |[|",
                "      ItemListNode: ListNode",
                "      CloseBracketNode: CloseBracket |]|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: SetLiteralNode",
                "      OpenBracketNode: OpenBracket |[|",
                "      ItemListNode: ListNode",
                "      CloseBracketNode: CloseBracket |]|",
                "    DelimiterNode: (none)"));
        }
    }
}
