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
    public class ForStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ForStatement; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("for I := 1 to 42 do", ParsesAs(
                "ForStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |I|",
                "  ColonEqualsNode: ColonEquals |:=|",
                "  StartingValueNode: Number |1|",
                "  DirectionNode: ToKeyword |to|",
                "  EndingValueNode: Number |42|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        [Test]
        public void DownTo()
        {
            Assert.That("for I := 1 downto 42 do", ParsesAs(
                "ForStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |I|",
                "  ColonEqualsNode: ColonEquals |:=|",
                "  StartingValueNode: Number |1|",
                "  DirectionNode: DownToKeyword |downto|",
                "  EndingValueNode: Number |42|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        [Test]
        public void Statement()
        {
            Assert.That("for I := 1 to 42 do Foo", ParsesAs(
                "ForStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |I|",
                "  ColonEqualsNode: ColonEquals |:=|",
                "  StartingValueNode: Number |1|",
                "  DirectionNode: ToKeyword |to|",
                "  EndingValueNode: Number |42|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Foo|"));
        }
        [Test]
        public void ForIn()
        {
            Assert.That("for Obj in List do", ParsesAs(
                "ForInStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |Obj|",
                "  InKeywordNode: InKeyword |in|",
                "  ExpressionNode: Identifier |List|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: (none)"));
        }
        [Test]
        public void ForInWithStatement()
        {
            Assert.That("for Obj in List do Foo", ParsesAs(
                "ForInStatementNode",
                "  ForKeywordNode: ForKeyword |for|",
                "  LoopVariableNode: Identifier |Obj|",
                "  InKeywordNode: InKeyword |in|",
                "  ExpressionNode: Identifier |List|",
                "  DoKeywordNode: DoKeyword |do|",
                "  StatementNode: Identifier |Foo|"));
        }
    }
}
