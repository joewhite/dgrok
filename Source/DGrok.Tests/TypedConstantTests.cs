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
    public class TypedConstantTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypedConstant; }
        }

        public void TestNumber()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestParenthesizedExpression()
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
        public void TestList()
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
        public void TestRecord()
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
        public void TestListOfExpressions()
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
        public void TestListOfLists()
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
        public void TestListOfRecords()
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
        public void TestRecordOfExpressions()
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
        public void TestRecordOfLists()
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
        public void TestRecordOfRecords()
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
        public void TestEmptyParentheses()
        {
            Assert.That("()", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
    }
}
