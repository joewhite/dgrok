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
        public void TestPortabilityDirectives()
        {
            Assert.That("unit Foo library deprecated; interface implementation end.", ParsesAs(
                "UnitNode",
                "  Unit: UnitKeyword |unit|",
                "  UnitName: Identifier |Foo|",
                "  PortabilityDirectives: ListNode",
                "    Items[0]: LibraryKeyword |library|",
                "    Items[1]: DeprecatedSemikeyword |deprecated|",
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
        public void TestInitialization()
        {
            Assert.That("unit Foo; interface implementation initialization end.", ParsesAs(
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
                "    InitializationHeader: InitializationKeyword |initialization|",
                "    InitializationStatements: ListNode",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestBegin()
        {
            Assert.That("unit Foo; interface implementation begin end.", ParsesAs(
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
                "    InitializationHeader: BeginKeyword |begin|",
                "    InitializationStatements: ListNode",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestAsm()
        {
            Assert.That("unit Foo; interface implementation asm end.", ParsesAs(
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
                "      Items[0]: DelimitedItemNode",
                "        Item: AssemblerStatementNode",
                "          Asm: AsmKeyword |asm|",
                "          End: EndKeyword |end|",
                "        Delimiter: (none)",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: (none)",
                "  Dot: Dot |.|"));
        }
    }
}
