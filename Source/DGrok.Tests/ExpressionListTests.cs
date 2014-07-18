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
                "    ItemNode: Number |24|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Number |42|",
                "    DelimiterNode: (none)"));
        }
        public void TestStringLiterals()
        {
            Assert.That("'foo', 'bar'", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: StringLiteral |'foo'|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: StringLiteral |'bar'|",
                "    DelimiterNode: (none)"));
        }
        public void TestIdentifiers()
        {
            Assert.That("Foo, Bar", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Foo|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Identifier |Bar|",
                "    DelimiterNode: (none)"));
        }
        public void TestSemikeywords()
        {
            Assert.That("Absolute, Index", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: Identifier |Absolute|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: Identifier |Index|",
                "    DelimiterNode: (none)"));
        }
        public void TestNils()
        {
            Assert.That("nil, nil", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: NilKeyword |nil|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: NilKeyword |nil|",
                "    DelimiterNode: (none)"));
        }
        public void TestParenthesizedExpressions()
        {
            Assert.That("(24), (42)", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: ParenthesizedExpressionNode",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      ExpressionNode: Number |24|",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: ParenthesizedExpressionNode",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      ExpressionNode: Number |42|",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "    DelimiterNode: (none)"));
        }
        public void TestSetLiterals()
        {
            Assert.That("[], []", ParsesAs(
                "ListNode",
                "  Items[0]: DelimitedItemNode",
                "    ItemNode: SetLiteralNode",
                "      OpenBracketNode: OpenBracket |[|",
                "      ItemListNode: ListNode",
                "      CloseBracketNode: CloseBracket |]|",
                "    DelimiterNode: Comma |,|",
                "  Items[1]: DelimitedItemNode",
                "    ItemNode: SetLiteralNode",
                "      OpenBracketNode: OpenBracket |[|",
                "      ItemListNode: ListNode",
                "      CloseBracketNode: CloseBracket |]|",
                "    DelimiterNode: (none)"));
        }
    }
}
