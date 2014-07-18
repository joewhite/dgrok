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
    public class MethodOrPropertyTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodOrProperty; }
        }

        [Test]
        public void Method()
        {
            Assert.That("procedure Foo;", ParsesAs(
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
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassMethod()
        {
            Assert.That("class procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  MethodTypeNode: ProcedureKeyword |procedure|",
                "  NameNode: Identifier |Foo|",
                "  OpenParenthesisNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ColonNode: (none)",
                "  ReturnTypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void Property()
        {
            Assert.That("property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |FFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        [Test]
        public void ClassProperty()
        {
            Assert.That("class property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |FFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
