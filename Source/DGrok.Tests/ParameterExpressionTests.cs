// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
