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
                "  OpenBracket: OpenBracket |[|",
                "  ItemList: ListNode",
                "  CloseBracket: CloseBracket |]|"));
        }
        public void TestOneValue()
        {
            Assert.That("[42]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracket: OpenBracket |[|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  CloseBracket: CloseBracket |]|"));
        }
        public void TestTwoRanges()
        {
            Assert.That("[1..2, 4..5]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracket: OpenBracket |[|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: BinaryOperationNode",
                "        Left: Number |1|",
                "        Operator: DotDot |..|",
                "        Right: Number |2|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: BinaryOperationNode",
                "        Left: Number |4|",
                "        Operator: DotDot |..|",
                "        Right: Number |5|",
                "      Delimiter: (none)",
                "  CloseBracket: CloseBracket |]|"));
        }
    }
}
