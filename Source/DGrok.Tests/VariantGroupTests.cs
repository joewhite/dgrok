// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
