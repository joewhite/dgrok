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
    public class ArrayTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ArrayType; }
        }

        public void TestDynamicArray()
        {
            Assert.That("array of Integer", ParsesAs(
                "ArrayTypeNode",
                "  Array: ArrayKeyword |array|",
                "  OpenBracket: (none)",
                "  IndexList: ListNode",
                "  CloseBracket: (none)",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |Integer|"));
        }
        public void TestTwoDimensionalArray()
        {
            Assert.That("array [24..42, Byte] of Integer", ParsesAs(
                "ArrayTypeNode",
                "  Array: ArrayKeyword |array|",
                "  OpenBracket: OpenBracket |[|",
                "  IndexList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: BinaryOperationNode",
                "        Left: Number |24|",
                "        Operator: DotDot |..|",
                "        Right: Number |42|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Byte|",
                "      Delimiter: (none)",
                "  CloseBracket: CloseBracket |]|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |Integer|"));
        }
    }
}
