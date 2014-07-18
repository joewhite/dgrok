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
    public class MethodOrPropertyTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodOrProperty; }
        }

        [Test]
        public void Method()
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
        public void ClassMethod()
        {
            Assert.That("class procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: ClassKeyword |class|",
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
        public void Property()
        {
            Assert.That("property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |FFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassProperty()
        {
            Assert.That("class property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |FFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
