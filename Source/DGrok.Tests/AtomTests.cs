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
    public class AtomTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Atom; }
        }

        [Test]
        public void Particle()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void Semikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
        [Test]
        public void DotOperator()
        {
            Assert.That("Foo.Bar", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Foo|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Bar|"));
        }
        [Test]
        public void TwoDotOperators()
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
        [Test]
        public void DotFollowedByKeyword()
        {
            Assert.That("Should.Not", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Should|",
                "  OperatorNode: Dot |.|",
                "  RightNode: Identifier |Not|"));
        }
        [Test]
        public void Caret()
        {
            Assert.That("Foo^", ParsesAs(
                "PointerDereferenceNode",
                "  OperandNode: Identifier |Foo|",
                "  CaretNode: Caret |^|"));
        }
        [Test]
        public void TwoCarets()
        {
            Assert.That("Foo^^", ParsesAs(
                "PointerDereferenceNode",
                "  OperandNode: PointerDereferenceNode",
                "    OperandNode: Identifier |Foo|",
                "    CaretNode: Caret |^|",
                "  CaretNode: Caret |^|"));
        }
        [Test]
        public void EmptyArrayIndexDoesNotParse()
        {
            AssertDoesNotParse("Foo[]");
        }
        [Test]
        public void OneArrayIndex()
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
        [Test]
        public void TwoArrayIndexes()
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
        [Test]
        public void NoParameters()
        {
            Assert.That("Foo()", ParsesAs(
                "ParameterizedNode",
                "  LeftNode: Identifier |Foo|",
                "  OpenDelimiterNode: OpenParenthesis |(|",
                "  ParameterListNode: ListNode",
                "  CloseDelimiterNode: CloseParenthesis |)|"));
        }
        [Test]
        public void OneParameter()
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
        [Test]
        public void TwoParameters()
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
        [Test]
        public void StringCast()
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
        [Test]
        public void FileCast()
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
        [Test]
        public void ColonSyntax()
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
