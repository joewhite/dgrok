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
