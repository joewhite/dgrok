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
    public class UnaryOperatorTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.UnaryOperator; }
        }

        [Test]
        public void AndDoesNotParse()
        {
            AssertDoesNotParse("and");
        }
        [Test]
        public void Not()
        {
            Assert.That("not", ParsesAs("NotKeyword |not|"));
        }
        [Test]
        public void PlusSign()
        {
            Assert.That("+", ParsesAs("PlusSign |+|"));
        }
        [Test]
        public void MinusSign()
        {
            Assert.That("-", ParsesAs("MinusSign |-|"));
        }
        [Test]
        public void AtSign()
        {
            Assert.That("@", ParsesAs("AtSign |@|"));
        }
        [Test]
        public void Inherited()
        {
            Assert.That("inherited", ParsesAs("InheritedKeyword |inherited|"));
        }
    }
}
