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

        public void TestTopLevelMethodsDoNotHit()
        {
            IList<Hit> hits = HitsFor(
                "procedure TFoo.Bar;",
                "begin",
                "end;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestNestedMethodsDoHit()
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
