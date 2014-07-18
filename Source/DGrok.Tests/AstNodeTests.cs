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
                "  LeftNode: Number |6|\r\n" +
                "  OperatorNode: TimesSign |*|\r\n" +
                "  RightNode: Number |9|"));
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
                "  LeftNode: BinaryOperationNode\r\n" +
                "    LeftNode: Number |2|\r\n" +
                "    OperatorNode: TimesSign |*|\r\n" +
                "    RightNode: Number |3|\r\n" +
                "  OperatorNode: TimesSign |*|\r\n" +
                "  RightNode: Number |9|"));
        }
        public void TestToCodeWithToken()
        {
            AstNode token = Parse("Foo", RuleType.Expression);
            Assert.That(token.ToCode(), Is.EqualTo("Foo"));
        }
        public void TestToCodeWithBinaryOperation()
        {
            AstNode parseTree = Parse("Foo.Bar", RuleType.Expression);
            Assert.That(parseTree.ToCode(), Is.EqualTo("Foo.Bar"));
        }
        public void TestToCodeWithComplexTree()
        {
            AstNode parseTree = Parse("(1 + 2) * (3 - 4)", RuleType.Expression);
            Assert.That(parseTree.ToCode(), Is.EqualTo("(1 + 2) * (3 - 4)"));
        }
        public void TestToCodeWithFirstChildNull()
        {
            AstNode parseTree = Parse("procedure Foo;", RuleType.MethodHeading);
            Assert.That(parseTree.ToCode(), Is.EqualTo("procedure Foo;"));
        }
        public void TestToCodeWithLastChildNull()
        {
            AstNode parseTree = Parse("42:", RuleType.Statement);
            Assert.That(parseTree.ToCode(), Is.EqualTo("42:"));
        }
        public void TestParentNodeOfType()
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
