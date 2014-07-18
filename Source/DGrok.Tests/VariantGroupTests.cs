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
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "  VariantSection: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: (none)"));
        }
        public void TestTrailingSemicolon()
        {
            Assert.That("1: ();", ParsesAs(
                "VariantGroupNode",
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "  VariantSection: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestMultipleValues()
        {
            Assert.That("fooBar, fooBaz: ()", ParsesAs(
                "VariantGroupNode",
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |fooBar|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |fooBaz|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "  VariantSection: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: (none)"));
        }
        public void TestOneFieldWithoutSemicolon()
        {
            Assert.That("1: (Foo: Integer)", ParsesAs(
                "VariantGroupNode",
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: (none)",
                "  VariantSection: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: (none)"));
        }
        public void TestOneFieldWithSemicolon()
        {
            Assert.That("1: (Foo: Integer;)", ParsesAs(
                "VariantGroupNode",
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "  VariantSection: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: (none)"));
        }
        public void TestTwoFieldsWithSemicolon()
        {
            Assert.That("1: (Foo: Integer; Bar: Boolean;)", ParsesAs(
                "VariantGroupNode",
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "    Items[1]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Bar|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Boolean|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "  VariantSection: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: (none)"));
        }
        public void TestVariantSection()
        {
            Assert.That("1: (Foo: Integer; case Byte of 1: ())", ParsesAs(
                "VariantGroupNode",
                "  ValueList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |1|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  FieldDeclList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "  VariantSection: VariantSectionNode",
                "    Case: CaseKeyword |case|",
                "    Name: (none)",
                "    Colon: (none)",
                "    Type: Identifier |Byte|",
                "    Of: OfKeyword |of|",
                "    VariantGroupList: ListNode",
                "      Items[0]: VariantGroupNode",
                "        ValueList: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: Number |1|",
                "            Delimiter: (none)",
                "        Colon: Colon |:|",
                "        OpenParenthesis: OpenParenthesis |(|",
                "        FieldDeclList: ListNode",
                "        VariantSection: (none)",
                "        CloseParenthesis: CloseParenthesis |)|",
                "        Semicolon: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Semicolon: (none)"));
        }
    }
}
