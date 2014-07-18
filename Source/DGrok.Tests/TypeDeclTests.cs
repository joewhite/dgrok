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
    public class TypeDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypeDecl; }
        }

        [Test]
        public void Simple()
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
        [Test]
        public void TypeType()
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
        [Test]
        public void PortabilityDirectives()
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
        [Test]
        public void ClassForwardDeclaration()
        {
            Assert.That("TFoo = class;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  NameNode: Identifier |TFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeNode: ClassKeyword |class|",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void DispInterfaceForwardDeclaration()
        {
            Assert.That("IFoo = dispinterface;", ParsesAs(
                "TypeForwardDeclarationNode",
                "  NameNode: Identifier |IFoo|",
                "  EqualSignNode: EqualSign |=|",
                "  TypeNode: DispInterfaceKeyword |dispinterface|",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void InterfaceForwardDeclaration()
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
