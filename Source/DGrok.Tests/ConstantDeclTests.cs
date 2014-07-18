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
