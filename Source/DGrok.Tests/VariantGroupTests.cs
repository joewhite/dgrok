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
    public class VariantGroupTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VariantGroup; }
        }

        [Test]
        public void Empty()
        {
            Assert.That("1: ()", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "  VariantSectionNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void TrailingSemicolon()
        {
            Assert.That("1: ();", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "  VariantSectionNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void MultipleValues()
        {
            Assert.That("fooBar, fooBaz: ()", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |fooBar|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |fooBaz|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "  VariantSectionNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void OneFieldWithoutSemicolon()
        {
            Assert.That("1: (Foo: Integer)", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: (none)",
                "  VariantSectionNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void OneFieldWithSemicolon()
        {
            Assert.That("1: (Foo: Integer;)", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "  VariantSectionNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void TwoFieldsWithSemicolon()
        {
            Assert.That("1: (Foo: Integer; Bar: Boolean;)", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Bar|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Boolean|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "  VariantSectionNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void VariantSection()
        {
            Assert.That("1: (Foo: Integer; case Byte of 1: ())", ParsesAs(
                "VariantGroupNode",
                "  ValueListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  FieldDeclListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "  VariantSectionNode: VariantSectionNode",
                "    CaseKeywordNode: CaseKeyword |case|",
                "    NameNode: (none)",
                "    ColonNode: (none)",
                "    TypeNode: Identifier |Byte|",
                "    OfKeywordNode: OfKeyword |of|",
                "    VariantGroupListNode: ListNode",
                "      Items[0]: VariantGroupNode",
                "        ValueListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Number |1|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        OpenParenthesisNode: OpenParenthesis |(|",
                "        FieldDeclListNode: ListNode",
                "        VariantSectionNode: (none)",
                "        CloseParenthesisNode: CloseParenthesis |)|",
                "        SemicolonNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  SemicolonNode: (none)"));
        }
    }
}
