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
    public class OpenArrayTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.OpenArray; }
        }

        public void TestArrayOfIdentifier()
        {
            Assert.That("array of TFoo", ParsesAs(
                "OpenArrayNode",
                "  Array: ArrayKeyword |array|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |TFoo|"));
        }
        public void TestArrayOfAtom()
        {
            Assert.That("array of Foo.TBar", ParsesAs(
                "OpenArrayNode",
                "  Array: ArrayKeyword |array|",
                "  Of: OfKeyword |of|",
                "  Type: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |TBar|"));
        }
        public void TestArrayOfString()
        {
            Assert.That("array of string", ParsesAs(
                "OpenArrayNode",
                "  Array: ArrayKeyword |array|",
                "  Of: OfKeyword |of|",
                "  Type: StringKeyword |string|"));
        }
        public void TestArrayOfFile()
        {
            Assert.That("array of file", ParsesAs(
                "OpenArrayNode",
                "  Array: ArrayKeyword |array|",
                "  Of: OfKeyword |of|",
                "  Type: FileKeyword |file|"));
        }
        public void TestArrayOfConst()
        {
            Assert.That("array of const", ParsesAs(
                "OpenArrayNode",
                "  Array: ArrayKeyword |array|",
                "  Of: OfKeyword |of|",
                "  Type: ConstKeyword |const|"));
        }
    }
}
