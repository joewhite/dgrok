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
                "  Left: Identifier |Foo|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Bar|"));
        }
        public void TestTwoDotOperators()
        {
            Assert.That("Foo.Bar.Baz", ParsesAs(
                "BinaryOperationNode",
                "  Left: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |Bar|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Baz|"));
        }
        public void TestDotFollowedByKeyword()
        {
            Assert.That("Should.Not", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |Should|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Not|"));
        }
        public void TestCaret()
        {
            Assert.That("Foo^", ParsesAs(
                "PointerDereferenceNode",
                "  Operand: Identifier |Foo|",
                "  Caret: Caret |^|"));
        }
        public void TestTwoCarets()
        {
            Assert.That("Foo^^", ParsesAs(
                "PointerDereferenceNode",
                "  Operand: PointerDereferenceNode",
                "    Operand: Identifier |Foo|",
                "    Caret: Caret |^|",
                "  Caret: Caret |^|"));
        }
        public void TestEmptyArrayIndexDoesNotParse()
        {
            AssertDoesNotParse("Foo[]");
        }
        public void TestOneArrayIndex()
        {
            Assert.That("Foo[42]", ParsesAs(
                "ParameterizedNode",
                "  Left: Identifier |Foo|",
                "  OpenDelimiter: OpenBracket |[|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  CloseDelimiter: CloseBracket |]|"));
        }
        public void TestTwoArrayIndexes()
        {
            Assert.That("Foo[24, 42]", ParsesAs(
                "ParameterizedNode",
                "  Left: Identifier |Foo|",
                "  OpenDelimiter: OpenBracket |[|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |24|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  CloseDelimiter: CloseBracket |]|"));
        }
        public void TestNoParameters()
        {
            Assert.That("Foo()", ParsesAs(
                "ParameterizedNode",
                "  Left: Identifier |Foo|",
                "  OpenDelimiter: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "  CloseDelimiter: CloseParenthesis |)|"));
        }
        public void TestOneParameter()
        {
            Assert.That("Foo(42)", ParsesAs(
                "ParameterizedNode",
                "  Left: Identifier |Foo|",
                "  OpenDelimiter: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  CloseDelimiter: CloseParenthesis |)|"));
        }
        public void TestTwoParameters()
        {
            Assert.That("Foo(24, 42)", ParsesAs(
                "ParameterizedNode",
                "  Left: Identifier |Foo|",
                "  OpenDelimiter: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |24|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  CloseDelimiter: CloseParenthesis |)|"));
        }
        public void TestStringCast()
        {
            Assert.That("string('0')", ParsesAs(
                "ParameterizedNode",
                "  Left: StringKeyword |string|",
                "  OpenDelimiter: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: StringLiteral |'0'|",
                "      Delimiter: (none)",
                "  CloseDelimiter: CloseParenthesis |)|"));
        }
        public void TestColonSyntax()
        {
            Assert.That("Str(X:0, S)", ParsesAs(
                "ParameterizedNode",
                "  Left: Identifier |Str|",
                "  OpenDelimiter: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: NumberFormatNode",
                "        Value: Identifier |X|",
                "        SizeColon: Colon |:|",
                "        Size: Number |0|",
                "        PrecisionColon: (none)",
                "        Precision: (none)",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |S|",
                "      Delimiter: (none)",
                "  CloseDelimiter: CloseParenthesis |)|"));
        }
    }
}
