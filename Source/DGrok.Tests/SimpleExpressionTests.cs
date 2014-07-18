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
                "  Left: Number |6|",
                "  Operator: TimesSign |*|",
                "  Right: Number |9|"));
        }
        public void TestAdd()
        {
            Assert.That("40 + 2", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |40|",
                "  Operator: PlusSign |+|",
                "  Right: Number |2|"));
        }
        public void TestTwoAdds()
        {
            Assert.That("30 + 10 + 2", ParsesAs(
                "BinaryOperationNode",
                "  Left: BinaryOperationNode",
                "    Left: Number |30|",
                "    Operator: PlusSign |+|",
                "    Right: Number |10|",
                "  Operator: PlusSign |+|",
                "  Right: Number |2|"));
        }
        public void TestMinus()
        {
            Assert.That("50 - 8", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |50|",
                "  Operator: MinusSign |-|",
                "  Right: Number |8|"));
        }
        public void TestOrderOfOperations()
        {
            Assert.That("1 * 2 + 3 * 4", ParsesAs(
                "BinaryOperationNode",
                "  Left: BinaryOperationNode",
                "    Left: Number |1|",
                "    Operator: TimesSign |*|",
                "    Right: Number |2|",
                "  Operator: PlusSign |+|",
                "  Right: BinaryOperationNode",
                "    Left: Number |3|",
                "    Operator: TimesSign |*|",
                "    Right: Number |4|"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute + Index", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |Absolute|",
                "  Operator: PlusSign |+|",
                "  Right: Identifier |Index|"));
        }
    }
}
