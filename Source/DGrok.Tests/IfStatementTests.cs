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
    public class IfStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.IfStatement; }
        }

        [Test]
        public void EmptyThen()
        {
            Assert.That("if Foo then", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        [Test]
        public void PopulatedThen()
        {
            Assert.That("if Foo then Bar", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: Identifier |Bar|",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        [Test]
        public void EmptyThenEmptyElse()
        {
            Assert.That("if Foo then else", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementNode: (none)"));
        }
        [Test]
        public void PopulatedThenAndElse()
        {
            Assert.That("if Foo then Bar else Baz", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: Identifier |Bar|",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementNode: Identifier |Baz|"));
        }
        [Test]
        public void IfThenIfThenElse()
        {
            Assert.That("if Foo then if Bar then else", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: IfStatementNode",
                "    IfKeywordNode: IfKeyword |if|",
                "    ConditionNode: Identifier |Bar|",
                "    ThenKeywordNode: ThenKeyword |then|",
                "    ThenStatementNode: (none)",
                "    ElseKeywordNode: ElseKeyword |else|",
                "    ElseStatementNode: (none)",
                "  ElseKeywordNode: (none)",
                "  ElseStatementNode: (none)"));
        }
        [Test]
        public void IfThenIfThenElseElse()
        {
            Assert.That("if Foo then if Bar then else else", ParsesAs(
                "IfStatementNode",
                "  IfKeywordNode: IfKeyword |if|",
                "  ConditionNode: Identifier |Foo|",
                "  ThenKeywordNode: ThenKeyword |then|",
                "  ThenStatementNode: IfStatementNode",
                "    IfKeywordNode: IfKeyword |if|",
                "    ConditionNode: Identifier |Bar|",
                "    ThenKeywordNode: ThenKeyword |then|",
                "    ThenStatementNode: (none)",
                "    ElseKeywordNode: ElseKeyword |else|",
                "    ElseStatementNode: (none)",
                "  ElseKeywordNode: ElseKeyword |else|",
                "  ElseStatementNode: (none)"));
        }
    }
}
