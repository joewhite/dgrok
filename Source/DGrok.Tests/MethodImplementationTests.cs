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
                "  MethodHeadingNode: MethodHeadingNode",
                "    ClassKeywordNode: (none)",
                "    MethodTypeNode: ProcedureKeyword |procedure|",
                "    NameNode: Identifier |Foo|",
                "    OpenParenthesisNode: (none)",
                "    ParameterListNode: ListNode",
                "    CloseParenthesisNode: (none)",
                "    ColonNode: (none)",
                "    ReturnTypeNode: (none)",
                "    DirectiveListNode: ListNode",
                "    SemicolonNode: Semicolon |;|",
                "  FancyBlockNode: FancyBlockNode",
                "    DeclListNode: ListNode",
                "    BlockNode: BlockNode",
                "      BeginKeywordNode: BeginKeyword |begin|",
                "      StatementListNode: ListNode",
                "      EndKeywordNode: EndKeyword |end|",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestForwardHasNoBody()
        {
            Assert.That("procedure Foo; forward;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: ForwardSemikeyword |forward|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestExternalHasNoBody()
        {
            Assert.That("procedure Foo; external 'Foo';", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: (none)",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: ExternalSemikeyword |external|",
                "      ValueNode: StringLiteral |'Foo'|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
