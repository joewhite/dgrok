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
    public class TypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Type; }
        }

        [Test]
        public void EmptyEnumDoesNotParse()
        {
            AssertDoesNotParse("()");
        }
        [Test]
        public void Enum()
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
        [Test]
        public void QualifiedIdentifier()
        {
            Assert.That("System.Integer", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |System|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Integer|"));
        }
        [Test]
        public void Range()
        {
            Assert.That("24..42", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |24|",
                "  OperatorNode: DotDot |..|",
                "  RightNode: Number |42|"));
        }
        [Test]
        public void Array()
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
        [Test]
        public void Set()
        {
            Assert.That("set of Byte", ParsesAs(
                "SetOfNode",
                "  SetKeywordNode: SetKeyword |set|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |Byte|"));
        }
        [Test]
        public void File()
        {
            Assert.That("file", ParsesAs(
                "FileTypeNode",
                "  FileKeywordNode: FileKeyword |file|",
                "  OfKeywordNode: (none)",
                "  TypeNode: (none)"));
        }
        [Test]
        public void RecordHelper()
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
        [Test]
        public void Record()
        {
            Assert.That("record end", ParsesAs(
                "RecordTypeNode",
                "  RecordKeywordNode: RecordKeyword |record|",
                "  ContentListNode: ListNode",
                "  VariantSectionNode: (none)",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Pointer()
        {
            Assert.That("^TFoo", ParsesAs(
                "PointerTypeNode",
                "  CaretNode: Caret |^|",
                "  TypeNode: Identifier |TFoo|"));
        }
        [Test]
        public void String()
        {
            Assert.That("string[42]", ParsesAs(
                "StringOfLengthNode",
                "  StringKeywordNode: StringKeyword |string|",
                "  OpenBracketNode: OpenBracket |[|",
                "  LengthNode: Number |42|",
                "  CloseBracketNode: CloseBracket |]|"));
        }
        [Test]
        public void ProcedureType()
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
        public void ClassHelper()
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
        [Test]
        public void ClassOf()
        {
            Assert.That("class of TObject", ParsesAs(
                "ClassOfNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |TObject|"));
        }
        [Test]
        public void Class()
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
        [Test]
        public void Interface()
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
        [Test]
        public void PackedType()
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
