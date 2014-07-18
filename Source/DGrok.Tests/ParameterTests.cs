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
    public class ParameterTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Parameter; }
        }

        public void TestOne()
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
        public void TestTwo()
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
        public void TestVarParameter()
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
        public void TestConstParameter()
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
        public void TestOutParameter()
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
        public void TestUntypedVar()
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
        public void TestDefaultParameter()
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
