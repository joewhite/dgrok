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
    public class TypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Type; }
        }

        public void TestEmptyEnumDoesNotParse()
        {
            AssertDoesNotParse("()");
        }
        public void TestEnum()
        {
            Assert.That("(fooBar)", ParsesAs(
                "EnumeratedTypeNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: EnumeratedTypeElementNode",
                "        NameNode: Identifier |fooBar|",
                "        EqualSignNode: (none)",
                "        ValueNode: (none)",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        public void TestQualifiedIdentifier()
        {
            Assert.That("System.Integer", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |System|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Integer|"));
        }
        public void TestRange()
        {
            Assert.That("24..42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |24|",
                "  OperatorNode: DotDot |..|",
                "  RightNode: Number |42|"));
        }
        public void TestArray()
        {
            Assert.That("array of Integer", ParsesAs(
                "ArrayTypeNode",
                "  ArrayKeywordNode: ArrayKeyword |array|",
                "  OpenBracketNode: (none)",
                "  IndexListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |Integer|"));
        }
        public void TestSet()
        {
            Assert.That("set of Byte", ParsesAs(
                "SetOfNode",
                "  SetKeywordNode: SetKeyword |set|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |Byte|"));
        }
        public void TestFile()
        {
            Assert.That("file", ParsesAs(
                "FileTypeNode",
                "  FileKeywordNode: FileKeyword |file|",
                "  OfKeywordNode: (none)",
                "  TypeNode: (none)"));
        }
        public void TestRecordHelper()
        {
            Assert.That("record helper for TPoint end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeywordNode: RecordKeyword |record|",
                "  HelperSemikeywordNode: HelperSemikeyword |helper|",
                "  OpenParenthesisNode: (none)",
                "  BaseHelperTypeNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  ForKeywordNode: ForKeyword |for|",
                "  TypeNode: Identifier |TPoint|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestRecord()
        {
            Assert.That("record end", ParsesAs(
                "RecordTypeNode",
                "  RecordKeywordNode: RecordKeyword |record|",
                "  ContentListNode: ListNode",
                "  VariantSectionNode: (none)",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestPointer()
        {
            Assert.That("^TFoo", ParsesAs(
                "PointerTypeNode",
                "  CaretNode: Caret |^|",
                "  TypeNode: Identifier |TFoo|"));
        }
        public void TestString()
        {
            Assert.That("string[42]", ParsesAs(
                "StringOfLengthNode",
                "  StringKeywordNode: StringKeyword |string|",
                "  OpenBracketNode: OpenBracket |[|",
                "  LengthNode: Number |42|",
                "  CloseBracketNode: CloseBracket |]|"));
        }
        public void TestProcedureType()
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
        public void TestClassHelper()
        {
            Assert.That("class helper for TObject end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeywordNode: ClassKeyword |class|",
                "  HelperSemikeywordNode: HelperSemikeyword |helper|",
                "  OpenParenthesisNode: (none)",
                "  BaseHelperTypeNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  ForKeywordNode: ForKeyword |for|",
                "  TypeNode: Identifier |TObject|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestClassOf()
        {
            Assert.That("class of TObject", ParsesAs(
                "ClassOfNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |TObject|"));
        }
        public void TestClass()
        {
            Assert.That("class end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestInterface()
        {
            Assert.That("interface end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: InterfaceKeyword |interface|",
                "  OpenParenthesisNode: (none)",
                "  BaseInterfaceNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  OpenBracketNode: (none)",
                "  GuidNode: (none)",
                "  CloseBracketNode: (none)",
                "  MethodAndPropertyListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestPackedType()
        {
            Assert.That("packed array of Byte", ParsesAs(
                "PackedTypeNode",
                "  PackedKeywordNode: PackedKeyword |packed|",
                "  TypeNode: ArrayTypeNode",
                "    ArrayKeywordNode: ArrayKeyword |array|",
                "    OpenBracketNode: (none)",
                "    IndexListNode: ListNode",
                "    CloseBracketNode: (none)",
                "    OfKeywordNode: OfKeyword |of|",
                "    TypeNode: Identifier |Byte|"));
        }
    }
}
