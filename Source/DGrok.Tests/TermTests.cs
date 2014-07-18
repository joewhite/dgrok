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
    public class TermTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Term; }
        }

        public void TestNumber()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestFactor()
        {
            Assert.That("not 42", ParsesAs(
                "UnaryOperationNode",
                "  Operator: NotKeyword |not|",
                "  Operand: Number |42|"));
        }
        public void TestMultiply()
        {
            Assert.That("6 * 9", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |6|",
                "  Operator: TimesSign |*|",
                "  Right: Number |9|"));
        }
        public void TestMultipleMultiply()
        {
            Assert.That("6 * 3 * 3", ParsesAs(
                "BinaryOperationNode",
                "  Left: BinaryOperationNode",
                "    Left: Number |6|",
                "    Operator: TimesSign |*|",
                "    Right: Number |3|",
                "  Operator: TimesSign |*|",
                "  Right: Number |3|"));
        }
        public void TestDivide()
        {
            Assert.That("84 / 2", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |84|",
                "  Operator: DivideBySign |/|",
                "  Right: Number |2|"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute * Index", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |Absolute|",
                "  Operator: TimesSign |*|",
                "  Right: Identifier |Index|"));
        }
    }
}
