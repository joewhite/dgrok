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
    public class LabelIdTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.LabelId; }
        }

        public void TestNumber()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestIdentifier()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        public void TestSemikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
    }
}
