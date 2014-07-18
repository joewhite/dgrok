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
    public class FindVariantRecordsTests : CodeBaseActionTestCase
    {
        protected override ICodeBaseAction CreateAction()
        {
            return new FindVariantRecords();
        }

        [Test]
        public void NoHits()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "type",
                "  TFoo = record",
                "  end;",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void Type()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "type",
                "  TBar = record",
                "    A: Integer;",
                "    case B: Integer of",
                "      0: ()",
                "  end;",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("TBar = record ... case B: Integer of"));
        }
        [Test]
        public void Constant()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "const",
                "  Bar: record",
                "    A: Integer;",
                "    case B: Integer of",
                "      0: ()",
                "  end = ();",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("Bar: record ... case B: Integer of"));
        }
        [Test]
        public void Field()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "type",
                "  TBar = class",
                "  strict private",
                "    FBaz: record",
                "      A: Integer;",
                "      case B: Integer of",
                "        0: ()",
                "    end;",
                "  end;",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("FBaz: record ... case B: Integer of"));
        }
        [Test]
        public void Var()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "var",
                "  Bar: record",
                "    A: Integer;",
                "    case B: Integer of",
                "      0: ()",
                "  end;",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("Bar: record ... case B: Integer of"));
        }
    }
}
