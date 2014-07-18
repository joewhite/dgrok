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
    public class StringTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.StringType; }
        }

        public void TestNoLength()
        {
            Assert.That("string", ParsesAs("StringKeyword |string|"));
        }
        public void TestWithLength()
        {
            Assert.That("string[42]", ParsesAs(
                "StringOfLengthNode",
                "  String: StringKeyword |string|",
                "  OpenBracket: OpenBracket |[|",
                "  Length: Number |42|",
                "  CloseBracket: CloseBracket |]|"));
        }
    }
}
