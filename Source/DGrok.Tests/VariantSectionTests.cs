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
    public class VariantSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VariantSection; }
        }

        public void TestUnnamed()
        {
            Assert.That("case Integer of 1: ()", ParsesAs(
                "VariantSectionNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  NameNode: (none)",
                "  ColonNode: (none)",
                "  TypeNode: Identifier |Integer|",
                "  OfKeywordNode: OfKeyword |of|",
                "  VariantGroupListNode: ListNode",
                "    Items[0]: VariantGroupNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      FieldDeclListNode: ListNode",
                "      VariantSectionNode: (none)",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "      SemicolonNode: (none)"));
        }
        public void TestNamed()
        {
            Assert.That("case Foo: Integer of 1: ()", ParsesAs(
                "VariantSectionNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  NameNode: Identifier |Foo|",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  OfKeywordNode: OfKeyword |of|",
                "  VariantGroupListNode: ListNode",
                "    Items[0]: VariantGroupNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      OpenParenthesisNode: OpenParenthesis |(|",
                "      FieldDeclListNode: ListNode",
                "      VariantSectionNode: (none)",
                "      CloseParenthesisNode: CloseParenthesis |)|",
                "      SemicolonNode: (none)"));
        }
        public void TestAtLeastOneGroupIsRequired()
        {
            AssertDoesNotParse("case Foo: Integer of");
        }
    }
}
