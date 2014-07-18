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
                "  HeaderKeyword: ImplementationKeyword |implementation|",
                "  UsesClause: (none)",
                "  Contents: ListNode"));
        }
        public void TestUses()
        {
            Assert.That("implementation uses Foo;", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeyword: ImplementationKeyword |implementation|",
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
        public void TestContents()
        {
            Assert.That("implementation const Foo = 42;", ParsesAs(
                "UnitSectionNode",
                "  HeaderKeyword: ImplementationKeyword |implementation|",
                "  UsesClause: (none)",
                "  Contents: ListNode",
                "    Items[0]: ConstSectionNode",
                "      Const: ConstKeyword |const|",
                "      ConstList: ListNode",
                "        Items[0]: ConstantDeclNode",
                "          Name: Identifier |Foo|",
                "          Colon: (none)",
                "          Type: (none)",
                "          EqualSign: EqualSign |=|",
                "          Value: Number |42|",
                "          PortabilityDirectiveList: ListNode",
                "          Semicolon: Semicolon |;|"));
        }
    }
}
