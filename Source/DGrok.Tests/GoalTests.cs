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
    public class GoalTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Goal; }
        }

        public void TestPackage()
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
        public void TestUnit()
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
        public void TestProgram()
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
        public void TestLibrary()
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
