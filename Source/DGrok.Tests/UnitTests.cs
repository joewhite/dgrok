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
    public class UnitTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Unit; }
        }

        public void TestEmpty()
        {
            Assert.That("unit Foo; interface implementation end.", ParsesAs(
                "UnitNode",
                "  UnitKeywordNode: UnitKeyword |unit|",
                "  UnitNameNode: Identifier |Foo|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|",
                "  InterfaceSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: InterfaceKeyword |interface|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  ImplementationSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: ImplementationKeyword |implementation|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: (none)",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("unit Foo library deprecated; interface implementation end.", ParsesAs(
                "UnitNode",
                "  UnitKeywordNode: UnitKeyword |unit|",
                "  UnitNameNode: Identifier |Foo|",
                "  PortabilityDirectiveListNode: ListNode",
                "    Items[0]: LibraryKeyword |library|",
                "    Items[1]: DeprecatedSemikeyword |deprecated|",
                "  SemicolonNode: Semicolon |;|",
                "  InterfaceSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: InterfaceKeyword |interface|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  ImplementationSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: ImplementationKeyword |implementation|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: (none)",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestInitialization()
        {
            Assert.That("unit Foo; interface implementation initialization end.", ParsesAs(
                "UnitNode",
                "  UnitKeywordNode: UnitKeyword |unit|",
                "  UnitNameNode: Identifier |Foo|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|",
                "  InterfaceSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: InterfaceKeyword |interface|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  ImplementationSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: ImplementationKeyword |implementation|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: InitializationKeyword |initialization|",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestBegin()
        {
            Assert.That("unit Foo; interface implementation begin end.", ParsesAs(
                "UnitNode",
                "  UnitKeywordNode: UnitKeyword |unit|",
                "  UnitNameNode: Identifier |Foo|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|",
                "  InterfaceSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: InterfaceKeyword |interface|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  ImplementationSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: ImplementationKeyword |implementation|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: BeginKeyword |begin|",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestAsm()
        {
            Assert.That("unit Foo; interface implementation asm end.", ParsesAs(
                "UnitNode",
                "  UnitKeywordNode: UnitKeyword |unit|",
                "  UnitNameNode: Identifier |Foo|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|",
                "  InterfaceSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: InterfaceKeyword |interface|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  ImplementationSectionNode: UnitSectionNode",
                "    HeaderKeywordNode: ImplementationKeyword |implementation|",
                "    UsesClauseNode: (none)",
                "    ContentListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: (none)",
                "    InitializationStatementListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: AssemblerStatementNode",
                "          AsmKeywordNode: AsmKeyword |asm|",
                "          EndKeywordNode: EndKeyword |end|",
                "        DelimiterNode: (none)",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: (none)",
                "  DotNode: Dot |.|"));
        }
    }
}
