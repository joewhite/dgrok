// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using DGrok.Visitors;
using NUnitLite.Framework;

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

        public void TestSectionWithOneVariable()
        {
            IList<Hit> hits = HitsFor(
                "var",
                "  Foo: Integer;");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("Foo"));
        }
        public void TestSectionWithTwoVariableGroups()
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
        public void TestTypedConstantsDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "const",
                "  Foo: Integer = 42;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestLocalVariablesDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure Foo;",
                "var",
                "  Foo: Integer;",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestClassVariablesDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "type",
                "  TFoo = class",
                "  var",
                "    Foo: Integer;",
                "  end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestRecordVariablesDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "type",
                "  TFoo = record",
                "  var",
                "    Foo: Integer;",
                "  end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestVarParametersDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure Foo(var X: Integer);",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
    }
}
