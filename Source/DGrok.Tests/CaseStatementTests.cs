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
    public class CaseStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.CaseStatement; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("case Foo of 1: end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void EmptyElse()
        {
            Assert.That("case Foo of 1: else end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void PopulatedElse()
        {
            Assert.That("case Foo of 1: else Foo; Bar; end", ParsesAs(
                "CaseStatementNode",
                "  CaseKeywordNode: CaseKeyword |case|",
                "  ExpressionNode: Identifier |Foo|",
                "  OfKeywordNode: OfKeyword |of|",
                "  SelectorListNode: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      ValueListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Number |1|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      StatementNode: (none)",
                "      SemicolonNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
    }
}
