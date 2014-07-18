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
                "  OperatorNode: NotKeyword |not|",
                "  OperandNode: Number |42|"));
        }
        public void TestMultiply()
        {
            Assert.That("6 * 9", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |6|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Number |9|"));
        }
        public void TestMultipleMultiply()
        {
            Assert.That("6 * 3 * 3", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Number |6|",
                "    OperatorNode: TimesSign |*|",
                "    RightNode: Number |3|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Number |3|"));
        }
        public void TestDivide()
        {
            Assert.That("84 / 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |84|",
                "  OperatorNode: DivideBySign |/|",
                "  RightNode: Number |2|"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute * Index", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Absolute|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Identifier |Index|"));
        }
    }
}
