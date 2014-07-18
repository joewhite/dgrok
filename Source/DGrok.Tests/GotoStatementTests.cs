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
    public class GotoStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.GotoStatement; }
        }

        public void TestNumber()
        {
            Assert.That("goto 42", ParsesAs(
                "GotoStatementNode",
                "  Goto: GotoKeyword |goto|",
                "  LabelId: Number |42|"));
        }
        public void TestIdentifier()
        {
            Assert.That("goto Foo", ParsesAs(
                "GotoStatementNode",
                "  Goto: GotoKeyword |goto|",
                "  LabelId: Identifier |Foo|"));
        }
        public void TestSemikeyword()
        {
            Assert.That("goto Absolute", ParsesAs(
                "GotoStatementNode",
                "  Goto: GotoKeyword |goto|",
                "  LabelId: Identifier |Absolute|"));
        }
    }
}
