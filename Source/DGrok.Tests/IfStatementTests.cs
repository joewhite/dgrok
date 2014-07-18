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
    public class IfStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.IfStatement; }
        }

        [Test]
        public void EmptyThen()
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
        [Test]
        public void PopulatedThen()
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
        [Test]
        public void EmptyThenEmptyElse()
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
        [Test]
        public void PopulatedThenAndElse()
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
        [Test]
        public void IfThenIfThenElse()
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
        [Test]
        public void IfThenIfThenElseElse()
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
