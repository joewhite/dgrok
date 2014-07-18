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
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class ClassTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ClassType; }
        }

        [Test]
        public void EmptyClass()
        {
            Assert.That("class end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Contents()
        {
            Assert.That("class procedure Foo; end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
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
        [Test]
        public void AbstractClass()
        {
            Assert.That("class abstract end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: AbstractSemikeyword |abstract|",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void SealedClass()
        {
            Assert.That("class sealed end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: SealedSemikeyword |sealed|",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void BaseClass()
        {
            Assert.That("class(TComponent) end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  InheritanceListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |TComponent|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Interface()
        {
            Assert.That("class(TInterfacedObject, IInterface) end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  InheritanceListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |TInterfacedObject|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |IInterface|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void NoBody()
        {
            Parser parser = CreateParser("class(Exception);");
            AstNode node = parser.ParseRule(RuleType.ClassType);
            parser.ParseToken(new SingleTokenTokenSet(TokenType.Semicolon));
            Assert.That(parser.AtEof, Is.True);
            Assert.That(node.Inspect(), Is.EqualTo(
                "ClassTypeNode" + Environment.NewLine +
                "  ClassKeywordNode: ClassKeyword |class|" + Environment.NewLine +
                "  DispositionNode: (none)" + Environment.NewLine +
                "  OpenParenthesisNode: OpenParenthesis |(|" + Environment.NewLine +
                "  InheritanceListNode: ListNode" + Environment.NewLine +
                "    Items[0]: DelimitedItemNode" + Environment.NewLine +
                "      ItemNode: Identifier |Exception|" + Environment.NewLine +
                "      DelimiterNode: (none)" + Environment.NewLine +
                "  CloseParenthesisNode: CloseParenthesis |)|" + Environment.NewLine +
                "  ContentListNode: ListNode" + Environment.NewLine +
                "  EndKeywordNode: (none)"));
        }
    }
}
