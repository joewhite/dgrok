// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
