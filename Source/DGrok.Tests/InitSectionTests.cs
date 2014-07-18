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
    public class InitSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.InitSection; }
        }

        [Test]
        public void End()
        {
            Assert.That("end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: (none)",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void EmptyInitialization()
        {
            Assert.That("initialization end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void EmptyInitializationFinalization()
        {
            Assert.That("initialization finalization end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: FinalizationKeyword |finalization|",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void PopulatedInitialization()
        {
            Assert.That("initialization Foo; Bar; end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void PopulatedInitializationFinalization()
        {
            Assert.That("initialization Foo; Bar; finalization Baz; Quux; end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: InitializationKeyword |initialization|",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinalizationKeywordNode: FinalizationKeyword |finalization|",
                "  FinalizationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Baz|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Quux|",
                "      DelimiterNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void EmptyBlock()
        {
            Assert.That("begin end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: BeginKeyword |begin|",
                "  InitializationStatementListNode: ListNode",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void PopulatedBlock()
        {
            Assert.That("begin Foo; Bar; end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: BeginKeyword |begin|",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void AsmBlock()
        {
            Assert.That("asm end", ParsesAs(
                "InitSectionNode",
                "  InitializationKeywordNode: (none)",
                "  InitializationStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: AssemblerStatementNode",
                "        AsmKeywordNode: AsmKeyword |asm|",
                "        EndKeywordNode: EndKeyword |end|",
                "      DelimiterNode: (none)",
                "  FinalizationKeywordNode: (none)",
                "  FinalizationStatementListNode: ListNode",
                "  EndKeywordNode: (none)"));
        }
    }
}
