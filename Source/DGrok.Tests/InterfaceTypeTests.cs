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
                "  Interface: InterfaceKeyword |interface|",
                "  OpenParenthesis: (none)",
                "  BaseInterface: (none)",
                "  CloseParenthesis: (none)",
                "  OpenBracket: (none)",
                "  Guid: (none)",
                "  CloseBracket: (none)",
                "  MethodAndPropertyList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestBaseInterface()
        {
            Assert.That("interface(IFoo) end", ParsesAs(
                "InterfaceTypeNode",
                "  Interface: InterfaceKeyword |interface|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  BaseInterface: Identifier |IFoo|",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  OpenBracket: (none)",
                "  Guid: (none)",
                "  CloseBracket: (none)",
                "  MethodAndPropertyList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestGuid()
        {
            Assert.That("interface ['{5781334E-E121-4C2B-B7A4-0396A632F94F}'] end", ParsesAs(
                "InterfaceTypeNode",
                "  Interface: InterfaceKeyword |interface|",
                "  OpenParenthesis: (none)",
                "  BaseInterface: (none)",
                "  CloseParenthesis: (none)",
                "  OpenBracket: OpenBracket |[|",
                "  Guid: StringLiteral |'{5781334E-E121-4C2B-B7A4-0396A632F94F}'|",
                "  CloseBracket: CloseBracket |]|",
                "  MethodAndPropertyList: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestMethod()
        {
            Assert.That("interface procedure Foo; end", ParsesAs(
                "InterfaceTypeNode",
                "  Interface: InterfaceKeyword |interface|",
                "  OpenParenthesis: (none)",
                "  BaseInterface: (none)",
                "  CloseParenthesis: (none)",
                "  OpenBracket: (none)",
                "  Guid: (none)",
                "  CloseBracket: (none)",
                "  MethodAndPropertyList: ListNode",
                "    Items[0]: MethodHeadingNode",
                "      Class: (none)",
                "      MethodType: ProcedureKeyword |procedure|",
                "      Name: Identifier |Foo|",
                "      OpenParenthesis: (none)",
                "      ParameterList: ListNode",
                "      CloseParenthesis: (none)",
                "      Colon: (none)",
                "      ReturnType: (none)",
                "      Semicolon: Semicolon |;|",
                "      DirectiveList: ListNode",
                "  End: EndKeyword |end|"));
        }
    }
}
