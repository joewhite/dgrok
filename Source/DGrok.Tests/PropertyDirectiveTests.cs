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
                "  Semicolon: Semicolon |;|",
                "  Directive: DefaultSemikeyword |default|",
                "  Value: (none)",
                "  Data: ListNode"));
        }
        public void TestDefaultExpression()
        {
            Assert.That("default 42", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: DefaultSemikeyword |default|",
                "  Value: Number |42|",
                "  Data: ListNode"));
        }
        public void TestDispId()
        {
            Assert.That("dispid 42", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: DispIdSemikeyword |dispid|",
                "  Value: Number |42|",
                "  Data: ListNode"));
        }
        public void TestImplements()
        {
            Assert.That("implements IFoo.Bar, IBaz", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: ImplementsSemikeyword |implements|",
                "  Value: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: BinaryOperationNode",
                "        Left: Identifier |IFoo|",
                "        Operator: Dot |.|",
                "        Right: Identifier |Bar|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |IBaz|",
                "      Delimiter: (none)",
                "  Data: ListNode"));
        }
        public void TestIndex()
        {
            Assert.That("index 42", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: IndexSemikeyword |index|",
                "  Value: Number |42|",
                "  Data: ListNode"));
        }
        public void TestNoDefault()
        {
            Assert.That("nodefault", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: NoDefaultSemikeyword |nodefault|",
                "  Value: (none)",
                "  Data: ListNode"));
        }
        public void TestRead()
        {
            Assert.That("read FFoo", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: ReadSemikeyword |read|",
                "  Value: Identifier |FFoo|",
                "  Data: ListNode"));
        }
        public void TestReadOnly()
        {
            Assert.That("readonly", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: ReadOnlySemikeyword |readonly|",
                "  Value: (none)",
                "  Data: ListNode"));
        }
        public void TestStored()
        {
            Assert.That("stored GetStored", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: StoredSemikeyword |stored|",
                "  Value: Identifier |GetStored|",
                "  Data: ListNode"));
        }
        public void TestWrite()
        {
            Assert.That("write FFoo", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: WriteSemikeyword |write|",
                "  Value: Identifier |FFoo|",
                "  Data: ListNode"));
        }
        public void TestWriteOnly()
        {
            Assert.That("writeonly", ParsesAs(
                "DirectiveNode",
                "  Semicolon: (none)",
                "  Directive: WriteOnlySemikeyword |writeonly|",
                "  Value: (none)",
                "  Data: ListNode"));
        }
        public void TestLookaheadRejectsLoneSemicolon()
        {
            Parser parser = CreateParser(";");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
