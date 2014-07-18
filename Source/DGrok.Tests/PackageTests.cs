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
    public class PackageTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Package; }
        }

        [Test]
        public void EmptyPackage()
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
        [Test]
        public void DottedName()
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
        [Test]
        public void RequiresAndContains()
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
        [Test]
        public void Attribute()
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
