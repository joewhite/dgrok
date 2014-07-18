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
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        public void TestPopulatedThen()
        {
            Assert.That("if Foo then Bar", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: Identifier |Bar|",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        public void TestEmptyThenEmptyElse()
        {
            Assert.That("if Foo then else", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementNode: (none)"));
        }
        public void TestPopulatedThenAndElse()
        {
            Assert.That("if Foo then Bar else Baz", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: Identifier |Bar|",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementNode: Identifier |Baz|"));
        }
        public void TestIfThenIfThenElse()
        {
            Assert.That("if Foo then if Bar then else", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: IfStatementNode",
                "    IfKeywordNode: IfKeyword |if|",
                "    ConditionNode: Identifier |Bar|",
                "    ThenKeywordNode: ThenKeyword |then|",
                "    ThenStatementNode: (none)",
                "    ElseKeywordNode: ElseKeyword |else|",
                "    ElseStatementNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        public void TestIfThenIfThenElseElse()
        {
            Assert.That("if Foo then if Bar then else else", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: IfStatementNode",
                "    IfKeywordNode: IfKeyword |if|",
                "    ConditionNode: Identifier |Bar|",
                "    ThenKeywordNode: ThenKeyword |then|",
                "    ThenStatementNode: (none)",
                "    ElseKeywordNode: ElseKeyword |else|",
                "    ElseStatementNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementNode: (none)"));
        }
    }
}
