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
    public class FancyBlockTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FancyBlock; }
        }

        [Test]
        public void BlockOnly()
        {
            Assert.That("begin end", ParsesAs(
                "FancyBlockNode",
                "  DeclListNode: ListNode",
                "  BlockNode: BlockNode",
                "    BeginKeywordNode: BeginKeyword |begin|",
                "    StatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void VarSection()
        {
            Assert.That("var Foo: Integer; begin end", ParsesAs(
                "FancyBlockNode",
                "  DeclListNode: ListNode",
                "    Items[0]: VarSectionNode",
                "      VarKeywordNode: VarKeyword |var|",
                "      VarListNode: ListNode",
                "        Items[0]: VarDeclNode",
                "          NameListNode: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              ItemNode: Identifier |Foo|",
                "              DelimiterNode: (none)",
                "          ColonNode: Colon |:|",
                "          TypeNode: Identifier |Integer|",
                "          FirstPortabilityDirectiveListNode: ListNode",
                "          AbsoluteSemikeywordNode: (none)",
                "          AbsoluteAddressNode: (none)",
                "          EqualSignNode: (none)",
                "          ValueNode: (none)",
                "          SecondPortabilityDirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|",
                "  BlockNode: BlockNode",
                "    BeginKeywordNode: BeginKeyword |begin|",
                "    StatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|"));
        }
    }
}
