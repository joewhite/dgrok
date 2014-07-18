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
                "  InitializationStatements: ListNode",
                "  FinalizationHeader: (none)",
                "  FinalizationStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyInitialization()
        {
            Assert.That("initialization end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: InitializationKeyword |initialization|",
                "  InitializationStatements: ListNode",
                "  FinalizationHeader: (none)",
                "  FinalizationStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyInitializationFinalization()
        {
            Assert.That("initialization finalization end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: InitializationKeyword |initialization|",
                "  InitializationStatements: ListNode",
                "  FinalizationHeader: FinalizationKeyword |finalization|",
                "  FinalizationStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPopulatedInitialization()
        {
            Assert.That("initialization Foo; Bar; end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: InitializationKeyword |initialization|",
                "  InitializationStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  FinalizationHeader: (none)",
                "  FinalizationStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPopulatedInitializationFinalization()
        {
            Assert.That("initialization Foo; Bar; finalization Baz; Quux; end", ParsesAs(
                "InitSectionNode",
                "  InitializationHeader: InitializationKeyword |initialization|",
                "  InitializationStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  FinalizationHeader: FinalizationKeyword |finalization|",
                "  FinalizationStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Baz|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Quux|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyBlock()
        {
            Assert.That("begin end", ParsesAs(
                "BlockNode",
                "  Begin: BeginKeyword |begin|",
                "  StatementList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPopulatedBlock()
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
