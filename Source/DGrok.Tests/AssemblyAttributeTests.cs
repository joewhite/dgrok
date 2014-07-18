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
    public class AssemblyAttributeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.AssemblyAttribute; }
        }

        [Test]
        public void Simple()
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
