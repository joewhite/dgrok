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
    public class FindWithStatementTests : CodeBaseActionTestCase
    {
        protected override void AddPrefix(List<string> unit)
        {
            unit.Add("unit Foo;");
            unit.Add("interface");
            unit.Add("implementation");
            unit.Add("begin");
        }
        protected override void AddSuffix(List<string> unit)
        {
            unit.Add("end.");
        }
        protected override ICodeBaseAction CreateAction()
        {
            return new FindWithStatements();
        }

        public void TestNoWiths()
        {
            IList<Hit> hits = HitsFor("WriteLn;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestSimpleWith()
        {
            IList<Hit> hits = HitsFor("with Canvas do FillRect(R);");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("with Canvas"));
        }
        public void TestMultipleExpressions()
        {
            IList<Hit> hits = HitsFor("with PaintBox1, Canvas do FillRect(ClientRect);");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("with PaintBox1, Canvas"));
        }
        public void TestMultipleWiths()
        {
            IList<Hit> hits = HitsFor("with PaintBox1 do with Canvas do FillRect(ClientRect);");
            Assert.That(hits.Count, Is.EqualTo(2));
            Assert.That(hits[0].Description, Is.EqualTo("with PaintBox1"));
            Assert.That(hits[1].Description, Is.EqualTo("with Canvas"));
        }
    }
}
