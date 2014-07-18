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
    public class ExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Expression; }
        }

        [Test]
        public void Number()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void SimpleExpression()
        {
            Assert.That("40 + 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |40|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Number |2|"));
        }
        [Test]
        public void Equals()
        {
            Assert.That("A = B", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |A|",
                "  OperatorNode: EqualSign |=|",
                "  RightNode: Identifier |B|"));
        }
        [Test]
        public void TwoEquals()
        {
            Assert.That("A = B = C", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Identifier |A|",
                "    OperatorNode: EqualSign |=|",
                "    RightNode: Identifier |B|",
                "  OperatorNode: EqualSign |=|",
                "  RightNode: Identifier |C|"));
        }
    }
}
