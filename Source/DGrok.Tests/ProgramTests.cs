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
    public class ProgramTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Program; }
        }

        public void TestSimpleProgram()
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
        public void TestSimpleLibrary()
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
        public void TestNoiseAfterProgramName()
        {
            Assert.That("program Foo(Input, Output); end.", ParsesAs(
                "ProgramNode",
                "  Program: ProgramKeyword |program|",
                "  Name: Identifier |Foo|",
                "  NoiseOpenParenthesis: OpenParenthesis |(|",
                "  NoiseContents: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Input|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Output|",
                "      Delimiter: (none)",
                "  NoiseCloseParenthesis: CloseParenthesis |)|",
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
        public void TestUses()
        {
            Assert.That("program Foo; uses SysUtils; end.", ParsesAs(
                "ProgramNode",
                "  Program: ProgramKeyword |program|",
                "  Name: Identifier |Foo|",
                "  NoiseOpenParenthesis: (none)",
                "  NoiseContents: ListNode",
                "  NoiseCloseParenthesis: (none)",
                "  Semicolon: Semicolon |;|",
                "  UsesClause: UsesClauseNode",
                "    Uses: UsesKeyword |uses|",
                "    UnitList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: UsedUnitNode",
                "          Name: Identifier |SysUtils|",
                "          In: (none)",
                "          FileName: (none)",
                "        Delimiter: (none)",
                "    Semicolon: Semicolon |;|",
                "  DeclarationList: ListNode",
                "  InitSection: InitSectionNode",
                "    InitializationHeader: (none)",
                "    InitializationStatements: ListNode",
                "    FinalizationHeader: (none)",
                "    FinalizationStatements: ListNode",
                "    End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestDeclaration()
        {
            Assert.That("program Foo; const Foo = 42; end.", ParsesAs(
                "ProgramNode",
                "  Program: ProgramKeyword |program|",
                "  Name: Identifier |Foo|",
                "  NoiseOpenParenthesis: (none)",
                "  NoiseContents: ListNode",
                "  NoiseCloseParenthesis: (none)",
                "  Semicolon: Semicolon |;|",
                "  UsesClause: (none)",
                "  DeclarationList: ListNode",
                "    Items[0]: ConstSectionNode",
                "      Const: ConstKeyword |const|",
                "      ConstList: ListNode",
                "        Items[0]: ConstantDeclNode",
                "          Name: Identifier |Foo|",
                "          Colon: (none)",
                "          Type: (none)",
                "          EqualSign: EqualSign |=|",
                "          Value: Number |42|",
                "          PortabilityDirectiveList: ListNode",
                "          Semicolon: Semicolon |;|",
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
