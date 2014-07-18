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
    public class ExpressionListTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExpressionList; }
        }

        public void TestNumbers()
        {
            Assert.That("24, 42", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Number |24|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Number |42|",
                "    Delimiter: (none)"));
        }
        public void TestStringLiterals()
        {
            Assert.That("'foo', 'bar'", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: StringLiteral |'foo'|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: StringLiteral |'bar'|",
                "    Delimiter: (none)"));
        }
        public void TestIdentifiers()
        {
            Assert.That("Foo, Bar", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |Foo|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Identifier |Bar|",
                "    Delimiter: (none)"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute, Index", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |Absolute|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Identifier |Index|",
                "    Delimiter: (none)"));
        }
        public void TestNils()
        {
            Assert.That("nil, nil", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: NilKeyword |nil|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: NilKeyword |nil|",
                "    Delimiter: (none)"));
        }
        public void TestParenthesizedExpressions()
        {
            Assert.That("(24), (42)", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: ParenthesizedExpressionNode",
                "      OpenParenthesis: OpenParenthesis |(|",
                "      Expression: Number |24|",
                "      CloseParenthesis: CloseParenthesis |)|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: ParenthesizedExpressionNode",
                "      OpenParenthesis: OpenParenthesis |(|",
                "      Expression: Number |42|",
                "      CloseParenthesis: CloseParenthesis |)|",
                "    Delimiter: (none)"));
        }
        public void TestSetLiterals()
        {
            Assert.That("[], []", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: SetLiteralNode",
                "      OpenBracket: OpenBracket |[|",
                "      ItemList: ListNode",
                "      CloseBracket: CloseBracket |]|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: SetLiteralNode",
                "      OpenBracket: OpenBracket |[|",
                "      ItemList: ListNode",
                "      CloseBracket: CloseBracket |]|",
                "    Delimiter: (none)"));
        }
    }
}
