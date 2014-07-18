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
    public class LabelDeclSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.LabelDeclSection; }
        }

        [Test]
        public void OneNumber()
        {
            Assert.That("label 42;", ParsesAs(
                "LabelDeclSectionNode",
                "  LabelKeywordNode: LabelKeyword |label|",
                "  LabelListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void OneIdentifier()
        {
            Assert.That("label Foo;", ParsesAs(
                "LabelDeclSectionNode",
                "  LabelKeywordNode: LabelKeyword |label|",
                "  LabelListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void OneSemikeyword()
        {
            Assert.That("label Absolute;", ParsesAs(
                "LabelDeclSectionNode",
                "  LabelKeywordNode: LabelKeyword |label|",
                "  LabelListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Absolute|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Mixed()
        {
            Assert.That("label Answer, 42;", ParsesAs(
                "LabelDeclSectionNode",
                "  LabelKeywordNode: LabelKeyword |label|",
                "  LabelListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Answer|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Number |42|",
                "      DelimiterNode: (none)",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
