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
    public class BlockTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Block; }
        }

        public void TestEmptyBlock()
        {
            Assert.That("begin end", ParsesAs(
                "BlockNode",
                "  Begin: BeginKeyword |begin|",
                "  StatementList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestStatements()
        {
            Assert.That("begin Foo; Bar; end", ParsesAs(
                "BlockNode",
                "  Begin: BeginKeyword |begin|",
                "  StatementList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
        public void TestAsmBlock()
        {
            Assert.That("asm end", ParsesAs(
                "AssemblerStatementNode",
                "  Asm: AsmKeyword |asm|",
                "  End: EndKeyword |end|"));
        }
    }
}
