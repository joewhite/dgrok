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
    public class MethodImplementationTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodImplementation; }
        }

        public void TestSimpleWithBody()
        {
            Assert.That("procedure Foo; begin end;", ParsesAs(
                "MethodImplementationNode",
                "  MethodHeading: MethodHeadingNode",
                "    Class: (none)",
                "    MethodType: ProcedureKeyword |procedure|",
                "    Name: Identifier |Foo|",
                "    OpenParenthesis: (none)",
                "    ParameterList: ListNode",
                "    CloseParenthesis: (none)",
                "    Colon: (none)",
                "    ReturnType: (none)",
                "    DirectiveList: ListNode",
                "    Semicolon: Semicolon |;|",
                "  FancyBlock: FancyBlockNode",
                "    DeclList: ListNode",
                "    Block: BlockNode",
                "      Begin: BeginKeyword |begin|",
                "      StatementList: ListNode",
                "      End: EndKeyword |end|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestForwardHasNoBody()
        {
            Assert.That("procedure Foo; forward;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: Semicolon |;|",
                "      Directive: ForwardSemikeyword |forward|",
                "      Value: (none)",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestExternalHasNoBody()
        {
            Assert.That("procedure Foo; external 'Foo';", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: Semicolon |;|",
                "      Directive: ExternalSemikeyword |external|",
                "      Value: StringLiteral |'Foo'|",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
