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
