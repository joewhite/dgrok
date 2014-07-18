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
    public class AstNodeTests
    {
        private Token MakeToken(TokenType type, string text)
        {
            return new Token(type, new Location("", "", 0), text, "");
        }
        private AstNode Parse(string text, RuleType ruleType)
        {
            Parser parser = Parser.FromText(text, "", CompilerDefines.CreateEmpty(), new MemoryFileLoader());
            return parser.ParseRule(ruleType);
        }

        public void TestInspectToken()
        {
            AstNode token = MakeToken(TokenType.Number, "42");
            Assert.That(token.Inspect(), Is.EqualTo("Number |42|"));
        }
        public void TestInspectBinaryOperationNode()
        {
            AstNode left = MakeToken(TokenType.Number, "6");
            Token op = MakeToken(TokenType.TimesSign, "*");
            AstNode right = MakeToken(TokenType.Number, "9");
            AstNode node = new BinaryOperationNode(left, op, right);
            Assert.That(node.Inspect(), Is.EqualTo(
                "BinaryOperationNode\r\n" +
                "  Left: Number |6|\r\n" +
                "  Operator: TimesSign |*|\r\n" +
                "  Right: Number |9|"));
        }
        public void TestInspectNestedBinaryOperationNodes()
        {
            AstNode leftLeft = MakeToken(TokenType.Number, "2");
            Token leftOp = MakeToken(TokenType.TimesSign, "*");
            AstNode leftRight = MakeToken(TokenType.Number, "3");
            Token op = MakeToken(TokenType.TimesSign, "*");
            AstNode right = MakeToken(TokenType.Number, "9");
            AstNode left = new BinaryOperationNode(leftLeft, leftOp, leftRight);
            AstNode node = new BinaryOperationNode(left, op, right);
            Assert.That(node.Inspect(), Is.EqualTo(
                "BinaryOperationNode\r\n" +
                "  Left: BinaryOperationNode\r\n" +
                "    Left: Number |2|\r\n" +
                "    Operator: TimesSign |*|\r\n" +
                "    Right: Number |3|\r\n" +
                "  Operator: TimesSign |*|\r\n" +
                "  Right: Number |9|"));
        }
        public void TestTokenToCode()
        {
            AstNode token = Parse("Foo", RuleType.Expression);
            Assert.That(token.ToCode(), Is.EqualTo("Foo"));
        }
        public void TestBinaryOperationToCode()
        {
            AstNode parseTree = Parse("Foo.Bar", RuleType.Expression);
            Assert.That(parseTree.ToCode(), Is.EqualTo("Foo.Bar"));
        }
        public void TestComplexTree()
        {
            AstNode parseTree = Parse("(1 + 2) * (3 - 4)", RuleType.Expression);
            Assert.That(parseTree.ToCode(), Is.EqualTo("(1 + 2) * (3 - 4)"));
        }
        public void TestFirstChildIsNull()
        {
            AstNode parseTree = Parse("procedure Foo;", RuleType.MethodHeading);
            Assert.That(parseTree.ToCode(), Is.EqualTo("procedure Foo;"));
        }
        public void TestLastChildIsNull()
        {
            AstNode parseTree = Parse("42:", RuleType.Statement);
            Assert.That(parseTree.ToCode(), Is.EqualTo("42:"));
        }
    }
}
