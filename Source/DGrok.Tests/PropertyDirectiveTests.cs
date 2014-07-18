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
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class PropertyDirectiveTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.PropertyDirective; }
        }

        [Test]
        public void DefaultProperty()
        {
            Assert.That("; default", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: DefaultSemikeyword |default|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void DefaultExpression()
        {
            Assert.That("default 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: DefaultSemikeyword |default|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void DispId()
        {
            Assert.That("dispid 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: DispIdSemikeyword |dispid|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Implements()
        {
            Assert.That("implements IFoo.Bar, IBaz", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ImplementsSemikeyword |implements|",
                "  ValueNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Identifier |IFoo|",
                "        OperatorNode: Dot |.|",
                "        RightNode: Identifier |Bar|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |IBaz|",
                "      DelimiterNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Index()
        {
            Assert.That("index 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: IndexSemikeyword |index|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void NoDefault()
        {
            Assert.That("nodefault", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: NoDefaultSemikeyword |nodefault|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Read()
        {
            Assert.That("read FFoo", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ReadSemikeyword |read|",
                "  ValueNode: Identifier |FFoo|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void ReadOnly()
        {
            Assert.That("readonly", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ReadOnlySemikeyword |readonly|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Stored()
        {
            Assert.That("stored GetStored", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: StoredSemikeyword |stored|",
                "  ValueNode: Identifier |GetStored|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Write()
        {
            Assert.That("write FFoo", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: WriteSemikeyword |write|",
                "  ValueNode: Identifier |FFoo|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void WriteOnly()
        {
            Assert.That("writeonly", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: WriteOnlySemikeyword |writeonly|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void LookaheadRejectsLoneSemicolon()
        {
            Parser parser = CreateParser(";");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
