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

        [Test]
        public void NoWiths()
        {
            IList<Hit> hits = HitsFor("WriteLn;");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void SimpleWith()
        {
            IList<Hit> hits = HitsFor("with Canvas do FillRect(R);");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("with Canvas"));
        }
        [Test]
        public void MultipleExpressions()
        {
            IList<Hit> hits = HitsFor("with PaintBox1, Canvas do FillRect(ClientRect);");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("with PaintBox1, Canvas"));
        }
        [Test]
        public void MultipleWiths()
        {
            IList<Hit> hits = HitsFor("with PaintBox1 do with Canvas do FillRect(ClientRect);");
            Assert.That(hits.Count, Is.EqualTo(2));
            Assert.That(hits[0].Description, Is.EqualTo("with PaintBox1"));
            Assert.That(hits[1].Description, Is.EqualTo("with Canvas"));
        }
    }
}
