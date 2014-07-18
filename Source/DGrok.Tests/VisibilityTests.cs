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
using NUnitLite.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class VisibilityTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Visibility; }
        }

        public void TestPrivate()
        {
            Assert.That("private", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: PrivateSemikeyword |private|"));
        }
        public void TestProtected()
        {
            Assert.That("protected", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: ProtectedSemikeyword |protected|"));
        }
        public void TestPublic()
        {
            Assert.That("public", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: PublicSemikeyword |public|"));
        }
        public void TestPublished()
        {
            Assert.That("published", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: (none)",
                "  VisibilityKeywordNode: PublishedSemikeyword |published|"));
        }
        public void TestStrictPrivate()
        {
            Assert.That("strict private", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: StrictSemikeyword |strict|",
                "  VisibilityKeywordNode: PrivateSemikeyword |private|"));
        }
        public void TestStrictProtected()
        {
            Assert.That("strict protected", ParsesAs(
                "VisibilityNode",
                "  StrictSemikeywordNode: StrictSemikeyword |strict|",
                "  VisibilityKeywordNode: ProtectedSemikeyword |protected|"));
        }
    }
}
