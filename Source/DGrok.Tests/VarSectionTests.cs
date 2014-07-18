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
    public class VarSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VarSection; }
        }

        public void TestSimple()
        {
            Assert.That("var Foo: Integer;", ParsesAs(
                "VarSectionNode",
                "  Var: VarKeyword |var|",
                "  VarList: ListNode",
                "    Items[0]: VarDeclNode",
                "      Names: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      Absolute: (none)",
                "      AbsoluteAddress: (none)",
                "      EqualSign: (none)",
                "      Value: (none)",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestThreadVar()
        {
            Assert.That("threadvar Foo: Integer;", ParsesAs(
                "VarSectionNode",
                "  Var: ThreadVarKeyword |threadvar|",
                "  VarList: ListNode",
                "    Items[0]: VarDeclNode",
                "      Names: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      Absolute: (none)",
                "      AbsoluteAddress: (none)",
                "      EqualSign: (none)",
                "      Value: (none)",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestVarAloneDoesNotParse()
        {
            AssertDoesNotParse("var");
        }
    }
}
