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
    public class PackageTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Package; }
        }

        public void TestEmptyPackage()
        {
            Assert.That("package Foo; end.", ParsesAs(
                "PackageNode",
                "  PackageKeywordNode: PackageSemikeyword |package|",
                "  NameNode: Identifier |Foo|",
                "  SemicolonNode: Semicolon |;|",
                "  RequiresClauseNode: (none)",
                "  ContainsClauseNode: (none)",
                "  AttributeListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestDottedName()
        {
            Assert.That("package Foo.Bar; end.", ParsesAs(
                "PackageNode",
                "  PackageKeywordNode: PackageSemikeyword |package|",
                "  NameNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  SemicolonNode: Semicolon |;|",
                "  RequiresClauseNode: (none)",
                "  ContainsClauseNode: (none)",
                "  AttributeListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestRequiresAndContains()
        {
            Assert.That("package Foo; requires Bar; contains Baz; end.", ParsesAs(
                "PackageNode",
                "  PackageKeywordNode: PackageSemikeyword |package|",
                "  NameNode: Identifier |Foo|",
                "  SemicolonNode: Semicolon |;|",
                "  RequiresClauseNode: RequiresClauseNode",
                "    RequiresSemikeywordNode: RequiresSemikeyword |requires|",
                "    PackageListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: Identifier |Bar|",
                "        DelimiterNode: (none)",
                "    SemicolonNode: Semicolon |;|",
                "  ContainsClauseNode: UsesClauseNode",
                "    UsesKeywordNode: ContainsSemikeyword |contains|",
                "    UnitListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: UsedUnitNode",
                "          NameNode: Identifier |Baz|",
                "          InKeywordNode: (none)",
                "          FileNameNode: (none)",
                "        DelimiterNode: (none)",
                "    SemicolonNode: Semicolon |;|",
                "  AttributeListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
        public void TestAttribute()
        {
            Assert.That("package Foo; [assembly: AssemblyVersion('0.0.0.0')] end.", ParsesAs(
                "PackageNode",
                "  PackageKeywordNode: PackageSemikeyword |package|",
                "  NameNode: Identifier |Foo|",
                "  SemicolonNode: Semicolon |;|",
                "  RequiresClauseNode: (none)",
                "  ContainsClauseNode: (none)",
                "  AttributeListNode: ListNode",
                "    Items[0]: AttributeNode",
                "      OpenBracketNode: OpenBracket |[|",
                "      ScopeNode: AssemblySemikeyword |assembly|",
                "      ColonNode: Colon |:|",
                "      ValueNode: ParameterizedNode",
                "        LeftNode: Identifier |AssemblyVersion|",
                "        OpenDelimiterNode: OpenParenthesis |(|",
                "        ParameterListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: StringLiteral |'0.0.0.0'|",
                "            DelimiterNode: (none)",
                "        CloseDelimiterNode: CloseParenthesis |)|",
                "      CloseBracketNode: CloseBracket |]|",
                "  EndKeywordNode: EndKeyword |end|",
                "  DotNode: Dot |.|"));
        }
    }
}
