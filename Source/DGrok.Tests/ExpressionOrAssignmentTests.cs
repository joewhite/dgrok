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
    public class ExpressionOrAssignmentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExpressionOrAssignment; }
        }

        public void TestExpression()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        public void TestAssignment()
        {
            Assert.That("Foo := 42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Foo|",
                "  OperatorNode: ColonEquals |:=|",
                "  RightNode: Number |42|"));
        }
    }
}
