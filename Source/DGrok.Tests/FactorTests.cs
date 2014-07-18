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
    public class FactorTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Factor; }
        }

        public void TestParticle()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestUnaryNot()
        {
            Assert.That("not not 42", ParsesAs(
                "UnaryOperationNode",
                "  OperatorNode: NotKeyword |not|",
                "  OperandNode: UnaryOperationNode",
                "    OperatorNode: NotKeyword |not|",
                "    OperandNode: Number |42|"));
        }
        public void TestNotAloneDoesNotParse()
        {
            AssertDoesNotParse("not");
        }
        public void TestDotOperator()
        {
            Assert.That("Foo.Bar", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Foo|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Bar|"));
        }
    }
}
