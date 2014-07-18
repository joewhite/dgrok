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
                "  Package: PackageSemikeyword |package|",
                "  Name: Identifier |Foo|",
                "  Semicolon: Semicolon |;|",
                "  AttributeList: ListNode",
                "  RequiresClause: (none)",
                "  ContainsClause: (none)",
                "  End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestDottedName()
        {
            Assert.That("package Foo.Bar; end.", ParsesAs(
                "PackageNode",
                "  Package: PackageSemikeyword |package|",
                "  Name: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |Bar|",
                "  Semicolon: Semicolon |;|",
                "  AttributeList: ListNode",
                "  RequiresClause: (none)",
                "  ContainsClause: (none)",
                "  End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestRequiresAndContains()
        {
            Assert.That("package Foo; requires Bar; contains Baz; end.", ParsesAs(
                "PackageNode",
                "  Package: PackageSemikeyword |package|",
                "  Name: Identifier |Foo|",
                "  Semicolon: Semicolon |;|",
                "  AttributeList: ListNode",
                "  RequiresClause: RequiresClauseNode",
                "    Requires: RequiresSemikeyword |requires|",
                "    PackageList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: Identifier |Bar|",
                "        Delimiter: (none)",
                "    Semicolon: Semicolon |;|",
                "  ContainsClause: UsesClauseNode",
                "    Uses: ContainsSemikeyword |contains|",
                "    UnitList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: UsedUnitNode",
                "          Name: Identifier |Baz|",
                "          In: (none)",
                "          FileName: (none)",
                "        Delimiter: (none)",
                "    Semicolon: Semicolon |;|",
                "  End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
        public void TestAttribute()
        {
            Assert.That("package Foo; [assembly: AssemblyVersion('0.0.0.0')] end.", ParsesAs(
                "PackageNode",
                "  Package: PackageSemikeyword |package|",
                "  Name: Identifier |Foo|",
                "  Semicolon: Semicolon |;|",
                "  AttributeList: ListNode",
                "    Items[0]: AttributeNode",
                "      OpenBracket: OpenBracket |[|",
                "      Scope: AssemblySemikeyword |assembly|",
                "      Colon: Colon |:|",
                "      Value: ParameterizedNode",
                "        Left: Identifier |AssemblyVersion|",
                "        OpenDelimiter: OpenParenthesis |(|",
                "        ParameterList: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: StringLiteral |'0.0.0.0'|",
                "            Delimiter: (none)",
                "        CloseDelimiter: CloseParenthesis |)|",
                "      CloseBracket: CloseBracket |]|",
                "  RequiresClause: (none)",
                "  ContainsClause: (none)",
                "  End: EndKeyword |end|",
                "  Dot: Dot |.|"));
        }
    }
}
