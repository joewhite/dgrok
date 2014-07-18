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
                "  Case: CaseKeyword |case|",
                "  Name: (none)",
                "  Colon: (none)",
                "  Type: Identifier |Integer|",
                "  Of: OfKeyword |of|",
                "  VariantGroupList: ListNode",
                "    Items[0]: VariantGroupNode",
                "      ValueList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Number |1|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      OpenParenthesis: OpenParenthesis |(|",
                "      FieldDeclList: ListNode",
                "      VariantSection: (none)",
                "      CloseParenthesis: CloseParenthesis |)|",
                "      Semicolon: (none)"));
        }
        public void TestNamed()
        {
            Assert.That("case Foo: Integer of 1: ()", ParsesAs(
                "VariantSectionNode",
                "  Case: CaseKeyword |case|",
                "  Name: Identifier |Foo|",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Of: OfKeyword |of|",
                "  VariantGroupList: ListNode",
                "    Items[0]: VariantGroupNode",
                "      ValueList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Number |1|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      OpenParenthesis: OpenParenthesis |(|",
                "      FieldDeclList: ListNode",
                "      VariantSection: (none)",
                "      CloseParenthesis: CloseParenthesis |)|",
                "      Semicolon: (none)"));
        }
        public void TestAtLeastOneGroupIsRequired()
        {
            AssertDoesNotParse("case Foo: Integer of");
        }
    }
}
