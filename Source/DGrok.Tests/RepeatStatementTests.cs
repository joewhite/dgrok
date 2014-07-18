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
                "  Repeat: RepeatKeyword |repeat|",
                "  StatementList: ListNode",
                "  Until: UntilKeyword |until|",
                "  Condition: Identifier |Doomsday|"));
        }
        public void TestPopulated()
        {
            Assert.That("repeat Foo; Bar; until Doomsday", ParsesAs(
                "RepeatStatementNode",
                "  Repeat: RepeatKeyword |repeat|",
                "  StatementList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  Until: UntilKeyword |until|",
                "  Condition: Identifier |Doomsday|"));
        }
    }
}
