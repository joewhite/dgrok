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
