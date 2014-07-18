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
    public class FindVariantRecordsTests : CodeBaseActionTestCase
    {
        protected override ICodeBaseAction CreateAction()
        {
            return new FindVariantRecords();
        }

        public void TestNoHits()
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
        public void TestType()
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
        public void TestConstant()
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
        public void TestField()
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
        public void TestVar()
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
