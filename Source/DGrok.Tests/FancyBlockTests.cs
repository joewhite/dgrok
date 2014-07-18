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
    public class FancyBlockTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FancyBlock; }
        }

        public void TestBlockOnly()
        {
            Assert.That("begin end", ParsesAs(
                "FancyBlockNode",
                "  DeclListNode: ListNode",
                "  BlockNode: BlockNode",
                "    BeginKeywordNode: BeginKeyword |begin|",
                "    StatementListNode: ListNode",
                "    EndKeywordNode: EndKeyword |end|"));
        }
        public void TestVarSection()
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
