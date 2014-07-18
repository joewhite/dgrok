// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class UnitTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Unit; }
        }

        [Test]
        public void Empty()
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
        [Test]
        public void PortabilityDirectives()
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
        [Test]
        public void Initialization()
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
        [Test]
        public void Begin()
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
        [Test]
        public void Asm()
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
