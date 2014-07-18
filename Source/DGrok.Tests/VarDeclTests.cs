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
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Absolute: (none)",
                "  AbsoluteAddress: (none)",
                "  EqualSign: (none)",
                "  Value: (none)",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestAbsolute()
        {
            Assert.That("Foo: Integer absolute Bar;", ParsesAs(
                "VarDeclNode",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Absolute: AbsoluteSemikeyword |absolute|",
                "  AbsoluteAddress: Identifier |Bar|",
                "  EqualSign: (none)",
                "  Value: (none)",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestInitialized()
        {
            Assert.That("Foo: Integer = 42;", ParsesAs(
                "VarDeclNode",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Absolute: (none)",
                "  AbsoluteAddress: (none)",
                "  EqualSign: EqualSign |=|",
                "  Value: Number |42|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestInitializedRecord()
        {
            Assert.That("Foo: TPoint = (X: 0; Y: 0);", ParsesAs(
                "VarDeclNode",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |TPoint|",
                "  Absolute: (none)",
                "  AbsoluteAddress: (none)",
                "  EqualSign: EqualSign |=|",
                "  Value: ConstantListNode",
                "    OpenParenthesis: OpenParenthesis |(|",
                "    ItemList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: RecordFieldConstantNode",
                "          Name: Identifier |X|",
                "          Colon: Colon |:|",
                "          Value: Number |0|",
                "        Delimiter: Semicolon |;|",
                "      Items[1]: DelimitedItemNode",
                "        Item: RecordFieldConstantNode",
                "          Name: Identifier |Y|",
                "          Colon: Colon |:|",
                "          Value: Number |0|",
                "        Delimiter: (none)",
                "    CloseParenthesis: CloseParenthesis |)|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("Foo: Integer deprecated library;", ParsesAs(
                "VarDeclNode",
                "  Names: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Absolute: (none)",
                "  AbsoluteAddress: (none)",
                "  EqualSign: (none)",
                "  Value: (none)",
                "  PortabilityDirectiveList: ListNode",
                "    Items[0]: DeprecatedSemikeyword |deprecated|",
                "    Items[1]: LibraryKeyword |library|",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
