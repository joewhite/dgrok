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
    public class FieldDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FieldDecl; }
        }

        public void TestSimple()
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
        public void TestWithoutSemicolon()
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
        public void TestPortabilityDirectives()
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
