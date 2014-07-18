// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class VariantSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VariantSection; }
        }

        [Test]
        public void Unnamed()
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
        [Test]
        public void Named()
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
        [Test]
        public void AtLeastOneGroupIsRequired()
        {
            AssertDoesNotParse("case Foo: Integer of");
        }
    }
}
