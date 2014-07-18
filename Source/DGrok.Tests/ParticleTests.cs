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
    public class ParticleTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Particle; }
        }

        [Test]
        public void Nil()
        {
            Assert.That("nil", ParsesAs("NilKeyword |nil|"));
        }
        [Test]
        public void StringLiteral()
        {
            Assert.That("'Foo'", ParsesAs("StringLiteral |'Foo'|"));
        }
        [Test]
        public void Number()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void Identifier()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        [Test]
        public void Semikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
        [Test]
        public void ParenthesizedExpression()
        {
            Assert.That("(Foo)", ParsesAs(
                "ParenthesizedExpressionNode",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  ExpressionNode: Identifier |Foo|",
                "  CloseParenthesisNode: CloseParenthesis |)|"));
        }
        [Test]
        public void SetLiteral()
        {
            Assert.That("[1, 3..4]", ParsesAs(
                "SetLiteralNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ItemListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Number |1|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Number |3|",
                "        OperatorNode: DotDot |..|",
                "        RightNode: Number |4|",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|"));
        }
        [Test]
        public void StringKeyword()
        {
            Assert.That("string", ParsesAs("StringKeyword |string|"));
        }
        [Test]
        public void FileKeyword()
        {
            Assert.That("file", ParsesAs("FileKeyword |file|"));
        }
    }
}
