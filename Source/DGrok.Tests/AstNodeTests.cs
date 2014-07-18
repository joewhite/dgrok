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
    public class AstNodeTests
    {
        private Token MakeToken(TokenType type, string text)
        {
            return new Token(type, 0, text);
        }

        public void TestInspectToken()
        {
            AstNode token = MakeToken(TokenType.Number, "42");
            Assert.That(token.Inspect(), Is.EqualTo("Number |42|"));
        }
        public void TestInspectBinaryOperationNode()
        {
            AstNode left = MakeToken(TokenType.Number, "6");
            AstNode op = MakeToken(TokenType.TimesSign, "*");
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
            AstNode leftOp = MakeToken(TokenType.TimesSign, "*");
            AstNode leftRight = MakeToken(TokenType.Number, "3");
            AstNode op = MakeToken(TokenType.TimesSign, "*");
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
    }
}
