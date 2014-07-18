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
                "  UsesKeywordNode: UsesKeyword |uses|",
                "  UnitListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: UsedUnitNode",
                "        NameNode: Identifier |Foo|",
                "        InKeywordNode: (none)",
                "        FileNameNode: (none)",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestInClause()
        {
            Assert.That("uses Foo in 'Foo.pas';", ParsesAs(
                "UsesClauseNode",
                "  UsesKeywordNode: UsesKeyword |uses|",
                "  UnitListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: UsedUnitNode",
                "        NameNode: Identifier |Foo|",
                "        InKeywordNode: InKeyword |in|",
                "        FileNameNode: StringLiteral |'Foo.pas'|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestTwo()
        {
            Assert.That("uses Foo, Bar;", ParsesAs(
                "UsesClauseNode",
                "  UsesKeywordNode: UsesKeyword |uses|",
                "  UnitListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: UsedUnitNode",
                "        NameNode: Identifier |Foo|",
                "        InKeywordNode: (none)",
                "        FileNameNode: (none)",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: UsedUnitNode",
                "        NameNode: Identifier |Bar|",
                "        InKeywordNode: (none)",
                "        FileNameNode: (none)",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestDottedName()
        {
            Assert.That("uses Foo.Bar;", ParsesAs(
                "UsesClauseNode",
                "  UsesKeywordNode: UsesKeyword |uses|",
                "  UnitListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: UsedUnitNode",
                "        NameNode: BinaryOperationNode",
                "          LeftNode: Identifier |Foo|",
                "          OperatorNode: Dot |.|",
                "          RightNode: Identifier |Bar|",
                "        InKeywordNode: (none)",
                "        FileNameNode: (none)",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestContains()
        {
            Assert.That("contains Foo;", ParsesAs(
                "UsesClauseNode",
                "  UsesKeywordNode: ContainsSemikeyword |contains|",
                "  UnitListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: UsedUnitNode",
                "        NameNode: Identifier |Foo|",
                "        InKeywordNode: (none)",
                "        FileNameNode: (none)",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
