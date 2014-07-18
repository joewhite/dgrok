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
using DGrok.Visitors;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests.Visitors
{
    [TestFixture]
    public class FindAsmBlocksTests : CodeBaseActionTestCase
    {
        protected override ICodeBaseAction CreateAction()
        {
            return new FindAsmBlocks();
        }

        [Test]
        public void NoHits()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void AsmBlockInMethod()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "procedure Foo;",
                "begin",
                "  asm MOV EAX, EAX end;",
                "end;",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("asm MOV EAX, EAX end"));
        }
        [Test]
        public void AssemblerMethod()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "procedure Foo;",
                "asm INT 3 end;",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("asm INT 3 end"));
        }
        [Test]
        public void AsmInitializationSection()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "asm RET end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("asm RET end"));
        }
    }
}
