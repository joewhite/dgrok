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
                "  OpenParenthesis: OpenParenthesis |(|",
                "  Expression: Identifier |Foo|",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestSetLiteral()
        {
            Assert.That("[1, 3..4]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracket: OpenBracket |[|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: BinaryOperationNode",
                "        Left: Number |3|",
                "        Operator: DotDot |..|",
                "        Right: Number |4|",
                "      Delimiter: (none)",
                "  CloseBracket: CloseBracket |]|"));
        }
        public void TestStringKeyword()
        {
            Assert.That("string", ParsesAs("StringKeyword |string|"));
        }
    }
}
