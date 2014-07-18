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
    public class ParameterExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ParameterExpression; }
        }

        public void TestValue()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestEquals()
        {
            Assert.That("42 = 42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |42|",
                "  OperatorNode: EqualSign |=|",
                "  RightNode: Number |42|"));
        }
        public void TestColon()
        {
            Assert.That("42:4", ParsesAs(
                "NumberFormatNode",
                "  ValueNode: Number |42|",
                "  SizeColonNode: Colon |:|",
                "  SizeNode: Number |4|",
                "  PrecisionColonNode: (none)",
                "  PrecisionNode: (none)"));
        }
        public void TestColonColon()
        {
            Assert.That("42.0:4:2", ParsesAs(
                "NumberFormatNode",
                "  ValueNode: Number |42.0|",
                "  SizeColonNode: Colon |:|",
                "  SizeNode: Number |4|",
                "  PrecisionColonNode: Colon |:|",
                "  PrecisionNode: Number |2|"));
        }
    }
}
