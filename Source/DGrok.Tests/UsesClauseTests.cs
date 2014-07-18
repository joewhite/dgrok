// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class UsesClauseTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.UsesClause; }
        }

        [Test]
        public void OneName()
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
        [Test]
        public void InClause()
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
        [Test]
        public void Two()
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
        [Test]
        public void DottedName()
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
        [Test]
        public void Contains()
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
