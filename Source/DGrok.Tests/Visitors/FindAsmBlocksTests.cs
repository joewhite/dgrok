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
    public class FindAsmBlocksTests : CodeBaseActionTestCase
    {
        protected override ICodeBaseAction CreateAction()
        {
            return new FindAsmBlocks();
        }

        [Test]
        public void NoHits()
        {
            IList<Hit> hits = HitsFor(
                "unit Foo;",
                "interface",
                "implementation",
                "end.");
            Assert.That(hits.Count, Is.EqualTo(0));
        }
        [Test]
        public void AsmBlockInMethod()
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
        [Test]
        public void AssemblerMethod()
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
        [Test]
        public void AsmInitializationSection()
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
