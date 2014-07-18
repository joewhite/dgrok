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
    public class PointerTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.PointerType; }
        }

        public void TestIdentifier()
        {
            Assert.That("^TFoo", ParsesAs(
                "PointerTypeNode",
                "  CaretNode: Caret |^|",
                "  TypeNode: Identifier |TFoo|"));
        }
        public void TestQualified()
        {
            Assert.That("^Foo.TBar", ParsesAs(
                "PointerTypeNode",
                "  CaretNode: Caret |^|",
                "  TypeNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |TBar|"));
        }
        public void TestString()
        {
            Assert.That("^string", ParsesAs(
                "PointerTypeNode",
                "  CaretNode: Caret |^|",
                "  TypeNode: StringKeyword |string|"));
        }
    }
}
