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
using DGrok.DelphiNodes;
using DGrok.Framework;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private Parser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = Parser.FromText("Foo.Bar.Baz", "",
                CompilerDefines.CreateEmpty(), new MemoryFileLoader());
        }

        [Test]
        public void ParseRuleSetsParentReferences()
        {
            BinaryOperationNode top = (BinaryOperationNode) _parser.ParseRule(RuleType.Expression);
            Assert.That(top.LeftNode.ParentNode, Is.SameAs(top), "Top.Left");
            Assert.That(top.OperatorNode.ParentNode, Is.SameAs(top), "Top.Operator");
            Assert.That(top.RightNode.ParentNode, Is.SameAs(top), "Top.Right");
            BinaryOperationNode left = (BinaryOperationNode) top.LeftNode;
            Assert.That(left.LeftNode.ParentNode, Is.SameAs(left), "Left.Left");
            Assert.That(left.OperatorNode.ParentNode, Is.SameAs(left), "Left.Operator");
            Assert.That(left.RightNode.ParentNode, Is.SameAs(left), "Left.Right");
        }
        [Test, ExpectedException(typeof(IndexOutOfRangeException))]
        public void CanParseRuleThrowsOnUnrecognizedRule()
        {
            _parser.CanParseRule((RuleType) 999);
        }
        [Test, ExpectedException(typeof(IndexOutOfRangeException))]
        public void ParseRuleThrowsOnUnrecognizedRule()
        {
            _parser.ParseRule((RuleType) 999);
        }
    }
}
