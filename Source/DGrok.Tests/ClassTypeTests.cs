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
