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
                "  Strict: (none)",
                "  Visibility: PrivateSemikeyword |private|"));
        }
        public void TestProtected()
        {
            Assert.That("protected", ParsesAs(
                "VisibilityNode",
                "  Strict: (none)",
                "  Visibility: ProtectedSemikeyword |protected|"));
        }
        public void TestPublic()
        {
            Assert.That("public", ParsesAs(
                "VisibilityNode",
                "  Strict: (none)",
                "  Visibility: PublicSemikeyword |public|"));
        }
        public void TestPublished()
        {
            Assert.That("published", ParsesAs(
                "VisibilityNode",
                "  Strict: (none)",
                "  Visibility: PublishedSemikeyword |published|"));
        }
        public void TestStrictPrivate()
        {
            Assert.That("strict private", ParsesAs(
                "VisibilityNode",
                "  Strict: StrictSemikeyword |strict|",
                "  Visibility: PrivateSemikeyword |private|"));
        }
        public void TestStrictProtected()
        {
            Assert.That("strict protected", ParsesAs(
                "VisibilityNode",
                "  Strict: StrictSemikeyword |strict|",
                "  Visibility: ProtectedSemikeyword |protected|"));
        }
    }
}
