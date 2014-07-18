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
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class ProcedureTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ProcedureType; }
        }

        [Test]
        public void Procedure()
        {
            Assert.That("procedure", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  FirstDirectiveListNode: ListNode",
                "  OfKeywordNode: (none)",
                "  ObjectKeywordNode: (none)",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void Function()
        {
            Assert.That("function: Integer", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: FunctionKeyword |function|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: Colon |:|",
                "  ReturnTypeNode: Identifier |Integer|",
                "  FirstDirectiveListNode: ListNode",
                "  OfKeywordNode: (none)",
                "  ObjectKeywordNode: (none)",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void EmptyParameterList()
        {
            Assert.That("procedure()", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  FirstDirectiveListNode: ListNode",
                "  OfKeywordNode: (none)",
                "  ObjectKeywordNode: (none)",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void Parameters()
        {
            Assert.That("procedure(Sender: TObject; var CanClose: Boolean)", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
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
                "  FirstDirectiveListNode: ListNode",
                "  OfKeywordNode: (none)",
                "  ObjectKeywordNode: (none)",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void OfObject()
        {
            Assert.That("procedure of object", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  FirstDirectiveListNode: ListNode",
                "  OfKeywordNode: OfKeyword |of|",
                "  ObjectKeywordNode: ObjectKeyword |object|",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void Directive()
        {
            Assert.That("procedure stdcall", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  FirstDirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: StdcallSemikeyword |stdcall|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  OfKeywordNode: (none)",
                "  ObjectKeywordNode: (none)",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void DirectiveBeforeOfObject()
        {
            Assert.That("procedure stdcall of object", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  FirstDirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: StdcallSemikeyword |stdcall|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  OfKeywordNode: OfKeyword |of|",
                "  ObjectKeywordNode: ObjectKeyword |object|",
                "  SecondDirectiveListNode: ListNode"));
        }
        [Test]
        public void DirectiveAfterOfObjectWithSemicolon()
        {
            Assert.That("procedure of object; stdcall", ParsesAs(
                "ProcedureTypeNode",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  FirstDirectiveListNode: ListNode",
                "  OfKeywordNode: OfKeyword |of|",
                "  ObjectKeywordNode: ObjectKeyword |object|",
                "  SecondDirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: StdcallSemikeyword |stdcall|",
                "      ValueNode: (none)",
                "      DataNode: ListNode"));
        }
        [Test]
        public void ToCodeWithoutTrailingDirectives()
        {
            Parser parser = Parser.FromText("procedure of object", "",
                CompilerDefines.CreateEmpty(), new MemoryFileLoader());
            AstNode node = parser.ParseRule(RuleType);
            Assert.That(node.ToCode(), Is.EqualTo("procedure of object"));
        }
    }
}
