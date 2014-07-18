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
    public class ConstSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ConstSection; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("const Foo = 24; Bar = 42;", ParsesAs(
                "ConstSectionNode",
                "  ConstKeywordNode: ConstKeyword |const|",
                "  ConstListNode: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      NameNode: Identifier |Foo|",
                "      ColonNode: (none)",
                "      TypeNode: (none)",
                "      EqualSignNode: EqualSign |=|",
                "      ValueNode: Number |24|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: ConstantDeclNode",
                "      NameNode: Identifier |Bar|",
                "      ColonNode: (none)",
                "      TypeNode: (none)",
                "      EqualSignNode: EqualSign |=|",
                "      ValueNode: Number |42|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ResourceString()
        {
            Assert.That("resourcestring Foo = 'Bar';", ParsesAs(
                "ConstSectionNode",
                "  ConstKeywordNode: ResourceStringKeyword |resourcestring|",
                "  ConstListNode: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      NameNode: Identifier |Foo|",
                "      ColonNode: (none)",
                "      TypeNode: (none)",
                "      EqualSignNode: EqualSign |=|",
                "      ValueNode: StringLiteral |'Bar'|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ConstAloneDoesNotParse()
        {
            AssertDoesNotParse("const");
        }
    }
}
