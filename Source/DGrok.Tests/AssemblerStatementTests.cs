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
    public class AssemblerStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.AssemblerStatement; }
        }

        public void TestErrorIfNoEnd()
        {
            AssertDoesNotParse("asm");
        }
        public void TestEmptyAsmBlock()
        {
            Assert.That("asm end", ParsesAs(
                "AssemblerStatementNode",
                "  AsmKeywordNode: AsmKeyword |asm|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestNonEmptyAsmBlock()
        {
            Assert.That("asm INT 3 end", ParsesAs(
                "AssemblerStatementNode",
                "  AsmKeywordNode: AsmKeyword |asm|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
    }
}
