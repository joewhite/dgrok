// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnitLite.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class VarDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VarDecl; }
        }

        public void TestSimple()
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
        public void TestAbsolute()
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
        public void TestInitialized()
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
        public void TestInitializedRecord()
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
        public void TestPortabilityDirectives()
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
        public void TestPortabilityDirectivesWithDefault()
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
