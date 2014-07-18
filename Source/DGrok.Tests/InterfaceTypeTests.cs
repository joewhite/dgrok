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
    public class InterfaceTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.InterfaceType; }
        }

        [Test]
        public void EmptyInterface()
        {
            Assert.That("interface end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: InterfaceKeyword |interface|",
                "  OpenParenthesisNode: (none)",
                "  BaseInterfaceNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  OpenBracketNode: (none)",
                "  GuidNode: (none)",
                "  CloseBracketNode: (none)",
                "  MethodAndPropertyListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void BaseInterface()
        {
            Assert.That("interface(IFoo) end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: InterfaceKeyword |interface|",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  BaseInterfaceNode: Identifier |IFoo|",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  OpenBracketNode: (none)",
                "  GuidNode: (none)",
                "  CloseBracketNode: (none)",
                "  MethodAndPropertyListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Guid()
        {
            Assert.That("interface ['{5781334E-E121-4C2B-B7A4-0396A632F94F}'] end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: InterfaceKeyword |interface|",
                "  OpenParenthesisNode: (none)",
                "  BaseInterfaceNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  OpenBracketNode: OpenBracket |[|",
                "  GuidNode: StringLiteral |'{5781334E-E121-4C2B-B7A4-0396A632F94F}'|",
                "  CloseBracketNode: CloseBracket |]|",
                "  MethodAndPropertyListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void GuidExpression()
        {
            Assert.That("interface [MyGuidConst] end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: InterfaceKeyword |interface|",
                "  OpenParenthesisNode: (none)",
                "  BaseInterfaceNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  OpenBracketNode: OpenBracket |[|",
                "  GuidNode: Identifier |MyGuidConst|",
                "  CloseBracketNode: CloseBracket |]|",
                "  MethodAndPropertyListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void Method()
        {
            Assert.That("interface procedure Foo; end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: InterfaceKeyword |interface|",
                "  OpenParenthesisNode: (none)",
                "  BaseInterfaceNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  OpenBracketNode: (none)",
                "  GuidNode: (none)",
                "  CloseBracketNode: (none)",
                "  MethodAndPropertyListNode: ListNode",
                "    Items[0]: MethodHeadingNode",
                "      ClassKeywordNode: (none)",
                "      MethodTypeNode: ProcedureKeyword |procedure|",
                "      NameNode: Identifier |Foo|",
                "      OpenParenthesisNode: (none)",
                "      ParameterListNode: ListNode",
                "      CloseParenthesisNode: (none)",
                "      ColonNode: (none)",
                "      ReturnTypeNode: (none)",
                "      DirectiveListNode: ListNode",
                "      SemicolonNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        [Test]
        public void DispInterface()
        {
            Assert.That("dispinterface end", ParsesAs(
                "InterfaceTypeNode",
                "  InterfaceKeywordNode: DispInterfaceKeyword |dispinterface|",
                "  OpenParenthesisNode: (none)",
                "  BaseInterfaceNode: (none)",
                "  CloseParenthesisNode: (none)",
                "  OpenBracketNode: (none)",
                "  GuidNode: (none)",
                "  CloseBracketNode: (none)",
                "  MethodAndPropertyListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
    }
}
