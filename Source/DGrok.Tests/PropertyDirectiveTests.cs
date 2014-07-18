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
    public class PropertyDirectiveTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.PropertyDirective; }
        }

        public void TestDefaultProperty()
        {
            Assert.That("; default", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: DefaultSemikeyword |default|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestDefaultExpression()
        {
            Assert.That("default 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: DefaultSemikeyword |default|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        public void TestDispId()
        {
            Assert.That("dispid 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: DispIdSemikeyword |dispid|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        public void TestImplements()
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
        public void TestIndex()
        {
            Assert.That("index 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: IndexSemikeyword |index|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        public void TestNoDefault()
        {
            Assert.That("nodefault", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: NoDefaultSemikeyword |nodefault|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestRead()
        {
            Assert.That("read FFoo", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ReadSemikeyword |read|",
                "  ValueNode: Identifier |FFoo|",
                "  DataNode: ListNode"));
        }
        public void TestReadOnly()
        {
            Assert.That("readonly", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ReadOnlySemikeyword |readonly|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestStored()
        {
            Assert.That("stored GetStored", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: StoredSemikeyword |stored|",
                "  ValueNode: Identifier |GetStored|",
                "  DataNode: ListNode"));
        }
        public void TestWrite()
        {
            Assert.That("write FFoo", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: WriteSemikeyword |write|",
                "  ValueNode: Identifier |FFoo|",
                "  DataNode: ListNode"));
        }
        public void TestWriteOnly()
        {
            Assert.That("writeonly", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: WriteOnlySemikeyword |writeonly|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestLookaheadRejectsLoneSemicolon()
        {
            Parser parser = CreateParser(";");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
