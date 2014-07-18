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
