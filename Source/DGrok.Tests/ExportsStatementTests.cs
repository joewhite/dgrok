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
                "  ExportsKeywordNode: ExportsKeyword |exports|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ExportsItemNode",
                "        NameNode: Identifier |Foo|",
                "        SpecifierListNode: ListNode",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestTwoFancy()
        {
            Assert.That("exports Foo index 42, Bar name 'Baz';", ParsesAs(
                "ExportsStatementNode",
                "  ExportsKeywordNode: ExportsKeyword |exports|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ExportsItemNode",
                "        NameNode: Identifier |Foo|",
                "        SpecifierListNode: ListNode",
                "          Items[0]: ExportsSpecifierNode",
                "            KeywordNode: IndexSemikeyword |index|",
                "            ValueNode: Number |42|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: ExportsItemNode",
                "        NameNode: Identifier |Bar|",
                "        SpecifierListNode: ListNode",
                "          Items[0]: ExportsSpecifierNode",
                "            KeywordNode: NameSemikeyword |name|",
                "            ValueNode: StringLiteral |'Baz'|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
