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
    public class PackedTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.PackedType; }
        }

        public void TestPackedArray()
        {
            Assert.That("packed array of Byte", ParsesAs(
                "PackedTypeNode",
                "  PackedKeywordNode: PackedKeyword |packed|",
                "  TypeNode: ArrayTypeNode",
                "    ArrayKeywordNode: ArrayKeyword |array|",
                "    OpenBracketNode: (none)",
                "    IndexListNode: ListNode",
                "    CloseBracketNode: (none)",
                "    OfKeywordNode: OfKeyword |of|",
                "    TypeNode: Identifier |Byte|"));
        }
    }
}
