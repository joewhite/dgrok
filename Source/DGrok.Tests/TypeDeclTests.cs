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
    public class TypeDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypeDecl; }
        }

        public void TestSimple()
        {
            Assert.That("TFoo = Integer;", ParsesAs(
                "TypeDeclNode",
                "  NameNode: Identifier |TFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeKeywordNode: (none)",
                "  TypeNode: Identifier |Integer|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestTypeType()
        {
            Assert.That("TFoo = type Integer;", ParsesAs(
                "TypeDeclNode",
                "  NameNode: Identifier |TFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeNode: Identifier |Integer|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("TFoo = Integer experimental platform;", ParsesAs(
                "TypeDeclNode",
                "  NameNode: Identifier |TFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeKeywordNode: (none)",
                "  TypeNode: Identifier |Integer|",
                "  PortabilityDirectiveListNode: ListNode",
                "    Items[0]: ExperimentalSemikeyword |experimental|",
                "    Items[1]: PlatformSemikeyword |platform|",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestClassForwardDeclaration()
        {
            Assert.That("TFoo = class;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  NameNode: Identifier |TFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeNode: ClassKeyword |class|",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestDispInterfaceForwardDeclaration()
        {
            Assert.That("IFoo = dispinterface;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  NameNode: Identifier |IFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeNode: DispInterfaceKeyword |dispinterface|",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestInterfaceForwardDeclaration()
        {
            Assert.That("IFoo = interface;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  NameNode: Identifier |IFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeNode: InterfaceKeyword |interface|",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
