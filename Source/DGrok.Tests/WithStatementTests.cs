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
    public class WithStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.WithStatement; }
        }

        public void TestEmpty()
        {
            Assert.That("with Foo do", ParsesAs(
                "WithStatementNode",
                "  With: WithKeyword |with|",
                "  ExpressionList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Do: DoKeyword |do|",
                "  Statement: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("with Foo do Bar", ParsesAs(
                "WithStatementNode",
                "  With: WithKeyword |with|",
                "  ExpressionList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Do: DoKeyword |do|",
                "  Statement: Identifier |Bar|"));
        }
    }
}
