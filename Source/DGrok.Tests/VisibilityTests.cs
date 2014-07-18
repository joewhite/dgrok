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
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class VisibilityTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Visibility; }
        }

        [Test]
        public void Private()
        {
            Assert.That("private", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: PrivateSemikeyword |private|"));
        }
        [Test]
        public void Protected()
        {
            Assert.That("protected", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: ProtectedSemikeyword |protected|"));
        }
        [Test]
        public void Public()
        {
            Assert.That("public", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: PublicSemikeyword |public|"));
        }
        [Test]
        public void Published()
        {
            Assert.That("published", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: PublishedSemikeyword |published|"));
        }
        [Test]
        public void StrictPrivate()
        {
            Assert.That("strict private", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: StrictSemikeyword |strict|",
                "  VisibilityKeywordNode: PrivateSemikeyword |private|"));
        }
        [Test]
        public void StrictProtected()
        {
            Assert.That("strict protected", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: StrictSemikeyword |strict|",
                "  VisibilityKeywordNode: ProtectedSemikeyword |protected|"));
        }
    }
}
