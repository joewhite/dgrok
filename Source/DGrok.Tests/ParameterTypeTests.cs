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
    public class ParameterTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ParameterType; }
        }

        public void TestIdentifier()
        {
            Assert.That("TFoo", ParsesAs("Identifier |TFoo|"));
        }
        public void TestQualifiedIdentifier()
        {
            Assert.That("Foo.TBar", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |Foo|",
                "  Operator: Dot |.|",
                "  Right: Identifier |TBar|"));
        }
        public void TestString()
        {
            Assert.That("string", ParsesAs("StringKeyword |string|"));
        }
        public void TestFile()
        {
            Assert.That("file", ParsesAs("FileKeyword |file|"));
        }
        public void TestOpenArray()
        {
            Assert.That("array of TFoo", ParsesAs(
                "OpenArrayNode",
                "  Array: ArrayKeyword |array|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |TFoo|"));
        }
    }
}
