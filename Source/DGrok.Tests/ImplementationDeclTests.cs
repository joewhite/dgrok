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
    public class ImplementationDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ImplementationDecl; }
        }

        [Test]
        public void LabelSection()
        {
            Assert.That("label 42;", ParsesAs(
                "LabelDeclSectionNode",
                "  LabelKeywordNode: LabelKeyword |label|",
                "  LabelListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ConstSection()
        {
            Assert.That("const Foo = 24; Bar = 42;", ParsesAs(
                "ConstSectionNode",
                "  ConstKeywordNode: ConstKeyword |const|",
                "  ConstListNode: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      NameNode: Identifier |Foo|",
                "      ColonNode: (none)",
                "      TypeNode: (none)",
                "      EqualSignNode: EqualSign |=|",
                "      ValueNode: Number |24|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: ConstantDeclNode",
                "      NameNode: Identifier |Bar|",
                "      ColonNode: (none)",
                "      TypeNode: (none)",
                "      EqualSignNode: EqualSign |=|",
                "      ValueNode: Number |42|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void TypeSection()
        {
            Assert.That("type TFoo = Integer; TBar = Byte;", ParsesAs(
                "TypeSectionNode",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeListNode: ListNode",
                "    Items[0]: TypeDeclNode",
                "      NameNode: Identifier |TFoo|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: TypeDeclNode",
                "      NameNode: Identifier |TBar|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Byte|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void VarSection()
        {
            Assert.That("var Foo: Integer;", ParsesAs(
                "VarSectionNode",
                "  VarKeywordNode: VarKeyword |var|",
                "  VarListNode: ListNode",
                "    Items[0]: VarDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      FirstPortabilityDirectiveListNode: ListNode",
                "      AbsoluteSemikeywordNode: (none)",
                "      AbsoluteAddressNode: (none)",
                "      EqualSignNode: (none)",
                "      ValueNode: (none)",
                "      SecondPortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void MethodImplementation()
        {
            Assert.That("procedure Foo; begin end;", ParsesAs(
                "MethodImplementationNode",
                "  MethodHeadingNode: MethodHeadingNode",
                "    ClassKeywordNode: (none)",
                "    MethodTypeNode: ProcedureKeyword |procedure|",
                "    NameNode: Identifier |Foo|",
                "    OpenParenthesisNode: (none)",
                "    ParameterListNode: ListNode",
                "    CloseParenthesisNode: (none)",
                "    ColonNode: (none)",
                "    ReturnTypeNode: (none)",
                "    DirectiveListNode: ListNode",
                "    SemicolonNode: Semicolon |;|",
                "  FancyBlockNode: FancyBlockNode",
                "    DeclListNode: ListNode",
                "    BlockNode: BlockNode",
                "      BeginKeywordNode: BeginKeyword |begin|",
                "      StatementListNode: ListNode",
                "      EndKeywordNode: EndKeyword |end|",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ExportsStatement()
        {
            Assert.That("exports Foo;", ParsesAs(
                "ExportsStatementNode",
                "  ExportsKeywordNode: ExportsKeyword |exports|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ExportsItemNode",
                "        NameNode: Identifier |Foo|",
                "        SpecifierListNode: ListNode",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void AssemblyAttribute()
        {
            Assert.That("[assembly: AssemblyVersion('0.0.0.0')]", ParsesAs(
                "AttributeNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ScopeNode: AssemblySemikeyword |assembly|",
                "  ColonNode: Colon |:|",
                "  ValueNode: ParameterizedNode",
                "    LeftNode: Identifier |AssemblyVersion|",
                "    OpenDelimiterNode: OpenParenthesis |(|",
                "    ParameterListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: StringLiteral |'0.0.0.0'|",
                "        DelimiterNode: (none)",
                "    CloseDelimiterNode: CloseParenthesis |)|",
                "  CloseBracketNode: CloseBracket |]|"));
        }
    }
}
