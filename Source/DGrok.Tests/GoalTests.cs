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
                "  Package: PackageSemikeyword |package|",
                "  Name: Identifier |Foo|",
                "  Semicolon: Semicolon |;|",
                "  RequiresClause: (none)",
                "  ContainsClause: (none)",
                "  AttributeList: ListNode",
                "  End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestUnit()
        {
            Assert.That("unit Foo; interface implementation end.", ParsesAs(
                "UnitNode",
                "  Unit: UnitKeyword |unit|",
                "  UnitName: Identifier |Foo|",
                "  PortabilityDirectives: ListNode",
                "  Semicolon: Semicolon |;|",
                "  InterfaceSection: UnitSectionNode",
                "    HeaderKeyword: InterfaceKeyword |interface|",
                "    UsesClause: (none)",
                "    Contents: ListNode",
                "  ImplementationSection: UnitSectionNode",
                "    HeaderKeyword: ImplementationKeyword |implementation|",
                "    UsesClause: (none)",
                "    Contents: ListNode",
                "  InitSection: InitSectionNode",
                "    InitializationHeader: (none)",
                "    InitializationStatements: ListNode",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: EndKeyword |end|",
                "  Dot: Dot |.|"));

        }
        public void TestProgram()
        {
            Assert.That("program Foo; end.", ParsesAs(
                "ProgramNode",
                "  Program: ProgramKeyword |program|",
                "  Name: Identifier |Foo|",
                "  NoiseOpenParenthesis: (none)",
                "  NoiseContents: ListNode",
                "  NoiseCloseParenthesis: (none)",
                "  Semicolon: Semicolon |;|",
                "  UsesClause: (none)",
                "  DeclarationList: ListNode",
                "  InitSection: InitSectionNode",
                "    InitializationHeader: (none)",
                "    InitializationStatements: ListNode",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestLibrary()
        {
            Assert.That("library Foo; end.", ParsesAs(
                "ProgramNode",
                "  Program: LibraryKeyword |library|",
                "  Name: Identifier |Foo|",
                "  NoiseOpenParenthesis: (none)",
                "  NoiseContents: ListNode",
                "  NoiseCloseParenthesis: (none)",
                "  Semicolon: Semicolon |;|",
                "  UsesClause: (none)",
                "  DeclarationList: ListNode",
                "  InitSection: InitSectionNode",
                "    InitializationHeader: (none)",
                "    InitializationStatements: ListNode",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
    }
}
