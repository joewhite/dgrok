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
                "  WithKeywordNode: WithKeyword |with|",
                "  ExpressionListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("with Foo do Bar", ParsesAs(
                "WithStatementNode",
                "  WithKeywordNode: WithKeyword |with|",
                "  ExpressionListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Bar|"));
        }
    }
}
