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
    public class InterfaceTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.InterfaceType; }
        }

        public void TestEmptyInterface()
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
        public void TestBaseInterface()
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
        public void TestGuid()
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
        public void TestGuidExpression()
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
        public void TestMethod()
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
        public void TestDispInterface()
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
