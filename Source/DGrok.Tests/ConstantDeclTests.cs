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
                "  NameNode: Identifier |Foo|",
                "  ColonNode: (none)",
                "  TypeNode: (none)",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|",
                "  PortabilityDirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestTyped()
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
        public void TestTypedConstantList()
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
        public void TestTypedWhereTypeIsNotIdentifier()
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
        public void TestPortabilityDirectives()
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
        public void TestLookaheadRejectsVisibilitySpecifier()
        {
            Parser parser = CreateParser("public");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
        public void TestLookaheadRejectsStrictVisibilitySpecifier()
        {
            Parser parser = CreateParser("strict private");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
