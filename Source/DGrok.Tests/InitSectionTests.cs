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
                "  InitializationKeywordNode: (none)",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestEmptyInitialization()
        {
            Assert.That("initialization end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestEmptyInitializationFinalization()
        {
            Assert.That("initialization finalization end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: FinalizationKeyword |finalization|",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestPopulatedInitialization()
        {
            Assert.That("initialization Foo; Bar; end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestPopulatedInitializationFinalization()
        {
            Assert.That("initialization Foo; Bar; finalization Baz; Quux; end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinalizationKeywordNode: FinalizationKeyword |finalization|",
                "  FinalizationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Baz|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Quux|",
                "      DelimiterNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestEmptyBlock()
        {
            Assert.That("begin end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: BeginKeyword |begin|",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestPopulatedBlock()
        {
            Assert.That("begin Foo; Bar; end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: BeginKeyword |begin|",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestAsmBlock()
        {
            Assert.That("asm end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: (none)",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: AssemblerStatementNode",
                "        AsmKeywordNode: AsmKeyword |asm|",
                "        EndKeywordNode: EndKeyword |end|",
                "      DelimiterNode: (none)",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: (none)"));
        }
    }
}
