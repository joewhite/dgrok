// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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
