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
    public class TypeSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypeSection; }
        }

        [Test]
        public void Types()
        {
            Assert.That("type TFoo = Integer; TBar = Byte;", ParsesAs(
                "TypeSectionNode",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeListNode: ListNode",
                "    Items[0]: TypeDeclNode",
                "      NameNode: Identifier |TFoo|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: TypeDeclNode",
                "      NameNode: Identifier |TBar|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Byte|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassForwardDeclaration()
        {
            Assert.That("type TFoo = class;", ParsesAs(
                "TypeSectionNode",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeListNode: ListNode",
                "    Items[0]: TypeForwardDeclarationNode",
                "      NameNode: Identifier |TFoo|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeNode: ClassKeyword |class|",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void BareTypeDoesNotParse()
        {
            AssertDoesNotParse("type");
        }
    }
}
