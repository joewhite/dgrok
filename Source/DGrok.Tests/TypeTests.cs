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
    public class TypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Type; }
        }

        public void TestEmptyEnumDoesNotParse()
        {
            AssertDoesNotParse("()");
        }
        public void TestEnum()
        {
            Assert.That("(fooBar)", ParsesAs(
                "EnumeratedTypeNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: EnumeratedTypeElementNode",
                "        Name: Identifier |fooBar|",
                "        EqualSign: (none)",
                "        Value: (none)",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestQualifiedIdentifier()
        {
            Assert.That("System.Integer", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |System|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Integer|"));
        }
        public void TestRange()
        {
            Assert.That("24..42", ParsesAs(
                "BinaryOperationNode",
                "  Left: Number |24|",
                "  Operator: DotDot |..|",
                "  Right: Number |42|"));
        }
    }
}
