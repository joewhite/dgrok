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
    public class GoalTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Goal; }
        }

        [Test]
        public void Package()
        {
            Assert.That("package Foo; end.", ParsesAs(
                "PackageNode",
                "  PackageKeywordNode: PackageSemikeyword |package|",
                "  NameNode: Identifier |Foo|",
                "  SemicolonNode: Semicolon |;|",
                "  RequiresClauseNode: (none)",
                "  ContainsClauseNode: (none)",
                "  AttributeListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        [Test]
        public void Unit()
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
        public void Program()
        {
            Assert.That("program Foo; end.", ParsesAs(
                "ProgramNode",
                "  ProgramKeywordNode: ProgramKeyword |program|",
                "  NameNode: Identifier |Foo|",
                "  NoiseOpenParenthesisNode: (none)",
                "  NoiseContentListNode: ListNode",
                "  NoiseCloseParenthesisNode: (none)",
                "  SemicolonNode: Semicolon |;|",
                "  UsesClauseNode: (none)",
                "  DeclarationListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: (none)",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        [Test]
        public void Library()
        {
            Assert.That("library Foo; end.", ParsesAs(
                "ProgramNode",
                "  ProgramKeywordNode: LibraryKeyword |library|",
                "  NameNode: Identifier |Foo|",
                "  NoiseOpenParenthesisNode: (none)",
                "  NoiseContentListNode: ListNode",
                "  NoiseCloseParenthesisNode: (none)",
                "  SemicolonNode: Semicolon |;|",
                "  UsesClauseNode: (none)",
                "  DeclarationListNode: ListNode",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: (none)",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
    }
}
