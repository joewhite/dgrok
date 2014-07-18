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
    public class AssemblyAttributeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.AssemblyAttribute; }
        }

        public void TestSimple()
        {
            Assert.That("[assembly: AssemblyVersion('0.0.0.0')]", ParsesAs(
                "AttributeNode",
                "  OpenBracketNode: OpenBracket |[|",
                "  ScopeNode: AssemblySemikeyword |assembly|",
                "  ColonNode: Colon |:|",
                "  ValueNode: ParameterizedNode",
                "    LeftNode: Identifier |AssemblyVersion|",
                "    OpenDelimiterNode: OpenParenthesis |(|",
                "    ParameterListNode: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        ItemNode: StringLiteral |'0.0.0.0'|",
                "        DelimiterNode: (none)",
                "    CloseDelimiterNode: CloseParenthesis |)|",
                "  CloseBracketNode: CloseBracket |]|"));
        }
    }
}
