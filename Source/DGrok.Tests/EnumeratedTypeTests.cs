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
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: EnumeratedTypeElementNode",
                "        Name: Identifier |fooBar|",
                "        EqualSign: (none)",
                "        Value: (none)",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: EnumeratedTypeElementNode",
                "        Name: Identifier |fooBaz|",
                "        EqualSign: (none)",
                "        Value: (none)",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
    }
}
