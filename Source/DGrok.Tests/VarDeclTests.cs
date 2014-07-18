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
