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
    public class FieldSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FieldSection; }
        }

        [Test]
        public void Fields()
        {
            Assert.That("Foo: Integer; Bar: Boolean;", ParsesAs(
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
                "      SemicolonNode: Semicolon |;|",
                "    Items[1]: FieldDeclNode",
                "      NameListNode: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          ItemNode: Identifier |Bar|",
                "          DelimiterNode: (none)",
                "      ColonNode: Colon |:|",
                "      TypeNode: Identifier |Boolean|",
                "      PortabilityDirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void VarWithField()
        {
            Assert.That("var Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: (none)",
                "  VarKeywordNode: VarKeyword |var|",
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
        public void ClassVarWithField()
        {
            Assert.That("class var Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  VarKeywordNode: VarKeyword |var|",
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
        public void EmptyVarSection()
        {
            Assert.That("var", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: (none)",
                "  VarKeywordNode: VarKeyword |var|",
                "  FieldListNode: ListNode"));
        }
        [Test]
        public void EmptyClassVarSection()
        {
            Assert.That("class var", ParsesAs(
                "FieldSectionNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  VarKeywordNode: VarKeyword |var|",
                "  FieldListNode: ListNode"));
        }
        [Test]
        public void ClassAloneDoesNotParse()
        {
            AssertDoesNotParse("class");
        }
    }
}
