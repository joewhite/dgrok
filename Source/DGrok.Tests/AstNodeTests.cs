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

        [Test]
        public void InspectToken()
        {
            AstNode token = MakeToken(TokenType.Number, "42");
            Assert.That(token.Inspect(), Is.EqualTo("Number |42|"));
        }
        [Test]
        public void InspectBinaryOperationNode()
        {
            AstNode left = MakeToken(TokenType.Number, "6");
            Token op = MakeToken(TokenType.TimesSign, "*");
            AstNode right = MakeToken(TokenType.Number, "9");
            AstNode node = new BinaryOperationNode(left, op, right);
            Assert.That(node.Inspect(), Is.EqualTo(
                "BinaryOperationNode\r\n" +
                "  LeftNode: Number |6|\r\n" +
                "  OperatorNode: TimesSign |*|\r\n" +
                "  RightNode: Number |9|"));
        }
        [Test]
        public void InspectNestedBinaryOperationNodes()
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
                "  LeftNode: BinaryOperationNode\r\n" +
                "    LeftNode: Number |2|\r\n" +
                "    OperatorNode: TimesSign |*|\r\n" +
                "    RightNode: Number |3|\r\n" +
                "  OperatorNode: TimesSign |*|\r\n" +
                "  RightNode: Number |9|"));
        }
        [Test]
        public void ToCodeWithToken()
        {
            AstNode token = Parse("Foo", RuleType.Expression);
            Assert.That(token.ToCode(), Is.EqualTo("Foo"));
        }
        [Test]
        public void ToCodeWithBinaryOperation()
        {
            AstNode parseTree = Parse("Foo.Bar", RuleType.Expression);
            Assert.That(parseTree.ToCode(), Is.EqualTo("Foo.Bar"));
        }
        [Test]
        public void ToCodeWithComplexTree()
        {
            AstNode parseTree = Parse("(1 + 2) * (3 - 4)", RuleType.Expression);
            Assert.That(parseTree.ToCode(), Is.EqualTo("(1 + 2) * (3 - 4)"));
        }
        [Test]
        public void ToCodeWithFirstChildNull()
        {
            AstNode parseTree = Parse("procedure Foo;", RuleType.MethodHeading);
            Assert.That(parseTree.ToCode(), Is.EqualTo("procedure Foo;"));
        }
        [Test]
        public void ToCodeWithLastChildNull()
        {
            AstNode parseTree = Parse("42:", RuleType.Statement);
            Assert.That(parseTree.ToCode(), Is.EqualTo("42:"));
        }
        [Test]
        public void ParentNodeOfType()
        {
            VarSectionNode varSectionNode =
                (VarSectionNode) Parse("var Foo: record Bar: Integer; end;", RuleType.VarSection);
            RecordTypeNode recordNode =
                (RecordTypeNode) varSectionNode.VarListNode.Items[0].TypeNode;
            VisibilitySectionNode visibilitySectionNode =
                recordNode.ContentListNode.Items[0];
            FieldSectionNode fieldSectionNode =
                (FieldSectionNode) visibilitySectionNode.ContentListNode.Items[0];
            FieldDeclNode barFieldNode =
                fieldSectionNode.FieldListNode.Items[0];
            Token barNameNode = barFieldNode.NameListNode.Items[0].ItemNode;

            Assert.That(barNameNode.ParentNodeOfType<FieldDeclNode>(), Is.SameAs(barFieldNode));
            Assert.That(barNameNode.ParentNodeOfType<FieldSectionNode>(), Is.SameAs(fieldSectionNode));
            Assert.That(barNameNode.ParentNodeOfType<VisibilitySectionNode>(), Is.SameAs(visibilitySectionNode));
            Assert.That(barNameNode.ParentNodeOfType<RecordTypeNode>(), Is.SameAs(recordNode));
            Assert.That(barNameNode.ParentNodeOfType<VarSectionNode>(), Is.SameAs(varSectionNode));
        }
    }
}
