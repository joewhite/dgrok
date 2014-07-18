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
    public class StatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Statement; }
        }

        public void TestSimpleStatement()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        public void TestLabelAlone()
        {
            Assert.That("Foo:", ParsesAs(
                "LabeledStatementNode",
                "  LabelIdNode: Identifier |Foo|",
                "  ColonNode: Colon |:|",
                "  StatementNode: (none)"));
        }
        public void TestLabeledStatement()
        {
            Assert.That("Foo: Bar", ParsesAs(
                "LabeledStatementNode",
                "  LabelIdNode: Identifier |Foo|",
                "  ColonNode: Colon |:|",
                "  StatementNode: Identifier |Bar|"));
        }
    }
}
