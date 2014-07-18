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
    public class ExpressionOrRangeListTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExpressionOrRangeList; }
        }

        public void TestSingleNumber()
        {
            Assert.That("42", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Number |42|",
                "    Delimiter: (none)"));
        }
        public void TestSingleExpression()
        {
            Assert.That("6 * 9", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: BinaryOperationNode",
                "      Left: Number |6|",
                "      Operator: TimesSign |*|",
                "      Right: Number |9|",
                "    Delimiter: (none)"));
        }
        public void TestSingleRange()
        {
            Assert.That("24..42", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: BinaryOperationNode",
                "      Left: Number |24|",
                "      Operator: DotDot |..|",
                "      Right: Number |42|",
                "    Delimiter: (none)"));
        }
        public void TestTwoNumbers()
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
        public void TestTwoCharacterLiterals()
        {
            Assert.That("'4', '2'", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: StringLiteral |'4'|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: StringLiteral |'2'|",
                "    Delimiter: (none)"));
        }
        public void TestTwoIdents()
        {
            Assert.That("fooBar, fooBaz", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |fooBar|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Identifier |fooBaz|",
                "    Delimiter: (none)"));
        }
        public void TestTwoSemikeywords()
        {
            Assert.That("Abstract, Index", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: Identifier |Abstract|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: Identifier |Index|",
                "    Delimiter: (none)"));
        }
        public void TestTwoParenthesizedExpressions()
        {
            Assert.That("(1), (2)", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    Item: ParenthesizedExpressionNode",
                "      OpenParenthesis: OpenParenthesis |(|",
                "      Expression: Number |1|",
                "      CloseParenthesis: CloseParenthesis |)|",
                "    Delimiter: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    Item: ParenthesizedExpressionNode",
                "      OpenParenthesis: OpenParenthesis |(|",
                "      Expression: Number |2|",
                "      CloseParenthesis: CloseParenthesis |)|",
                "    Delimiter: (none)"));
        }
    }
}
