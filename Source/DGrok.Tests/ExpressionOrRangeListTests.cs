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
