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
    public class WhileStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.WhileStatement; }
        }

        public void TestNoBody()
        {
            Assert.That("while Foo do", ParsesAs(
                "WhileStatementNode",
                "  WhileKeywordNode: WhileKeyword |while|",
                "  ConditionNode: Identifier |Foo|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        public void TestWithBody()
        {
            Assert.That("while Foo do Bar", ParsesAs(
                "WhileStatementNode",
                "  WhileKeywordNode: WhileKeyword |while|",
                "  ConditionNode: Identifier |Foo|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Bar|"));
        }
    }
}
