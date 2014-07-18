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
    public class FindGlobalVariablesTests : CodeBaseActionTestCase
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
            return new FindGlobalVariables();
        }

        [Test]
        public void SectionWithOneVariable()
        {
            IList<Hit> hits = HitsFor(
                "var",
                "  Foo: Integer;");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("Foo"));
        }
        [Test]
        public void SectionWithTwoVariableGroups()
        {
            IList<Hit> hits = HitsFor(
                "var",
                "  Foo, Bar: Integer;",
                "  Baz, Quux: Boolean;");
            Assert.That(hits.Count, Is.EqualTo(4));
            Assert.That(hits[0].Description, Is.EqualTo("Foo"));
            Assert.That(hits[1].Description, Is.EqualTo("Bar"));
            Assert.That(hits[2].Description, Is.EqualTo("Baz"));
            Assert.That(hits[3].Description, Is.EqualTo("Quux"));
        }
        [Test]
        public void TypedConstantsDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "const",
                "  Foo: Integer = 42;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void LocalVariablesDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure Foo;",
                "var",
                "  Foo: Integer;",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void ClassVariablesDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "type",
                "  TFoo = class",
                "  var",
                "    Foo: Integer;",
                "  end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void RecordVariablesDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "type",
                "  TFoo = record",
                "  var",
                "    Foo: Integer;",
                "  end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void VarParametersDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure Foo(var X: Integer);",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
    }
}
