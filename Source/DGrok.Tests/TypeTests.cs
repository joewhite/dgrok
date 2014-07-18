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
