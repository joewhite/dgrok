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
                "  OpenParenthesis: OpenParenthesis |(|",
                "  Expression: BinaryOperationNode",
                "    Left: Number |6|",
                "    Operator: TimesSign |*|",
                "    Right: Number |9|",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestList()
        {
            Assert.That("(6, 9)", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |6|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Number |9|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestRecord()
        {
            Assert.That("(Foo: 24; Bar.Baz: 42)", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: RecordFieldConstantNode",
                "        Name: Identifier |Foo|",
                "        Colon: Colon |:|",
                "        Value: Number |24|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: RecordFieldConstantNode",
                "        Name: BinaryOperationNode",
                "          Left: Identifier |Bar|",
                "          Operator: Dot |.|",
                "          Right: Identifier |Baz|",
                "        Colon: Colon |:|",
                "        Value: Number |42|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestListOfExpressions()
        {
            Assert.That("((6), (9))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ParenthesizedExpressionNode",
                "        OpenParenthesis: OpenParenthesis |(|",
                "        Expression: Number |6|",
                "        CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: ParenthesizedExpressionNode",
                "        OpenParenthesis: OpenParenthesis |(|",
                "        Expression: Number |9|",
                "        CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestListOfLists()
        {
            Assert.That("((6, 9))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ConstantListNode",
                "        OpenParenthesis: OpenParenthesis |(|",
                "        ItemList: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: Number |6|",
                "            Delimiter: Comma |,|",
                "          Items[1]: DelimitedItemNode",
                "            Item: Number |9|",
                "            Delimiter: (none)",
                "        CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestListOfRecords()
        {
            Assert.That("((Foo: 42))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ConstantListNode",
                "        OpenParenthesis: OpenParenthesis |(|",
                "        ItemList: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: RecordFieldConstantNode",
                "              Name: Identifier |Foo|",
                "              Colon: Colon |:|",
                "              Value: Number |42|",
                "            Delimiter: (none)",
                "        CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestRecordOfExpressions()
        {
            Assert.That("(Foo: (42))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: RecordFieldConstantNode",
                "        Name: Identifier |Foo|",
                "        Colon: Colon |:|",
                "        Value: ParenthesizedExpressionNode",
                "          OpenParenthesis: OpenParenthesis |(|",
                "          Expression: Number |42|",
                "          CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestRecordOfLists()
        {
            Assert.That("(Foo: (6, 9))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: RecordFieldConstantNode",
                "        Name: Identifier |Foo|",
                "        Colon: Colon |:|",
                "        Value: ConstantListNode",
                "          OpenParenthesis: OpenParenthesis |(|",
                "          ItemList: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              Item: Number |6|",
                "              Delimiter: Comma |,|",
                "            Items[1]: DelimitedItemNode",
                "              Item: Number |9|",
                "              Delimiter: (none)",
                "          CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestRecordOfRecords()
        {
            Assert.That("(Foo: (Bar: 42))", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: RecordFieldConstantNode",
                "        Name: Identifier |Foo|",
                "        Colon: Colon |:|",
                "        Value: ConstantListNode",
                "          OpenParenthesis: OpenParenthesis |(|",
                "          ItemList: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              Item: RecordFieldConstantNode",
                "                Name: Identifier |Bar|",
                "                Colon: Colon |:|",
                "                Value: Number |42|",
                "              Delimiter: (none)",
                "          CloseParenthesis: CloseParenthesis |)|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestEmptyParentheses()
        {
            Assert.That("()", ParsesAs(
                "ConstantListNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
    }
}
