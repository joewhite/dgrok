// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
