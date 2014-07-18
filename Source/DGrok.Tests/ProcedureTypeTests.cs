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
