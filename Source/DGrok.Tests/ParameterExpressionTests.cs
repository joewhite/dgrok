// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class ParameterExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ParameterExpression; }
        }

        [Test]
        public void Value()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void Equals()
        {
            Assert.That("42 = 42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |42|",
                "  OperatorNode: EqualSign |=|",
                "  RightNode: Number |42|"));
        }
        [Test]
        public void Colon()
        {
            Assert.That("42:4", ParsesAs(
                "NumberFormatNode",
                "  ValueNode: Number |42|",
                "  SizeColonNode: Colon |:|",
                "  SizeNode: Number |4|",
                "  PrecisionColonNode: (none)",
                "  PrecisionNode: (none)"));
        }
        [Test]
        public void ColonColon()
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
