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
    public class ParameterTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Parameter; }
        }

        [Test]
        public void One()
        {
            Assert.That("Foo: TBar", ParsesAs(
                "ParameterNode",
                "  ModifierNode: (none)",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TBar|",
                "  EqualSignNode: (none)",
                "  DefaultValueNode: (none)"));
        }
        [Test]
        public void Two()
        {
            Assert.That("Foo, Bar: TBaz", ParsesAs(
                "ParameterNode",
                "  ModifierNode: (none)",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TBaz|",
                "  EqualSignNode: (none)",
                "  DefaultValueNode: (none)"));
        }
        [Test]
        public void VarParameter()
        {
            Assert.That("var Foo: TBar", ParsesAs(
                "ParameterNode",
                "  ModifierNode: VarKeyword |var|",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TBar|",
                "  EqualSignNode: (none)",
                "  DefaultValueNode: (none)"));
        }
        [Test]
        public void ConstParameter()
        {
            Assert.That("const Foo: TBar", ParsesAs(
                "ParameterNode",
                "  ModifierNode: ConstKeyword |const|",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TBar|",
                "  EqualSignNode: (none)",
                "  DefaultValueNode: (none)"));
        }
        [Test]
        public void OutParameter()
        {
            Assert.That("out Foo: TBar", ParsesAs(
                "ParameterNode",
                "  ModifierNode: OutSemikeyword |out|",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TBar|",
                "  EqualSignNode: (none)",
                "  DefaultValueNode: (none)"));
        }
        [Test]
        public void UntypedVar()
        {
            Assert.That("var Foo", ParsesAs(
                "ParameterNode",
                "  ModifierNode: VarKeyword |var|",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: (none)",
                "  TypeNode: (none)",
                "  EqualSignNode: (none)",
                "  DefaultValueNode: (none)"));
        }
        [Test]
        public void DefaultParameter()
        {
            Assert.That("Foo: TBar = 42", ParsesAs(
                "ParameterNode",
                "  ModifierNode: (none)",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TBar|",
                "  EqualSignNode: EqualSign |=|",
                "  DefaultValueNode: Number |42|"));
        }
    }
}
