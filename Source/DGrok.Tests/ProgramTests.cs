// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
