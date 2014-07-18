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
