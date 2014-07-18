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
    public class FindNestedMethodsTests : CodeBaseActionTestCase
    {
        protected override void AddPrefix(List<string> unit)
        {
            unit.Add("unit Foo;");
            unit.Add("interface");
            unit.Add("implementation");
        }
        protected override void AddSuffix(List<string> unit)
        {
            unit.Add("end.");
        }
        protected override ICodeBaseAction CreateAction()
        {
            return new FindNestedMethods();
        }

        [Test]
        public void TopLevelMethodsDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure TFoo.Bar;",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void NestedMethodsDoHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure TFoo.Bar;",
                "  procedure Baz;",
                "  begin",
                "  end;",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("TFoo.Bar -> Baz"));
        }
    }
}
