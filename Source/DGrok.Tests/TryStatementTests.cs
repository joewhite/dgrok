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
