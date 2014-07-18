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
    public class SimpleExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SimpleExpression; }
        }

        [Test]
        public void Number()
        {
            Assert.That("42", ParsesAs("Number |42|"));
        }
        [Test]
        public void Term()
        {
            Assert.That("6 * 9", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |6|",
                "  OperatorNode: TimesSign |*|",
                "  RightNode: Number |9|"));
        }
        [Test]
        public void Add()
        {
            Assert.That("40 + 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |40|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Number |2|"));
        }
        [Test]
        public void TwoAdds()
        {
            Assert.That("30 + 10 + 2", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Number |30|",
                "    OperatorNode: PlusSign |+|",
                "    RightNode: Number |10|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Number |2|"));
        }
        [Test]
        public void Minus()
        {
            Assert.That("50 - 8", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Number |50|",
                "  OperatorNode: MinusSign |-|",
                "  RightNode: Number |8|"));
        }
        [Test]
        public void OrderOfOperations()
        {
            Assert.That("1 * 2 + 3 * 4", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: BinaryOperationNode",
                "    LeftNode: Number |1|",
                "    OperatorNode: TimesSign |*|",
                "    RightNode: Number |2|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: BinaryOperationNode",
                "    LeftNode: Number |3|",
                "    OperatorNode: TimesSign |*|",
                "    RightNode: Number |4|"));
        }
        [Test]
        public void Semikeywords()
        {
            Assert.That("Absolute + Index", ParsesAs(
                "BinaryOperationNode",
                "  LeftNode: Identifier |Absolute|",
                "  OperatorNode: PlusSign |+|",
                "  RightNode: Identifier |Index|"));
        }
    }
}
