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
    public class VariantGroupTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VariantGroup; }
        }

        public void TestEmpty()
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
        public void TestTrailingSemicolon()
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
        public void TestMultipleValues()
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
        public void TestOneFieldWithoutSemicolon()
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
        public void TestOneFieldWithSemicolon()
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
        public void TestTwoFieldsWithSemicolon()
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
        public void TestVariantSection()
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
