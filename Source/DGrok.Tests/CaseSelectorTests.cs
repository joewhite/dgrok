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
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  StatementNode: (none)",
                "  SemicolonNode: (none)"));
        }
        public void TestSingleRange()
        {
            Assert.That("1..2:", ParsesAs(
                "CaseSelectorNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Number |1|",
                "        OperatorNode: DotDot |..|",
                "        RightNode: Number |2|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  StatementNode: (none)",
                "  SemicolonNode: (none)"));
        }
        public void TestTwoValues()
        {
            Assert.That("1, 2:", ParsesAs(
                "CaseSelectorNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Number |2|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  StatementNode: (none)",
                "  SemicolonNode: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("1: Foo", ParsesAs(
                "CaseSelectorNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  StatementNode: Identifier |Foo|",
                "  SemicolonNode: (none)"));
        }
        public void TestStatementWithSemicolon()
        {
            Assert.That("1: Foo;", ParsesAs(
                "CaseSelectorNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  StatementNode: Identifier |Foo|",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
