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
    public class ImplementationDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ImplementationDecl; }
        }

        public void TestLabelSection()
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
        public void TestConstSection()
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
        public void TestTypeSection()
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
        public void TestVarSection()
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
        public void TestMethodImplementation()
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
        public void TestExportsStatement()
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
        public void TestAssemblyAttribute()
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
