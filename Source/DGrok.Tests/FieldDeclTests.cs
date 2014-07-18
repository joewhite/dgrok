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
    public class FieldDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FieldDecl; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("Foo: Integer;", ParsesAs(
                "FieldDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void WithoutSemicolon()
        {
            Assert.That("Foo: Integer", ParsesAs(
                "FieldDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: (none)"));
        }
        [Test]
        public void PortabilityDirectives()
        {
            Assert.That("Foo: Integer library deprecated;", ParsesAs(
                "FieldDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  PortabilityDirectiveListNode: ListNode",
                "    Items[0]: LibraryKeyword |library|",
                "    Items[1]: DeprecatedSemikeyword |deprecated|",
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
