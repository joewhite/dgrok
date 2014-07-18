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
