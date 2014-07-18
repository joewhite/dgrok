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
    public class InitSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.InitSection; }
        }

        public void TestEnd()
        {
            Assert.That("end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: (none)",
                "  InitializationStatements: (none)",
                "  FinalizationHeader: (none)",
                "  FinalizationStatements: (none)",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyInitialization()
        {
            Assert.That("initialization end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: InitializationKeyword |initialization|",
                "  InitializationStatements: (none)",
                "  FinalizationHeader: (none)",
                "  FinalizationStatements: (none)",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyInitializationFinalization()
        {
            Assert.That("initialization finalization end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: InitializationKeyword |initialization|",
                "  InitializationStatements: (none)",
                "  FinalizationHeader: FinalizationKeyword |finalization|",
                "  FinalizationStatements: (none)",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyBegin()
        {
            Assert.That("begin end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: BeginKeyword |begin|",
                "  InitializationStatements: (none)",
                "  FinalizationHeader: (none)",
                "  FinalizationStatements: (none)",
                "  End: EndKeyword |end|"));
        }
    }
}
