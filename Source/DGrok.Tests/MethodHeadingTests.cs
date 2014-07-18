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
    public class MethodHeadingTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodHeading; }
        }

        [Test]
        public void Procedure()
        {
            Assert.That("procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void FunctionWithoutReturnType()
        {
            Assert.That("function Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: FunctionKeyword |function|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Constructor()
        {
            Assert.That("constructor Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ConstructorKeyword |constructor|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Destructor()
        {
            Assert.That("destructor Foo; override;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: DestructorKeyword |destructor|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: OverrideSemikeyword |override|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void MultipleDirectives()
        {
            Assert.That("procedure Foo; virtual; abstract; deprecated;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: VirtualSemikeyword |virtual|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "    Items[1]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: AbstractSemikeyword |abstract|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "    Items[2]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: DeprecatedSemikeyword |deprecated|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void DirectiveWithoutTrailingSemicolon()
        {
            Assert.That("procedure Foo; deprecated", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: DeprecatedSemikeyword |deprecated|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void QualifiedName()
        {
            Assert.That("procedure TFoo.Bar;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: BinaryOperationNode",
                "    LeftNode: Identifier |TFoo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void EmptyParameterList()
        {
            Assert.That("procedure Foo();", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Parameters()
        {
            Assert.That("procedure Foo(Sender: TObject; var CanClose: Boolean);", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ParameterNode",
                "        ModifierNode: (none)",
                "        NameListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Identifier |Sender|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        TypeNode: Identifier |TObject|",
                "        EqualSignNode: (none)",
                "        DefaultValueNode: (none)",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: ParameterNode",
                "        ModifierNode: VarKeyword |var|",
                "        NameListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Identifier |CanClose|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        TypeNode: Identifier |Boolean|",
                "        EqualSignNode: (none)",
                "        DefaultValueNode: (none)",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ReturnType()
        {
            Assert.That("function Foo: Boolean;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: FunctionKeyword |function|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: Colon |:|",
                "  ReturnTypeNode: Identifier |Boolean|",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassProcedure()
        {
            Assert.That("class procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassFunction()
        {
            Assert.That("class function Foo: Integer;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  MethodTypeNode: FunctionKeyword |function|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: Colon |:|",
                "  ReturnTypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void MethodResolution()
        {
            Assert.That("procedure IFoo.Bar = Baz;", ParsesAs(
                "MethodResolutionNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  InterfaceMethodNode: BinaryOperationNode",
                "    LeftNode: Identifier |IFoo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  EqualSignNode: EqualSign |=|",
                "  ImplementationMethodNode: Identifier |Baz|",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Operator()
        {
            Assert.That("class operator Implicit(Value: Integer): TValue;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  MethodTypeNode: OperatorSemikeyword |operator|",
                "  NameNode: Identifier |Implicit|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ParameterNode",
                "        ModifierNode: (none)",
                "        NameListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Identifier |Value|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        TypeNode: Identifier |Integer|",
                "        EqualSignNode: (none)",
                "        DefaultValueNode: (none)",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ColonNode: Colon |:|",
                "  ReturnTypeNode: Identifier |TValue|",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
