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
            Assert.That("Foo = Integer;", ParsesAs(
                "TypeDeclNode",
                "  Name: Identifier |Foo|",
                "  EqualSign: EqualSign |=|",
                "  TypeKeyword: (none)",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTypeType()
        {
            Assert.That("Foo = type Integer;", ParsesAs(
                "TypeDeclNode",
                "  Name: Identifier |Foo|",
                "  EqualSign: EqualSign |=|",
                "  TypeKeyword: TypeKeyword |type|",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("Foo = Integer experimental platform;", ParsesAs(
                "TypeDeclNode",
                "  Name: Identifier |Foo|",
                "  EqualSign: EqualSign |=|",
                "  TypeKeyword: (none)",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "    Items[0]: ExperimentalSemikeyword |experimental|",
                "    Items[1]: PlatformSemikeyword |platform|",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
