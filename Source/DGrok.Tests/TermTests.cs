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
    public class TermTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Term; }
        }

        [Test]
        public void Number()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void Factor()
        {
            Assert.That("not 42", ParsesAs(
                "UnaryOperationNode",
                "  OperatorNode: NotKeyword |not|",
                "  OperandNode: Number |42|"));
        }
        [Test]
        public void Multiply()
        {
            Assert.That("6 * 9", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |6|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Number |9|"));
        }
        [Test]
        public void MultipleMultiply()
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
        [Test]
        public void Divide()
        {
            Assert.That("84 / 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |84|",
                "  OperatorNode: DivideBySign |/|",
                "  RightNode: Number |2|"));
        }
        [Test]
        public void Semikeywords()
        {
            Assert.That("Absolute * Index", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Absolute|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Identifier |Index|"));
        }
    }
}
