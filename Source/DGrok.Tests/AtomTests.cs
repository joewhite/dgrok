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
    public class AtomTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Atom; }
        }

        public void TestParticle()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestSemikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
        public void TestDotOperator()
        {
            Assert.That("Foo.Bar", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Foo|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Bar|"));
        }
        public void TestTwoDotOperators()
        {
            Assert.That("Foo.Bar.Baz", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Baz|"));
        }
        public void TestDotFollowedByKeyword()
        {
            Assert.That("Should.Not", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Should|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Not|"));
        }
        public void TestCaret()
        {
            Assert.That("Foo^", ParsesAs(
                "PointerDereferenceNode",
                "  OperandNode: Identifier |Foo|",
                "  CaretNode: Caret |^|"));
        }
        public void TestTwoCarets()
        {
            Assert.That("Foo^^", ParsesAs(
                "PointerDereferenceNode",
                "  OperandNode: PointerDereferenceNode",
                "    OperandNode: Identifier |Foo|",
                "    CaretNode: Caret |^|",
                "  CaretNode: Caret |^|"));
        }
        public void TestEmptyArrayIndexDoesNotParse()
        {
            AssertDoesNotParse("Foo[]");
        }
        public void TestOneArrayIndex()
        {
            Assert.That("Foo[42]", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Foo|",
                "  OpenDelimiterNode: OpenBracket |[|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseBracket |]|"));
        }
        public void TestTwoArrayIndexes()
        {
            Assert.That("Foo[24, 42]", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Foo|",
                "  OpenDelimiterNode: OpenBracket |[|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |24|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseBracket |]|"));
        }
        public void TestNoParameters()
        {
            Assert.That("Foo()", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Foo|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
        public void TestOneParameter()
        {
            Assert.That("Foo(42)", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Foo|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
        public void TestTwoParameters()
        {
            Assert.That("Foo(24, 42)", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Foo|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |24|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
        public void TestStringCast()
        {
            Assert.That("string('0')", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: StringKeyword |string|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: StringLiteral |'0'|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
        public void TestFileCast()
        {
            Assert.That("file(AUntypedVarParameter)", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: FileKeyword |file|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |AUntypedVarParameter|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
        public void TestColonSyntax()
        {
            Assert.That("Str(X:0, S)", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Str|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: NumberFormatNode",
                "        ValueNode: Identifier |X|",
                "        SizeColonNode: Colon |:|",
                "        SizeNode: Number |0|",
                "        PrecisionColonNode: (none)",
                "        PrecisionNode: (none)",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |S|",
                "      DelimiterNode: (none)",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
    }
}
