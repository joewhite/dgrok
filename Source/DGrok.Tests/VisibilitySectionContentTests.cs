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
    public class VisibilitySectionContentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VisibilitySectionContent; }
        }

        [Test]
        public void FieldSection()
        {
            Assert.That("Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: (none)",
                "  VarKeywordNode: (none)",
                "  FieldListNode: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Foo|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void MethodSection()
        {
            Assert.That("procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ConstSection()
        {
            Assert.That("const Foo = 24;", ParsesAs(
                "ConstSectionNode",
                "  ConstKeywordNode: ConstKeyword |const|",
                "  ConstListNode: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      NameNode: Identifier |Foo|",
                "      ColonNode: (none)",
                "      TypeNode: (none)",
                "      EqualSignNode: EqualSign |=|",
                "      ValueNode: Number |24|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void TypeSection()
        {
            Assert.That("type TFoo = Integer;", ParsesAs(
                "TypeSectionNode",
                "  TypeKeywordNode: TypeKeyword |type|",
                "  TypeListNode: ListNode",
                "    Items[0]: TypeDeclNode",
                "      NameNode: Identifier |TFoo|",
                "      EqualSignNode: EqualSign |=|",
                "      TypeKeywordNode: (none)",
                "      TypeNode: Identifier |Integer|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
    }
}
