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
                "  HeaderKeywordNode: InterfaceKeyword |interface|",
                "  UsesClauseNode: (none)",
                "  ContentListNode: ListNode"));
        }
        public void TestUses()
        {
            Assert.That("interface uses Foo;", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeywordNode: InterfaceKeyword |interface|",
                "  UsesClauseNode: UsesClauseNode",
                "    UsesKeywordNode: UsesKeyword |uses|",
                "    UnitListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: UsedUnitNode",
                "          NameNode: Identifier |Foo|",
                "          InKeywordNode: (none)",
                "          FileNameNode: (none)",
                "        DelimiterNode: (none)",
                "    SemicolonNode: Semicolon |;|",
                "  ContentListNode: ListNode"));
        }
        public void TestWithContents()
        {
            Assert.That("interface resourcestring Foo = 'Bar';", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeywordNode: InterfaceKeyword |interface|",
                "  UsesClauseNode: (none)",
                "  ContentListNode: ListNode",
                "    Items[0]: ConstSectionNode",
                "      ConstKeywordNode: ResourceStringKeyword |resourcestring|",
                "      ConstListNode: ListNode",
                "        Items[0]: ConstantDeclNode",
                "          NameNode: Identifier |Foo|",
                "          ColonNode: (none)",
                "          TypeNode: (none)",
                "          EqualSignNode: EqualSign |=|",
                "          ValueNode: StringLiteral |'Bar'|",
                "          PortabilityDirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|"));
        }
    }
}
