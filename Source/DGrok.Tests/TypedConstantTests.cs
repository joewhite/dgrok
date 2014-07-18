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
    public class TypedConstantTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypedConstant; }
        }

        [Test]
        public void Number()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void ParenthesizedExpression()
        {
            Assert.That("(6 * 9)", ParsesAs(
                "ParenthesizedExpressionNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ExpressionNode: BinaryOperationNode",
                "    LeftNode: Number |6|",
                "    OperatorNode: TimesSign |*|",
                "    RightNode: Number |9|",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void List()
        {
            Assert.That("(6, 9)", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |6|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Number |9|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void Record()
        {
            Assert.That("(Foo: 24; Bar.Baz: 42)", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: RecordFieldConstantNode",
                "        NameNode: Identifier |Foo|",
                "        ColonNode: Colon |:|",
                "        ValueNode: Number |24|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: RecordFieldConstantNode",
                "        NameNode: BinaryOperationNode",
                "          LeftNode: Identifier |Bar|",
                "          OperatorNode: Dot |.|",
                "          RightNode: Identifier |Baz|",
                "        ColonNode: Colon |:|",
                "        ValueNode: Number |42|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void ListOfExpressions()
        {
            Assert.That("((6), (9))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ParenthesizedExpressionNode",
                "        OpenParenthesisNode: OpenParenthesis |(|",
                "        ExpressionNode: Number |6|",
                "        CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: ParenthesizedExpressionNode",
                "        OpenParenthesisNode: OpenParenthesis |(|",
                "        ExpressionNode: Number |9|",
                "        CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void ListOfLists()
        {
            Assert.That("((6, 9))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ConstantListNode",
                "        OpenParenthesisNode: OpenParenthesis |(|",
                "        ItemListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Number |6|",
                "            DelimiterNode: Comma |,|",
                "          Items[1]: DelimitedItemNode",
                "            ItemNode: Number |9|",
                "            DelimiterNode: (none)",
                "        CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void ListOfRecords()
        {
            Assert.That("((Foo: 42))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ConstantListNode",
                "        OpenParenthesisNode: OpenParenthesis |(|",
                "        ItemListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: RecordFieldConstantNode",
                "              NameNode: Identifier |Foo|",
                "              ColonNode: Colon |:|",
                "              ValueNode: Number |42|",
                "            DelimiterNode: (none)",
                "        CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void RecordOfExpressions()
        {
            Assert.That("(Foo: (42))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: RecordFieldConstantNode",
                "        NameNode: Identifier |Foo|",
                "        ColonNode: Colon |:|",
                "        ValueNode: ParenthesizedExpressionNode",
                "          OpenParenthesisNode: OpenParenthesis |(|",
                "          ExpressionNode: Number |42|",
                "          CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void RecordOfLists()
        {
            Assert.That("(Foo: (6, 9))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: RecordFieldConstantNode",
                "        NameNode: Identifier |Foo|",
                "        ColonNode: Colon |:|",
                "        ValueNode: ConstantListNode",
                "          OpenParenthesisNode: OpenParenthesis |(|",
                "          ItemListNode: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              ItemNode: Number |6|",
                "              DelimiterNode: Comma |,|",
                "            Items[1]: DelimitedItemNode",
                "              ItemNode: Number |9|",
                "              DelimiterNode: (none)",
                "          CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void RecordOfRecords()
        {
            Assert.That("(Foo: (Bar: 42))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: RecordFieldConstantNode",
                "        NameNode: Identifier |Foo|",
                "        ColonNode: Colon |:|",
                "        ValueNode: ConstantListNode",
                "          OpenParenthesisNode: OpenParenthesis |(|",
                "          ItemListNode: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              ItemNode: RecordFieldConstantNode",
                "                NameNode: Identifier |Bar|",
                "                ColonNode: Colon |:|",
                "                ValueNode: Number |42|",
                "              DelimiterNode: (none)",
                "          CloseParenthesisNode: CloseParenthesis |)|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void EmptyParentheses()
        {
            Assert.That("()", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
    }
}
