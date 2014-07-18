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
    public class ParticleTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Particle; }
        }

        public void TestNil()
        {
            Assert.That("nil", ParsesAs("NilKeyword |nil|"));
        }
        public void TestStringLiteral()
        {
            Assert.That("'Foo'", ParsesAs("StringLiteral |'Foo'|"));
        }
        public void TestNumber()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        public void TestIdentifier()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        public void TestSemikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
        public void TestParenthesizedExpression()
        {
            Assert.That("(Foo)", ParsesAs(
                "ParenthesizedExpressionNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ExpressionNode: Identifier |Foo|",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        public void TestSetLiteral()
        {
            Assert.That("[1, 3..4]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Number |3|",
                "        OperatorNode: DotDot |..|",
                "        RightNode: Number |4|",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|"));
        }
        public void TestStringKeyword()
        {
            Assert.That("string", ParsesAs("StringKeyword |string|"));
        }
        public void TestFileKeyword()
        {
            Assert.That("file", ParsesAs("FileKeyword |file|"));
        }
    }
}
