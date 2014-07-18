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
    public class PortabilityDirectiveTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.PortabilityDirective; }
        }

        public void TestPlatform()
        {
            Assert.That("platform", ParsesAs("PlatformSemikeyword |platform|"));
        }
        public void TestDeprecated()
        {
            Assert.That("deprecated", ParsesAs("DeprecatedSemikeyword |deprecated|"));
        }
        public void TestLibrary()
        {
            Assert.That("library", ParsesAs("LibraryKeyword |library|"));
        }
        public void TestExperimental()
        {
            Assert.That("experimental", ParsesAs("ExperimentalSemikeyword |experimental|"));
        }
    }
}
