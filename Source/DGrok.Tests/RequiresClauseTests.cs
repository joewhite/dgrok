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
    public class RequiresClauseTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RequiresClause; }
        }

        public void TestOneItem()
        {
            Assert.That("requires Foo;", ParsesAs(
                "RequiresClauseNode",
                "  Requires: RequiresSemikeyword |requires|",
                "  IdentList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTwoItems()
        {
            Assert.That("requires Foo, Bar;", ParsesAs(
                "RequiresClauseNode",
                "  Requires: RequiresSemikeyword |requires|",
                "  IdentList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
