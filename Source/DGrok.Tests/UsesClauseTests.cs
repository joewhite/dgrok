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
    public class UsesClauseTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.UsesClause; }
        }

        public void TestOneName()
        {
            Assert.That("uses Foo;", ParsesAs(
                "UsesClauseNode",
                "  Uses: UsesKeyword |uses|",
                "  UnitList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: UsedUnitNode",
                "        Name: Identifier |Foo|",
                "        In: (none)",
                "        FileName: (none)",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestInClause()
        {
            Assert.That("uses Foo in 'Foo.pas';", ParsesAs(
                "UsesClauseNode",
                "  Uses: UsesKeyword |uses|",
                "  UnitList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: UsedUnitNode",
                "        Name: Identifier |Foo|",
                "        In: InKeyword |in|",
                "        FileName: StringLiteral |'Foo.pas'|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTwo()
        {
            Assert.That("uses Foo, Bar;", ParsesAs(
                "UsesClauseNode",
                "  Uses: UsesKeyword |uses|",
                "  UnitList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: UsedUnitNode",
                "        Name: Identifier |Foo|",
                "        In: (none)",
                "        FileName: (none)",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: UsedUnitNode",
                "        Name: Identifier |Bar|",
                "        In: (none)",
                "        FileName: (none)",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestDottedName()
        {
            Assert.That("uses Foo.Bar;", ParsesAs(
                "UsesClauseNode",
                "  Uses: UsesKeyword |uses|",
                "  UnitList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: UsedUnitNode",
                "        Name: BinaryOperationNode",
                "          Left: Identifier |Foo|",
                "          Operator: Dot |.|",
                "          Right: Identifier |Bar|",
                "        In: (none)",
                "        FileName: (none)",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestContains()
        {
            Assert.That("contains Foo;", ParsesAs(
                "UsesClauseNode",
                "  Uses: ContainsSemikeyword |contains|",
                "  UnitList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: UsedUnitNode",
                "        Name: Identifier |Foo|",
                "        In: (none)",
                "        FileName: (none)",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
