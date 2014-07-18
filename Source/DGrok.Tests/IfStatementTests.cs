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
    public class IfStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.IfStatement; }
        }

        public void TestEmptyThen()
        {
            Assert.That("if Foo then", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: (none)",
                "  Else: (none)",
                "  ElseStatement: (none)"));
        }
        public void TestPopulatedThen()
        {
            Assert.That("if Foo then Bar", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: Identifier |Bar|",
                "  Else: (none)",
                "  ElseStatement: (none)"));
        }
        public void TestEmptyThenEmptyElse()
        {
            Assert.That("if Foo then else", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: (none)",
                "  Else: ElseKeyword |else|",
                "  ElseStatement: (none)"));
        }
        public void TestPopulatedThenAndElse()
        {
            Assert.That("if Foo then Bar else Baz", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: Identifier |Bar|",
                "  Else: ElseKeyword |else|",
                "  ElseStatement: Identifier |Baz|"));
        }
        public void TestIfThenIfThenElse()
        {
            Assert.That("if Foo then if Bar then else", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: IfStatementNode",
                "    If: IfKeyword |if|",
                "    Condition: Identifier |Bar|",
                "    Then: ThenKeyword |then|",
                "    ThenStatement: (none)",
                "    Else: ElseKeyword |else|",
                "    ElseStatement: (none)",
                "  Else: (none)",
                "  ElseStatement: (none)"));
        }
        public void TestIfThenIfThenElseElse()
        {
            Assert.That("if Foo then if Bar then else else", ParsesAs(
                "IfStatementNode",
                "  If: IfKeyword |if|",
                "  Condition: Identifier |Foo|",
                "  Then: ThenKeyword |then|",
                "  ThenStatement: IfStatementNode",
                "    If: IfKeyword |if|",
                "    Condition: Identifier |Bar|",
                "    Then: ThenKeyword |then|",
                "    ThenStatement: (none)",
                "    Else: ElseKeyword |else|",
                "    ElseStatement: (none)",
                "  Else: ElseKeyword |else|",
                "  ElseStatement: (none)"));
        }
    }
}
