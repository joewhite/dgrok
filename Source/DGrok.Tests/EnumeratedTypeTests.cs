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
    public class EnumeratedTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.EnumeratedType; }
        }

        public void TestEmptyEnumDoesNotParse()
        {
            AssertDoesNotParse("()");
        }
        public void TestTwoValues()
        {
            Assert.That("(fooBar, fooBaz)", ParsesAs(
                "EnumeratedTypeNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: EnumeratedTypeElementNode",
                "        NameNode: Identifier |fooBar|",
                "        EqualSignNode: (none)",
                "        ValueNode: (none)",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: EnumeratedTypeElementNode",
                "        NameNode: Identifier |fooBaz|",
                "        EqualSignNode: (none)",
                "        ValueNode: (none)",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
    }
}
