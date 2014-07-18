// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class MethodImplementationTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodImplementation; }
        }

        [Test]
        public void SimpleWithBody()
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
        [Test]
        public void ForwardHasNoBody()
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
        [Test]
        public void ExternalHasNoBody()
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
