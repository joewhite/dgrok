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
                "  Modifier: (none)",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TBar|",
                "  EqualSign: (none)",
                "  DefaultValue: (none)"));
        }
        public void TestTwo()
        {
            Assert.That("Foo, Bar: TBaz", ParsesAs(
                "ParameterNode",
                "  Modifier: (none)",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TBaz|",
                "  EqualSign: (none)",
                "  DefaultValue: (none)"));
        }
        public void TestVarParameter()
        {
            Assert.That("var Foo: TBar", ParsesAs(
                "ParameterNode",
                "  Modifier: VarKeyword |var|",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TBar|",
                "  EqualSign: (none)",
                "  DefaultValue: (none)"));
        }
        public void TestConstParameter()
        {
            Assert.That("const Foo: TBar", ParsesAs(
                "ParameterNode",
                "  Modifier: ConstKeyword |const|",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TBar|",
                "  EqualSign: (none)",
                "  DefaultValue: (none)"));
        }
        public void TestOutParameter()
        {
            Assert.That("out Foo: TBar", ParsesAs(
                "ParameterNode",
                "  Modifier: OutSemikeyword |out|",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TBar|",
                "  EqualSign: (none)",
                "  DefaultValue: (none)"));
        }
        public void TestUntypedVar()
        {
            Assert.That("var Foo", ParsesAs(
                "ParameterNode",
                "  Modifier: VarKeyword |var|",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: (none)",
                "  Type: (none)",
                "  EqualSign: (none)",
                "  DefaultValue: (none)"));
        }
        public void TestDefaultParameter()
        {
            Assert.That("Foo: TBar = 42", ParsesAs(
                "ParameterNode",
                "  Modifier: (none)",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TBar|",
                "  EqualSign: EqualSign |=|",
                "  DefaultValue: Number |42|"));
        }
    }
}
