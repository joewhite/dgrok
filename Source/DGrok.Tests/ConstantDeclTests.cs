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
    public class ConstantDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ConstantDecl; }
        }

        public void TestSimple()
        {
            Assert.That("Foo = 42;", ParsesAs(
                "ConstantDeclNode",
                "  Name: Identifier |Foo|",
                "  Colon: (none)",
                "  Type: (none)",
                "  EqualSign: EqualSign |=|",
                "  Value: Number |42|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTyped()
        {
            Assert.That("Foo: Integer = 42;", ParsesAs(
                "ConstantDeclNode",
                "  Name: Identifier |Foo|",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  EqualSign: EqualSign |=|",
                "  Value: Number |42|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTypedConstantList()
        {
            Assert.That("Foo: TMyArray = (24, 42);", ParsesAs(
                "ConstantDeclNode",
                "  Name: Identifier |Foo|",
                "  Colon: Colon |:|",
                "  Type: Identifier |TMyArray|",
                "  EqualSign: EqualSign |=|",
                "  Value: ConstantListNode",
                "    OpenParenthesis: OpenParenthesis |(|",
                "    ItemList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: Number |24|",
                "        Delimiter: Comma |,|",
                "      Items[1]: DelimitedItemNode",
                "        Item: Number |42|",
                "        Delimiter: (none)",
                "    CloseParenthesis: CloseParenthesis |)|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestTypedWhereTypeIsNotIdentifier()
        {
            Assert.That("Foo: set of Byte = [];", ParsesAs(
                "ConstantDeclNode",
                "  Name: Identifier |Foo|",
                "  Colon: Colon |:|",
                "  Type: SetOfNode",
                "    Set: SetKeyword |set|",
                "    Of: OfKeyword |of|",
                "    Type: Identifier |Byte|",
                "  EqualSign: EqualSign |=|",
                "  Value: SetLiteralNode",
                "    OpenBracket: OpenBracket |[|",
                "    ItemList: ListNode",
                "    CloseBracket: CloseBracket |]|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("Foo = 42 library experimental;", ParsesAs(
                "ConstantDeclNode",
                "  Name: Identifier |Foo|",
                "  Colon: (none)",
                "  Type: (none)",
                "  EqualSign: EqualSign |=|",
                "  Value: Number |42|",
                "  PortabilityDirectiveList: ListNode",
                "    Items[0]: LibraryKeyword |library|",
                "    Items[1]: ExperimentalSemikeyword |experimental|",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
