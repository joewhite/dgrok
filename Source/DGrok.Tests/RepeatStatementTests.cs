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
    public class RepeatStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RepeatStatement; }
        }

        public void TestEmpty()
        {
            Assert.That("repeat until Doomsday", ParsesAs(
                "RepeatStatementNode",
                "  RepeatKeywordNode: RepeatKeyword |repeat|",
                "  StatementListNode: ListNode",
                "  UntilKeywordNode: UntilKeyword |until|",
                "  ConditionNode: Identifier |Doomsday|"));
        }
        public void TestPopulated()
        {
            Assert.That("repeat Foo; Bar; until Doomsday", ParsesAs(
                "RepeatStatementNode",
                "  RepeatKeywordNode: RepeatKeyword |repeat|",
                "  StatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  UntilKeywordNode: UntilKeyword |until|",
                "  ConditionNode: Identifier |Doomsday|"));
        }
    }
}
