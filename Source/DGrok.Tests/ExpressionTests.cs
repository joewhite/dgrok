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
    public class ExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Expression; }
        }

        public void TestNumber()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestSimpleExpression()
        {
            Assert.That("40 + 2", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |40|",
                "  Operator: PlusSign |+|",
                "  Right: Number |2|"));
        }
        public void TestEquals()
        {
            Assert.That("A = B", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |A|",
                "  Operator: EqualSign |=|",
                "  Right: Identifier |B|"));
        }
        public void TestTwoEquals()
        {
            Assert.That("A = B = C", ParsesAs(
                "BinaryOperationNode",
                "  Left: BinaryOperationNode",
                "    Left: Identifier |A|",
                "    Operator: EqualSign |=|",
                "    Right: Identifier |B|",
                "  Operator: EqualSign |=|",
                "  Right: Identifier |C|"));
        }
    }
}
