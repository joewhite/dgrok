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
    public class FieldSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FieldSection; }
        }

        public void TestFields()
        {
            Assert.That("Foo: Integer; Bar: Boolean;", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: (none)",
                "  VarKeywordNode: (none)",
                "  FieldListNode: ListNode",
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
                "      SemicolonNode: Semicolon |;|"));
        }
        public void TestVarWithField()
        {
            Assert.That("var Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: (none)",
                "  VarKeywordNode: VarKeyword |var|",
                "  FieldListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        public void TestClassVarWithField()
        {
            Assert.That("class var Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  VarKeywordNode: VarKeyword |var|",
                "  FieldListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        public void TestEmptyVarSection()
        {
            Assert.That("var", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: (none)",
                "  VarKeywordNode: VarKeyword |var|",
                "  FieldListNode: ListNode"));
        }
        public void TestEmptyClassVarSection()
        {
            Assert.That("class var", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  VarKeywordNode: VarKeyword |var|",
                "  FieldListNode: ListNode"));
        }
        public void TestClassAloneDoesNotParse()
        {
            AssertDoesNotParse("class");
        }
    }
}
