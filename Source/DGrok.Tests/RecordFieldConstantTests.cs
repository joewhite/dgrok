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
    public class RecordFieldConstantTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RecordFieldConstant; }
        }

        public void TestSimple()
        {
            Assert.That("Foo: 42", ParsesAs(
                "RecordFieldConstantNode",
                "  Name: Identifier |Foo|",
                "  Colon: Colon |:|",
                "  Value: Number |42|"));
        }
        public void TestDottedName()
        {
            Assert.That("Foo.Bar: 42", ParsesAs(
                "RecordFieldConstantNode",
                "  Name: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |Bar|",
                "  Colon: Colon |:|",
                "  Value: Number |42|"));
        }
    }
}
