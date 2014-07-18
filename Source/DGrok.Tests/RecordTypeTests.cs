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
    public class RecordTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RecordType; }
        }

        [Test]
        public void EmptyRecord()
        {
            Assert.That("record end", ParsesAs(
                "RecordTypeNode",
                "  RecordKeywordNode: RecordKeyword |record|",
                "  ContentListNode: ListNode",
                "  VariantSectionNode: (none)",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Contents()
        {
            Assert.That("record procedure Foo; end", ParsesAs(
                "RecordTypeNode",
                "  RecordKeywordNode: RecordKeyword |record|",
                "  ContentListNode: ListNode",
                "    Items[0]: VisibilitySectionNode",
                "      VisibilityNode: (none)",
                "      ContentListNode: ListNode",
                "        Items[0]: MethodHeadingNode",
                "          ClassKeywordNode: (none)",
                "          MethodTypeNode: ProcedureKeyword |procedure|",
                "          NameNode: Identifier |Foo|",
                "          OpenParenthesisNode: (none)",
                "          ParameterListNode: ListNode",
                "          CloseParenthesisNode: (none)",
                "          ColonNode: (none)",
                "          ReturnTypeNode: (none)",
                "          DirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|",
                "  VariantSectionNode: (none)",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void VariantSection()
        {
            Assert.That("record case Integer of 1: (); end", ParsesAs(
                "RecordTypeNode",
                "  RecordKeywordNode: RecordKeyword |record|",
                "  ContentListNode: ListNode",
                "  VariantSectionNode: VariantSectionNode",
                "    CaseKeywordNode: CaseKeyword |case|",
                "    NameNode: (none)",
                "    ColonNode: (none)",
                "    TypeNode: Identifier |Integer|",
                "    OfKeywordNode: OfKeyword |of|",
                "    VariantGroupListNode: ListNode",
                "      Items[0]: VariantGroupNode",
                "        ValueListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Number |1|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        OpenParenthesisNode: OpenParenthesis |(|",
                "        FieldDeclListNode: ListNode",
                "        VariantSectionNode: (none)",
                "        CloseParenthesisNode: CloseParenthesis |)|",
                "        SemicolonNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
    }
}
