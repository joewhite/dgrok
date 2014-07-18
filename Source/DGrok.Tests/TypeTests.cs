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
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: EnumeratedTypeElementNode",
                "        Name: Identifier |fooBar|",
                "        EqualSign: (none)",
                "        Value: (none)",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestQualifiedIdentifier()
        {
            Assert.That("System.Integer", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |System|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Integer|"));
        }
        public void TestRange()
        {
            Assert.That("24..42", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |24|",
                "  Operator: DotDot |..|",
                "  Right: Number |42|"));
        }
        public void TestArray()
        {
            Assert.That("array of Integer", ParsesAs(
                "ArrayTypeNode",
                "  Array: ArrayKeyword |array|",
                "  OpenBracket: (none)",
                "  IndexList: ListNode",
                "  CloseBracket: (none)",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |Integer|"));
        }
        public void TestSet()
        {
            Assert.That("set of Byte", ParsesAs(
                "SetOfNode",
                "  Set: SetKeyword |set|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |Byte|"));
        }
        public void TestFile()
        {
            Assert.That("file", ParsesAs(
                "FileTypeNode",
                "  File: FileKeyword |file|",
                "  Of: (none)",
                "  Type: (none)"));
        }
        public void TestRecordHelper()
        {
            Assert.That("record helper for TPoint end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: RecordKeyword |record|",
                "  Helper: HelperSemikeyword |helper|",
                "  OpenParenthesis: (none)",
                "  BaseHelperType: (none)",
                "  CloseParenthesis: (none)",
                "  For: ForKeyword |for|",
                "  Type: Identifier |TPoint|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestRecord()
        {
            Assert.That("record end", ParsesAs(
                "RecordTypeNode",
                "  Record: RecordKeyword |record|",
                "  Contents: ListNode",
                "  VariantSection: (none)",
                "  End: EndKeyword |end|"));
        }
        public void TestPointer()
        {
            Assert.That("^TFoo", ParsesAs(
                "PointerTypeNode",
                "  Caret: Caret |^|",
                "  Type: Identifier |TFoo|"));
        }
        public void TestString()
        {
            Assert.That("string[42]", ParsesAs(
                "StringOfLengthNode",
                "  String: StringKeyword |string|",
                "  OpenBracket: OpenBracket |[|",
                "  Length: Number |42|",
                "  CloseBracket: CloseBracket |]|"));
        }
        public void TestProcedureType()
        {
            Assert.That("procedure of object", ParsesAs(
                "ProcedureTypeNode",
                "  MethodType: ProcedureKeyword |procedure|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  FirstDirectives: ListNode",
                "  Of: OfKeyword |of|",
                "  Object: ObjectKeyword |object|",
                "  SecondDirectives: ListNode"));
        }
        public void TestClassHelper()
        {
            Assert.That("class helper for TObject end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: ClassKeyword |class|",
                "  Helper: HelperSemikeyword |helper|",
                "  OpenParenthesis: (none)",
                "  BaseHelperType: (none)",
                "  CloseParenthesis: (none)",
                "  For: ForKeyword |for|",
                "  Type: Identifier |TObject|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestClassOf()
        {
            Assert.That("class of TObject", ParsesAs(
                "ClassOfNode",
                "  Class: ClassKeyword |class|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |TObject|"));
        }
        public void TestClass()
        {
            Assert.That("class end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: (none)",
                "  OpenParenthesis: (none)",
                "  InheritanceList: ListNode",
                "  CloseParenthesis: (none)",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestInterface()
        {
            Assert.That("interface end", ParsesAs(
                "InterfaceTypeNode",
                "  Interface: InterfaceKeyword |interface|",
                "  OpenParenthesis: (none)",
                "  BaseInterface: (none)",
                "  CloseParenthesis: (none)",
                "  OpenBracket: (none)",
                "  Guid: (none)",
                "  CloseBracket: (none)",
                "  MethodAndPropertyList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPackedType()
        {
            Assert.That("packed array of Byte", ParsesAs(
                "PackedTypeNode",
                "  Packed: PackedKeyword |packed|",
                "  Type: ArrayTypeNode",
                "    Array: ArrayKeyword |array|",
                "    OpenBracket: (none)",
                "    IndexList: ListNode",
                "    CloseBracket: (none)",
                "    Of: OfKeyword |of|",
                "    Type: Identifier |Byte|"));
        }
    }
}
