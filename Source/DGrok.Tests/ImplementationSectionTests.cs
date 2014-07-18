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
    public class ImplementationSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ImplementationSection; }
        }

        public void TestEmptyImplementationSection()
        {
            Assert.That("implementation", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeywordNode: ImplementationKeyword |implementation|",
                "  UsesClauseNode: (none)",
                "  ContentListNode: ListNode"));
        }
        public void TestUses()
        {
            Assert.That("implementation uses Foo;", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeywordNode: ImplementationKeyword |implementation|",
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
        public void TestContents()
        {
            Assert.That("implementation const Foo = 42;", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeywordNode: ImplementationKeyword |implementation|",
                "  UsesClauseNode: (none)",
                "  ContentListNode: ListNode",
                "    Items[0]: ConstSectionNode",
                "      ConstKeywordNode: ConstKeyword |const|",
                "      ConstListNode: ListNode",
                "        Items[0]: ConstantDeclNode",
                "          NameNode: Identifier |Foo|",
                "          ColonNode: (none)",
                "          TypeNode: (none)",
                "          EqualSignNode: EqualSign |=|",
                "          ValueNode: Number |42|",
                "          PortabilityDirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|"));
        }
    }
}
