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
