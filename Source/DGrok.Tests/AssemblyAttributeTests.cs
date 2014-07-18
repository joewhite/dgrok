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
                "  OpenBracket: OpenBracket |[|",
                "  Scope: AssemblySemikeyword |assembly|",
                "  Colon: Colon |:|",
                "  Value: ParameterizedNode",
                "    Left: Identifier |AssemblyVersion|",
                "    OpenDelimiter: OpenParenthesis |(|",
                "    ParameterList: ListNode",
                "      Items[0]: DelimitedItemNode",
                "        Item: StringLiteral |'0.0.0.0'|",
                "        Delimiter: (none)",
                "    CloseDelimiter: CloseParenthesis |)|",
                "  CloseBracket: CloseBracket |]|"));
        }
    }
}
