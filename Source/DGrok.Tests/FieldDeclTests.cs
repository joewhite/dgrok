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
                "  NameList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestPortabilityDirectives()
        {
            Assert.That("Foo: Integer library deprecated;", ParsesAs(
                "FieldDeclNode",
                "  NameList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  PortabilityDirectiveList: ListNode",
                "    Items[0]: LibraryKeyword |library|",
                "    Items[1]: DeprecatedSemikeyword |deprecated|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestLookaheadRejectsVisibilitySpecifier()
        {
            Parser parser = Parser.FromText("public", CompilerDefines.CreateEmpty());
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
        public void TestLookaheadRejectsStrictVisibilitySpecifier()
        {
            Parser parser = Parser.FromText("strict private", CompilerDefines.CreateEmpty());
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
