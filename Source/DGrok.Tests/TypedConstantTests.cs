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
