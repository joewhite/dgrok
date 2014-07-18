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
    public class CaseSelectorTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.CaseSelector; }
        }

        public void TestSingleValue()
        {
            Assert.That("1:", ParsesAs(
                "CaseSelectorNode",
                "  Values: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Statement: (none)",
                "  Semicolon: (none)"));
        }
        public void TestSingleRange()
        {
            Assert.That("1..2:", ParsesAs(
                "CaseSelectorNode",
                "  Values: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: BinaryOperationNode",
                "        Left: Number |1|",
                "        Operator: DotDot |..|",
                "        Right: Number |2|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Statement: (none)",
                "  Semicolon: (none)"));
        }
        public void TestTwoValues()
        {
            Assert.That("1, 2:", ParsesAs(
                "CaseSelectorNode",
                "  Values: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Number |2|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Statement: (none)",
                "  Semicolon: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("1: Foo", ParsesAs(
                "CaseSelectorNode",
                "  Values: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Statement: Identifier |Foo|",
                "  Semicolon: (none)"));
        }
        public void TestStatementWithSemicolon()
        {
            Assert.That("1: Foo;", ParsesAs(
                "CaseSelectorNode",
                "  Values: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Statement: Identifier |Foo|",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
