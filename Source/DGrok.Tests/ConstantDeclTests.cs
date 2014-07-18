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
    public class ConstantDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ConstantDecl; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("Foo = 42;", ParsesAs(
                "ConstantDeclNode",
                "  NameNode: Identifier |Foo|",
                "  ColonNode: (none)",
                "  TypeNode: (none)",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Typed()
        {
            Assert.That("Foo: Integer = 42;", ParsesAs(
                "ConstantDeclNode",
                "  NameNode: Identifier |Foo|",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void TypedConstantList()
        {
            Assert.That("Foo: TMyArray = (24, 42);", ParsesAs(
                "ConstantDeclNode",
                "  NameNode: Identifier |Foo|",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TMyArray|",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: ConstantListNode",
                "    OpenParenthesisNode: OpenParenthesis |(|",
                "    ItemListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: Number |24|",
                "        DelimiterNode: Comma |,|",
                "      Items[1]: DelimitedItemNode",
                "        ItemNode: Number |42|",
                "        DelimiterNode: (none)",
                "    CloseParenthesisNode: CloseParenthesis |)|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void TypedWhereTypeIsNotIdentifier()
        {
            Assert.That("Foo: set of Byte = [];", ParsesAs(
                "ConstantDeclNode",
                "  NameNode: Identifier |Foo|",
                "  ColonNode: Colon |:|",
                "  TypeNode: SetOfNode",
                "    SetKeywordNode: SetKeyword |set|",
                "    OfKeywordNode: OfKeyword |of|",
                "    TypeNode: Identifier |Byte|",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: SetLiteralNode",
                "    OpenBracketNode: OpenBracket |[|",
                "    ItemListNode: ListNode",
                "    CloseBracketNode: CloseBracket |]|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void PortabilityDirectives()
        {
            Assert.That("Foo = 42 library experimental;", ParsesAs(
                "ConstantDeclNode",
                "  NameNode: Identifier |Foo|",
                "  ColonNode: (none)",
                "  TypeNode: (none)",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|",
                "  PortabilityDirectiveListNode: ListNode",
                "    Items[0]: LibraryKeyword |library|",
                "    Items[1]: ExperimentalSemikeyword |experimental|",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void LookaheadRejectsVisibilitySpecifier()
        {
            Parser parser = CreateParser("public");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
        [Test]
        public void LookaheadRejectsStrictVisibilitySpecifier()
        {
            Parser parser = CreateParser("strict private");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
