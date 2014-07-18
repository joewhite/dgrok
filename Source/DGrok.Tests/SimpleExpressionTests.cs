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
    public class SimpleExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SimpleExpression; }
        }

        public void TestNumber()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestTerm()
        {
            Assert.That("6 * 9", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |6|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Number |9|"));
        }
        public void TestAdd()
        {
            Assert.That("40 + 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |40|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Number |2|"));
        }
        public void TestTwoAdds()
        {
            Assert.That("30 + 10 + 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Number |30|",
                "    OperatorNode: PlusSign |+|",
                "    RightNode: Number |10|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Number |2|"));
        }
        public void TestMinus()
        {
            Assert.That("50 - 8", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |50|",
                "  OperatorNode: MinusSign |-|",
                "  RightNode: Number |8|"));
        }
        public void TestOrderOfOperations()
        {
            Assert.That("1 * 2 + 3 * 4", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Number |1|",
                "    OperatorNode: TimesSign |*|",
                "    RightNode: Number |2|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: BinaryOperationNode",
                "    LeftNode: Number |3|",
                "    OperatorNode: TimesSign |*|",
                "    RightNode: Number |4|"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute + Index", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Absolute|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Identifier |Index|"));
        }
    }
}
