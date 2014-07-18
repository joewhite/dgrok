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
    public class CaseStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.CaseStatement; }
        }

        public void TestSimple()
        {
            Assert.That("case Foo of 1: end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestEmptyElse()
        {
            Assert.That("case Foo of 1: else end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestPopulatedElse()
        {
            Assert.That("case Foo of 1: else Foo; Bar; end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
    }
}
