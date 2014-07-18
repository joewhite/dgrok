// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
