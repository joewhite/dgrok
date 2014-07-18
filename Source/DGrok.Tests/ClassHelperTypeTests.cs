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
    public class ClassHelperTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ClassHelperType; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("class helper for TObject end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeywordNode: ClassKeyword |class|",
                "  HelperSemikeywordNode: HelperSemikeyword |helper|",
                "  OpenParenthesisNode: (none)",
                "  BaseHelperTypeNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  ForKeywordNode: ForKeyword |for|",
                "  TypeNode: Identifier |TObject|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Descended()
        {
            Assert.That("class helper (TFooHelper) for TObject end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeywordNode: ClassKeyword |class|",
                "  HelperSemikeywordNode: HelperSemikeyword |helper|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  BaseHelperTypeNode: Identifier |TFooHelper|",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ForKeywordNode: ForKeyword |for|",
                "  TypeNode: Identifier |TObject|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Method()
        {
            Assert.That("class helper for TObject procedure Foo; end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeywordNode: ClassKeyword |class|",
                "  HelperSemikeywordNode: HelperSemikeyword |helper|",
                "  OpenParenthesisNode: (none)",
                "  BaseHelperTypeNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  ForKeywordNode: ForKeyword |for|",
                "  TypeNode: Identifier |TObject|",
                "  ContentListNode: ListNode",
                "    Items[0]: VisibilitySectionNode",
                "      VisibilityNode: (none)",
                "      ContentListNode: ListNode",
                "        Items[0]: MethodHeadingNode",
                "          ClassKeywordNode: (none)",
                "          MethodTypeNode: ProcedureKeyword |procedure|",
                "          NameNode: Identifier |Foo|",
                "          OpenParenthesisNode: (none)",
                "          ParameterListNode: ListNode",
                "          CloseParenthesisNode: (none)",
                "          ColonNode: (none)",
                "          ReturnTypeNode: (none)",
                "          DirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
    }
}
