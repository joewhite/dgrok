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
    public class ProcedureTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ProcedureType; }
        }

        public void TestProcedure()
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
        public void TestFunction()
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
        public void TestEmptyParameterList()
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
        public void TestParameters()
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
        public void TestOfObject()
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
        public void TestDirective()
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
        public void TestDirectiveBeforeOfObject()
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
        public void TestDirectiveAfterOfObjectWithSemicolon()
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
        public void TestToCodeWithoutTrailingDirectives()
        {
            Parser parser = Parser.FromText("procedure of object", "",
                CompilerDefines.CreateEmpty(), new MemoryFileLoader());
            AstNode node = parser.ParseRule(RuleType);
            Assert.That(node.ToCode(), Is.EqualTo("procedure of object"));
        }
    }
}
