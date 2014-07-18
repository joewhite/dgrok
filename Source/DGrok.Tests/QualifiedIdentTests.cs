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
    public class QualifiedIdentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.QualifiedIdent; }
        }

        [Test]
        public void Identifier()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        [Test]
        public void Semikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
        [Test]
        public void KeywordDoesNotParse()
        {
            AssertDoesNotParse("Class");
        }
        [Test]
        public void AmpersandKeyword()
        {
            Assert.That("&Class", ParsesAs("Identifier |&Class|"));
        }
        [Test]
        public void Dots()
        {
            Assert.That("Foo.Bar.Baz", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Baz|"));
        }
        [Test]
        public void DotKeyword()
        {
            Assert.That("Should.Not", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Should|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Not|"));
        }
    }
}
