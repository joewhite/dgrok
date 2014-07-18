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
    public class TypeSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypeSection; }
        }

        [Test]
        public void Types()
        {
            Assert.That("type TFoo = Integer; TBar = Byte;", ParsesAs(
                "TypeSectionNode",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeListNode: ListNode",
                "    Items[0]: TypeDeclNode",
                "      NameNode: Identifier |TFoo|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: TypeDeclNode",
                "      NameNode: Identifier |TBar|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Byte|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassForwardDeclaration()
        {
            Assert.That("type TFoo = class;", ParsesAs(
                "TypeSectionNode",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeListNode: ListNode",
                "    Items[0]: TypeForwardDeclarationNode",
                "      NameNode: Identifier |TFoo|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeNode: ClassKeyword |class|",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void BareTypeDoesNotParse()
        {
            AssertDoesNotParse("type");
        }
    }
}
