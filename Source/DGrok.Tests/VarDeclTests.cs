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
    public class VarDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VarDecl; }
        }

        [Test]
        public void Simple()
        {
            Assert.That("Foo: Integer;", ParsesAs(
                "VarDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  FirstPortabilityDirectiveListNode: ListNode",
                "  AbsoluteSemikeywordNode: (none)",
                "  AbsoluteAddressNode: (none)",
                "  EqualSignNode: (none)",
                "  ValueNode: (none)",
                "  SecondPortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Absolute()
        {
            Assert.That("Foo: Integer absolute Bar;", ParsesAs(
                "VarDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  FirstPortabilityDirectiveListNode: ListNode",
                "  AbsoluteSemikeywordNode: AbsoluteSemikeyword |absolute|",
                "  AbsoluteAddressNode: Identifier |Bar|",
                "  EqualSignNode: (none)",
                "  ValueNode: (none)",
                "  SecondPortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Initialized()
        {
            Assert.That("Foo: Integer = 42;", ParsesAs(
                "VarDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  FirstPortabilityDirectiveListNode: ListNode",
                "  AbsoluteSemikeywordNode: (none)",
                "  AbsoluteAddressNode: (none)",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|",
                "  SecondPortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void InitializedRecord()
        {
            Assert.That("Foo: TPoint = (X: 0; Y: 0);", ParsesAs(
                "VarDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |TPoint|",
                "  FirstPortabilityDirectiveListNode: ListNode",
                "  AbsoluteSemikeywordNode: (none)",
                "  AbsoluteAddressNode: (none)",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: ConstantListNode",
                "    OpenParenthesisNode: OpenParenthesis |(|",
                "    ItemListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: RecordFieldConstantNode",
                "          NameNode: Identifier |X|",
                "          ColonNode: Colon |:|",
                "          ValueNode: Number |0|",
                "        DelimiterNode: Semicolon |;|",
                "      Items[1]: DelimitedItemNode",
                "        ItemNode: RecordFieldConstantNode",
                "          NameNode: Identifier |Y|",
                "          ColonNode: Colon |:|",
                "          ValueNode: Number |0|",
                "        DelimiterNode: (none)",
                "    CloseParenthesisNode: CloseParenthesis |)|",
                "  SecondPortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void PortabilityDirectives()
        {
            Assert.That("Foo: Integer deprecated library;", ParsesAs(
                "VarDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  FirstPortabilityDirectiveListNode: ListNode",
                "    Items[0]: DeprecatedSemikeyword |deprecated|",
                "    Items[1]: LibraryKeyword |library|",
                "  AbsoluteSemikeywordNode: (none)",
                "  AbsoluteAddressNode: (none)",
                "  EqualSignNode: (none)",
                "  ValueNode: (none)",
                "  SecondPortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void PortabilityDirectivesWithDefault()
        {
            Assert.That("Foo: Integer deprecated = 42 platform;", ParsesAs(
                "VarDeclNode",
                "  NameListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |Foo|",
                "      DelimiterNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  FirstPortabilityDirectiveListNode: ListNode",
                "    Items[0]: DeprecatedSemikeyword |deprecated|",
                "  AbsoluteSemikeywordNode: (none)",
                "  AbsoluteAddressNode: (none)",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|",
                "  SecondPortabilityDirectiveListNode: ListNode",
                "    Items[0]: PlatformSemikeyword |platform|",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
