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
                "  DeclList: ListNode",
                "  Block: BlockNode",
                "    Begin: BeginKeyword |begin|",
                "    StatementList: ListNode",
                "    End: EndKeyword |end|"));
        }
        public void TestVarSection()
        {
            Assert.That("var Foo: Integer; begin end", ParsesAs(
                "FancyBlockNode",
                "  DeclList: ListNode",
                "    Items[0]: VarSectionNode",
                "      Var: VarKeyword |var|",
                "      VarList: ListNode",
                "        Items[0]: VarDeclNode",
                "          Names: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              Item: Identifier |Foo|",
                "              Delimiter: (none)",
                "          Colon: Colon |:|",
                "          Type: Identifier |Integer|",
                "          Absolute: (none)",
                "          AbsoluteAddress: (none)",
                "          EqualSign: (none)",
                "          Value: (none)",
                "          PortabilityDirectiveList: ListNode",
                "          Semicolon: Semicolon |;|",
                "  Block: BlockNode",
                "    Begin: BeginKeyword |begin|",
                "    StatementList: ListNode",
                "    End: EndKeyword |end|"));
        }
    }
}
