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
    public class FindAsmBlocksTests : CodeBaseActionTestCase
    {
        protected override ICodeBaseAction CreateAction()
        {
            return new FindAsmBlocks();
        }

        public void TestNoHits()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        public void TestAsmBlockInMethod()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "procedure Foo;",
                "begin",
                "  asm MOV EAX, EAX end;",
                "end;",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("asm MOV EAX, EAX end"));
        }
        public void TestAssemblerMethod()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "procedure Foo;",
                "asm INT 3 end;",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("asm INT 3 end"));
        }
        public void TestAsmInitializationSection()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "asm RET end.");
            Assert.That(hits.Count, Is.EqualTo(1));
            Assert.That(hits[0].Description, Is.EqualTo("asm RET end"));
        }
    }
}
