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
                "  Name: Identifier |TFoo|",
                "  EqualSign: EqualSign |=|",
                "  TypeKeyword: (none)",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTypeType()
        {
            Assert.That("TFoo = type Integer;", ParsesAs(
                "TypeDeclNode",
                "  Name: Identifier |TFoo|",
                "  EqualSign: EqualSign |=|",
                "  TypeKeyword: TypeKeyword |type|",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("TFoo = Integer experimental platform;", ParsesAs(
                "TypeDeclNode",
                "  Name: Identifier |TFoo|",
                "  EqualSign: EqualSign |=|",
                "  TypeKeyword: (none)",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "    Items[0]: ExperimentalSemikeyword |experimental|",
                "    Items[1]: PlatformSemikeyword |platform|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestClassForwardDeclaration()
        {
            Assert.That("TFoo = class;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  Name: Identifier |TFoo|",
                "  EqualSign: EqualSign |=|",
                "  Type: ClassKeyword |class|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestDispInterfaceForwardDeclaration()
        {
            Assert.That("IFoo = dispinterface;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  Name: Identifier |IFoo|",
                "  EqualSign: EqualSign |=|",
                "  Type: DispInterfaceKeyword |dispinterface|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestInterfaceForwardDeclaration()
        {
            Assert.That("IFoo = interface;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  Name: Identifier |IFoo|",
                "  EqualSign: EqualSign |=|",
                "  Type: InterfaceKeyword |interface|",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
