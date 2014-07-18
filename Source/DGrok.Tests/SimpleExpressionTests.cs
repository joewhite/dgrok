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
    public class SimpleExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SimpleExpression; }
        }

        [Test]
        public void Number()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void Term()
        {
            Assert.That("6 * 9", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |6|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Number |9|"));
        }
        [Test]
        public void Add()
        {
            Assert.That("40 + 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |40|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Number |2|"));
        }
        [Test]
        public void TwoAdds()
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
        [Test]
        public void Minus()
        {
            Assert.That("50 - 8", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |50|",
                "  OperatorNode: MinusSign |-|",
                "  RightNode: Number |8|"));
        }
        [Test]
        public void OrderOfOperations()
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
        [Test]
        public void Semikeywords()
        {
            Assert.That("Absolute + Index", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Absolute|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Identifier |Index|"));
        }
    }
}
