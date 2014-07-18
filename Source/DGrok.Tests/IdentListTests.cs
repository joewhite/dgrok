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
    public class IdentListTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.IdentList; }
        }

        public void TestOneIdent()
        {
            Assert.That("Foo", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |Foo|",
                "    Delimiter: (none)"));
        }
        public void TestTwoIdents()
        {
            Assert.That("Foo, Bar", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |Foo|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Identifier |Bar|",
                "    Delimiter: (none)"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute, Index", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |Absolute|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Identifier |Index|",
                "    Delimiter: (none)"));
        }
    }
}
