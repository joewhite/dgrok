// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnitLite.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class TryStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TryStatement; }
        }

        public void TestEmptyTryFinally()
        {
            Assert.That("try finally end", ParsesAs(
                "TryFinallyNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Finally: FinallyKeyword |finally|",
                "  FinallyStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPopulatedTryFinally()
        {
            Assert.That("try Foo; Bar; finally Baz; Quux; end", ParsesAs(
                "TryFinallyNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  Finally: FinallyKeyword |finally|",
                "  FinallyStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Baz|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Quux|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyTryExcept()
        {
            Assert.That("try except end", ParsesAs(
                "TryExceptNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Except: ExceptKeyword |except|",
                "  ExceptionItemList: ListNode",
                "  Else: (none)",
                "  ElseStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPopulatedTryExceptWithDefaultHandler()
        {
            Assert.That("try Foo; Bar; except Baz; Quux; end", ParsesAs(
                "TryExceptNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  Except: ExceptKeyword |except|",
                "  ExceptionItemList: ListNode",
                "  Else: (none)",
                "  ElseStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Baz|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Quux|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
        public void TestTryExceptWithHandlers()
        {
            Assert.That("try except on Exception do; on EInvalidOp do; end", ParsesAs(
                "TryExceptNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Except: ExceptKeyword |except|",
                "  ExceptionItemList: ListNode",
                "    Items[0]: ExceptionItemNode",
                "      On: OnSemikeyword |on|",
                "      Name: (none)",
                "      Colon: (none)",
                "      Type: Identifier |Exception|",
                "      Do: DoKeyword |do|",
                "      Statement: (none)",
                "      Semicolon: Semicolon |;|",
                "    Items[1]: ExceptionItemNode",
                "      On: OnSemikeyword |on|",
                "      Name: (none)",
                "      Colon: (none)",
                "      Type: Identifier |EInvalidOp|",
                "      Do: DoKeyword |do|",
                "      Statement: (none)",
                "      Semicolon: Semicolon |;|",
                "  Else: (none)",
                "  ElseStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestTryExceptWithHandlerAndElse()
        {
            Assert.That("try except on EInvalidOp do; else Foo; Bar; end", ParsesAs(
                "TryExceptNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Except: ExceptKeyword |except|",
                "  ExceptionItemList: ListNode",
                "    Items[0]: ExceptionItemNode",
                "      On: OnSemikeyword |on|",
                "      Name: (none)",
                "      Colon: (none)",
                "      Type: Identifier |EInvalidOp|",
                "      Do: DoKeyword |do|",
                "      Statement: (none)",
                "      Semicolon: Semicolon |;|",
                "  Else: ElseKeyword |else|",
                "  ElseStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
        public void TestTryExceptWithOnlyElse()
        {
            Assert.That("try except else Foo; Bar; end", ParsesAs(
                "TryExceptNode",
                "  Try: TryKeyword |try|",
                "  TryStatements: ListNode",
                "  Except: ExceptKeyword |except|",
                "  ExceptionItemList: ListNode",
                "  Else: ElseKeyword |else|",
                "  ElseStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
    }
}
