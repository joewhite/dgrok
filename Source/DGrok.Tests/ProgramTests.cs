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
    public class ProgramTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Program; }
        }

        [Test]
        public void SimpleProgram()
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
        public void SimpleLibrary()
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
        [Test]
        public void NoiseAfterProgramName()
        {
            Assert.That("program Foo(Input, Output); end.", ParsesAs(
                "ProgramNode",
                "  ProgramKeywordNode: ProgramKeyword |program|",
                "  NameNode: Identifier |Foo|",
                "  NoiseOpenParenthesisNode: OpenParenthesis |(|",
                "  NoiseContentListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Input|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Output|",
                "      DelimiterNode: (none)",
                "  NoiseCloseParenthesisNode: CloseParenthesis |)|",
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
        public void Uses()
        {
            Assert.That("program Foo; uses SysUtils; end.", ParsesAs(
                "ProgramNode",
                "  ProgramKeywordNode: ProgramKeyword |program|",
                "  NameNode: Identifier |Foo|",
                "  NoiseOpenParenthesisNode: (none)",
                "  NoiseContentListNode: ListNode",
                "  NoiseCloseParenthesisNode: (none)",
                "  SemicolonNode: Semicolon |;|",
                "  UsesClauseNode: UsesClauseNode",
                "    UsesKeywordNode: UsesKeyword |uses|",
                "    UnitListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: UsedUnitNode",
                "          NameNode: Identifier |SysUtils|",
                "          InKeywordNode: (none)",
                "          FileNameNode: (none)",
                "        DelimiterNode: (none)",
                "    SemicolonNode: Semicolon |;|",
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
        public void Declaration()
        {
            Assert.That("program Foo; const Foo = 42; end.", ParsesAs(
                "ProgramNode",
                "  ProgramKeywordNode: ProgramKeyword |program|",
                "  NameNode: Identifier |Foo|",
                "  NoiseOpenParenthesisNode: (none)",
                "  NoiseContentListNode: ListNode",
                "  NoiseCloseParenthesisNode: (none)",
                "  SemicolonNode: Semicolon |;|",
                "  UsesClauseNode: (none)",
                "  DeclarationListNode: ListNode",
                "    Items[0]: ConstSectionNode",
                "      ConstKeywordNode: ConstKeyword |const|",
                "      ConstListNode: ListNode",
                "        Items[0]: ConstantDeclNode",
                "          NameNode: Identifier |Foo|",
                "          ColonNode: (none)",
                "          TypeNode: (none)",
                "          EqualSignNode: EqualSign |=|",
                "          ValueNode: Number |42|",
                "          PortabilityDirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|",
                "  InitSectionNode: InitSectionNode",
                "    InitializationKeywordNode: (none)",
                "    InitializationStatementListNode: ListNode",
                "    FinalizationKeywordNode: (none)",
                "    FinalizationStatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        [Test]
        public void AssemblyAttribute()
        {
            Assert.That("program Foo; [assembly: AssemblyVersion('0.0.0.0')] end.", ParsesAs(
                "ProgramNode",
                "  ProgramKeywordNode: ProgramKeyword |program|",
                "  NameNode: Identifier |Foo|",
                "  NoiseOpenParenthesisNode: (none)",
                "  NoiseContentListNode: ListNode",
                "  NoiseCloseParenthesisNode: (none)",
                "  SemicolonNode: Semicolon |;|",
                "  UsesClauseNode: (none)",
                "  DeclarationListNode: ListNode",
                "    Items[0]: AttributeNode",
                "      OpenBracketNode: OpenBracket |[|",
                "      ScopeNode: AssemblySemikeyword |assembly|",
                "      ColonNode: Colon |:|",
                "      ValueNode: ParameterizedNode",
                "        LeftNode: Identifier |AssemblyVersion|",
                "        OpenDelimiterNode: OpenParenthesis |(|",
                "        ParameterListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: StringLiteral |'0.0.0.0'|",
                "            DelimiterNode: (none)",
                "        CloseDelimiterNode: CloseParenthesis |)|",
                "      CloseBracketNode: CloseBracket |]|",
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
            Assert.That("program Foo; initialization end.", ParsesAs(
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
            Assert.That("program Foo; begin end.", ParsesAs(
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
            Assert.That("program Foo; asm end.", ParsesAs(
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
