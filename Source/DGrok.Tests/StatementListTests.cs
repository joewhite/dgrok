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
    public class StatementListTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.StatementList; }
        }

        public void TestSingleStatementNoSemicolon()
        {
            Assert.That("Foo", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Foo|",
                "    DelimiterNode: (none)"));
        }
        public void TestSingleStatementWithSemicolon()
        {
            Assert.That("Foo;", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Foo|",
                "    DelimiterNode: Semicolon |;|"));
        }
        public void TestLoneSemicolon()
        {
            Assert.That(";", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: (none)",
                "    DelimiterNode: Semicolon |;|"));
        }
    }
}
