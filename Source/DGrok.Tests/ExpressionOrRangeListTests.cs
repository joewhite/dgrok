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
    public class ExpressionOrRangeListTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExpressionOrRangeList; }
        }

        [Test]
        public void SingleNumber()
        {
            Assert.That("42", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Number |42|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void SingleExpression()
        {
            Assert.That("6 * 9", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: BinaryOperationNode",
                "      LeftNode: Number |6|",
                "      OperatorNode: TimesSign |*|",
                "      RightNode: Number |9|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void SingleRange()
        {
            Assert.That("24..42", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: BinaryOperationNode",
                "      LeftNode: Number |24|",
                "      OperatorNode: DotDot |..|",
                "      RightNode: Number |42|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void TwoNumbers()
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
        public void TwoCharacterLiterals()
        {
            Assert.That("'4', '2'", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: StringLiteral |'4'|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: StringLiteral |'2'|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void TwoIdents()
        {
            Assert.That("fooBar, fooBaz", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |fooBar|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Identifier |fooBaz|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void TwoSemikeywords()
        {
            Assert.That("Abstract, Index", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Abstract|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Identifier |Index|",
                "    DelimiterNode: (none)"));
        }
        [Test]
        public void TwoParenthesizedExpressions()
        {
            Assert.That("(1), (2)", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: ParenthesizedExpressionNode",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      ExpressionNode: Number |1|",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: ParenthesizedExpressionNode",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      ExpressionNode: Number |2|",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "    DelimiterNode: (none)"));
        }
    }
}
