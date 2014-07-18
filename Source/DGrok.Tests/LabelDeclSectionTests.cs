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
    public class LabelDeclSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.LabelDeclSection; }
        }

        public void TestOneNumber()
        {
            Assert.That("label 42;", ParsesAs(
                "LabelDeclSectionNode",
                "  Label: LabelKeyword |label|",
                "  LabelList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestOneIdentifier()
        {
            Assert.That("label Foo;", ParsesAs(
                "LabelDeclSectionNode",
                "  Label: LabelKeyword |label|",
                "  LabelList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestOneSemikeyword()
        {
            Assert.That("label Absolute;", ParsesAs(
                "LabelDeclSectionNode",
                "  Label: LabelKeyword |label|",
                "  LabelList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Absolute|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestMixed()
        {
            Assert.That("label Answer, 42;", ParsesAs(
                "LabelDeclSectionNode",
                "  Label: LabelKeyword |label|",
                "  LabelList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Answer|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
