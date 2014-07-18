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
    public class InterfaceSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.InterfaceSection; }
        }

        public void TestEmptyInterfaceSection()
        {
            Assert.That("interface", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeyword: InterfaceKeyword |interface|",
                "  UsesClause: (none)",
                "  Contents: ListNode"));
        }
        public void TestUses()
        {
            Assert.That("interface uses Foo;", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeyword: InterfaceKeyword |interface|",
                "  UsesClause: UsesClauseNode",
                "    Uses: UsesKeyword |uses|",
                "    UnitList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: UsedUnitNode",
                "          Name: Identifier |Foo|",
                "          In: (none)",
                "          FileName: (none)",
                "        Delimiter: (none)",
                "    Semicolon: Semicolon |;|",
                "  Contents: ListNode"));
        }
        public void TestWithContents()
        {
            Assert.That("interface resourcestring Foo = 'Bar';", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeyword: InterfaceKeyword |interface|",
                "  UsesClause: (none)",
                "  Contents: ListNode",
                "    Items[0]: ConstSectionNode",
                "      Const: ResourceStringKeyword |resourcestring|",
                "      ConstList: ListNode",
                "        Items[0]: ConstantDeclNode",
                "          Name: Identifier |Foo|",
                "          Colon: (none)",
                "          Type: (none)",
                "          EqualSign: EqualSign |=|",
                "          Value: StringLiteral |'Bar'|",
                "          PortabilityDirectiveList: ListNode",
                "          Semicolon: Semicolon |;|"));
        }
    }
}
