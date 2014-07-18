// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.DelphiNodes;
using DGrok.Framework;
using NUnitLite.Framework;

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

        public void TestParseRuleSetsParentReferences()
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
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestCanParseRuleThrowsOnUnrecognizedRule()
        {
            _parser.CanParseRule((RuleType) 999);
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestParseRuleThrowsOnUnrecognizedRule()
        {
            _parser.ParseRule((RuleType) 999);
        }
    }
}
