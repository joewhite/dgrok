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
    public class TryStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TryStatement; }
        }

        [Test]
        public void EmptyTryFinally()
        {
            Assert.That("try finally end", ParsesAs(
                "TryFinallyNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  FinallyKeywordNode: FinallyKeyword |finally|",
                "  FinallyStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void PopulatedTryFinally()
        {
            Assert.That("try Foo; Bar; finally Baz; Quux; end", ParsesAs(
                "TryFinallyNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  FinallyKeywordNode: FinallyKeyword |finally|",
                "  FinallyStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Baz|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Quux|",
                "      DelimiterNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void EmptyTryExcept()
        {
            Assert.That("try except end", ParsesAs(
                "TryExceptNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  ExceptKeywordNode: ExceptKeyword |except|",
                "  ExceptionItemListNode: ListNode",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void PopulatedTryExceptWithDefaultHandler()
        {
            Assert.That("try Foo; Bar; except Baz; Quux; end", ParsesAs(
                "TryExceptNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Bar|",
                "      DelimiterNode: Semicolon |;|",
                "  ExceptKeywordNode: ExceptKeyword |except|",
                "  ExceptionItemListNode: ListNode",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Baz|",
                "      DelimiterNode: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Quux|",
                "      DelimiterNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void TryExceptWithHandlers()
        {
            Assert.That("try except on Exception do; on EInvalidOp do; end", ParsesAs(
                "TryExceptNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  ExceptKeywordNode: ExceptKeyword |except|",
                "  ExceptionItemListNode: ListNode",
                "    Items[0]: ExceptionItemNode",
                "      OnSemikeywordNode: OnSemikeyword |on|",
                "      NameNode: (none)",
                "      ColonNode: (none)",
                "      TypeNode: Identifier |Exception|",
                "      DoKeywordNode: DoKeyword |do|",
                "      StatementNode: (none)",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: ExceptionItemNode",
                "      OnSemikeywordNode: OnSemikeyword |on|",
                "      NameNode: (none)",
                "      ColonNode: (none)",
                "      TypeNode: Identifier |EInvalidOp|",
                "      DoKeywordNode: DoKeyword |do|",
                "      StatementNode: (none)",
                "      SemicolonNode: Semicolon |;|",
                "  ElseKeywordNode: (none)",
                "  ElseStatementListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void TryExceptWithHandlerAndElse()
        {
            Assert.That("try except on EInvalidOp do; else Foo; Bar; end", ParsesAs(
                "TryExceptNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  ExceptKeywordNode: ExceptKeyword |except|",
                "  ExceptionItemListNode: ListNode",
                "    Items[0]: ExceptionItemNode",
                "      OnSemikeywordNode: OnSemikeyword |on|",
                "      NameNode: (none)",
                "      ColonNode: (none)",
                "      TypeNode: Identifier |EInvalidOp|",
                "      DoKeywordNode: DoKeyword |do|",
                "      StatementNode: (none)",
                "      SemicolonNode: Semicolon |;|",
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
        [Test]
        public void TryExceptWithOnlyElse()
        {
            Assert.That("try except else Foo; Bar; end", ParsesAs(
                "TryExceptNode",
                "  TryKeywordNode: TryKeyword |try|",
                "  TryStatementListNode: ListNode",
                "  ExceptKeywordNode: ExceptKeyword |except|",
                "  ExceptionItemListNode: ListNode",
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
