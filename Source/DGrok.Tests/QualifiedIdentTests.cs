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
    public class QualifiedIdentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.QualifiedIdent; }
        }

        public void TestIdentifier()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        public void TestSemikeyword()
        {
            Assert.That("Absolute", ParsesAs("Identifier |Absolute|"));
        }
        public void TestKeywordDoesNotParse()
        {
            AssertDoesNotParse("Class");
        }
        public void TestAmpersandKeyword()
        {
            Assert.That("&Class", ParsesAs("Identifier |&Class|"));
        }
        public void TestDots()
        {
            Assert.That("Foo.Bar.Baz", ParsesAs(
                "BinaryOperationNode",
                "  Left: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |Bar|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Baz|"));
        }
        public void TestDotKeyword()
        {
            Assert.That("Should.Not", ParsesAs(
                "BinaryOperationNode",
                "  Left: Identifier |Should|",
                "  Operator: Dot |.|",
                "  Right: Identifier |Not|"));
        }
    }
}
