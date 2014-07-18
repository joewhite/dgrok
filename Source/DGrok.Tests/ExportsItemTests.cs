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
    public class ExportsItemTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExportsItem; }
        }

        public void TestNameOnly()
        {
            Assert.That("Foo", ParsesAs(
                "ExportsItemNode",
                "  NameNode: Identifier |Foo|",
                "  SpecifierListNode: ListNode"));
        }
        public void TestDottedName()
        {
            Assert.That("Foo.Bar", ParsesAs(
                "ExportsItemNode",
                "  NameNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  SpecifierListNode: ListNode"));
        }
        public void TestIndex()
        {
            Assert.That("Foo index 42", ParsesAs(
                "ExportsItemNode",
                "  NameNode: Identifier |Foo|",
                "  SpecifierListNode: ListNode",
                "    Items[0]: ExportsSpecifierNode",
                "      KeywordNode: IndexSemikeyword |index|",
                "      ValueNode: Number |42|"));
        }
        public void TestName()
        {
            Assert.That("Foo name 'Foo'", ParsesAs(
                "ExportsItemNode",
                "  NameNode: Identifier |Foo|",
                "  SpecifierListNode: ListNode",
                "    Items[0]: ExportsSpecifierNode",
                "      KeywordNode: NameSemikeyword |name|",
                "      ValueNode: StringLiteral |'Foo'|"));
        }
        public void TestIndexAndName()
        {
            Assert.That("Foo index 42 name 'Foo'", ParsesAs(
                "ExportsItemNode",
                "  NameNode: Identifier |Foo|",
                "  SpecifierListNode: ListNode",
                "    Items[0]: ExportsSpecifierNode",
                "      KeywordNode: IndexSemikeyword |index|",
                "      ValueNode: Number |42|",
                "    Items[1]: ExportsSpecifierNode",
                "      KeywordNode: NameSemikeyword |name|",
                "      ValueNode: StringLiteral |'Foo'|"));
        }
        public void TestNameAndIndex()
        {
            Assert.That("Foo name 'Foo' index 42", ParsesAs(
                "ExportsItemNode",
                "  NameNode: Identifier |Foo|",
                "  SpecifierListNode: ListNode",
                "    Items[0]: ExportsSpecifierNode",
                "      KeywordNode: NameSemikeyword |name|",
                "      ValueNode: StringLiteral |'Foo'|",
                "    Items[1]: ExportsSpecifierNode",
                "      KeywordNode: IndexSemikeyword |index|",
                "      ValueNode: Number |42|"));
        }
    }
}
