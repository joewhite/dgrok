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
                "  Class: (none)",
                "  Var: (none)",
                "  FieldList: ListNode",
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
                "      Semicolon: Semicolon |;|"));
        }
        public void TestVarWithField()
        {
            Assert.That("var Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  Class: (none)",
                "  Var: VarKeyword |var|",
                "  FieldList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestClassVarWithField()
        {
            Assert.That("class var Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  Class: ClassKeyword |class|",
                "  Var: VarKeyword |var|",
                "  FieldList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestEmptyVarSection()
        {
            Assert.That("var", ParsesAs(
                "FieldSectionNode",
                "  Class: (none)",
                "  Var: VarKeyword |var|",
                "  FieldList: ListNode"));
        }
        public void TestEmptyClassVarSection()
        {
            Assert.That("class var", ParsesAs(
                "FieldSectionNode",
                "  Class: ClassKeyword |class|",
                "  Var: VarKeyword |var|",
                "  FieldList: ListNode"));
        }
        public void TestClassAloneDoesNotParse()
        {
            AssertDoesNotParse("class");
        }
    }
}
