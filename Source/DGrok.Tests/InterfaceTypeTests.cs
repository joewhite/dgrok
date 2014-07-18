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
