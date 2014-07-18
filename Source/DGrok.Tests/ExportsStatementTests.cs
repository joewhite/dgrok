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
    public class ExportsStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExportsStatement; }
        }

        public void TestOneSimple()
        {
            Assert.That("exports Foo;", ParsesAs(
                "ExportsStatementNode",
                "  Exports: ExportsKeyword |exports|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ExportsItemNode",
                "        Name: Identifier |Foo|",
                "        SpecifierList: ListNode",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTwoFancy()
        {
            Assert.That("exports Foo index 42, Bar name 'Baz';", ParsesAs(
                "ExportsStatementNode",
                "  Exports: ExportsKeyword |exports|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ExportsItemNode",
                "        Name: Identifier |Foo|",
                "        SpecifierList: ListNode",
                "          Items[0]: ExportsSpecifierNode",
                "            Keyword: IndexSemikeyword |index|",
                "            Value: Number |42|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: ExportsItemNode",
                "        Name: Identifier |Bar|",
                "        SpecifierList: ListNode",
                "          Items[0]: ExportsSpecifierNode",
                "            Keyword: NameSemikeyword |name|",
                "            Value: StringLiteral |'Baz'|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
