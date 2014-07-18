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
    public class SetLiteralTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SetLiteral; }
        }

        public void TestEmptySet()
        {
            Assert.That("[]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ItemListNode: ListNode",
                "  CloseBracketNode: CloseBracket |]|"));
        }
        public void TestOneValue()
        {
            Assert.That("[42]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|"));
        }
        public void TestTwoRanges()
        {
            Assert.That("[1..2, 4..5]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Number |1|",
                "        OperatorNode: DotDot |..|",
                "        RightNode: Number |2|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Number |4|",
                "        OperatorNode: DotDot |..|",
                "        RightNode: Number |5|",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|"));
        }
    }
}
