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
    public class MulOpTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MulOp; }
        }

        public void TestPlusDoesNotParse()
        {
            AssertDoesNotParse("+");
        }
        public void TestTimesSign()
        {
            Assert.That("*", ParsesAs("TimesSign |*|"));
        }
        public void TestDivideBySign()
        {
            Assert.That("/", ParsesAs("DivideBySign |/|"));
        }
        public void TestDivKeyword()
        {
            Assert.That("div", ParsesAs("DivKeyword |div|"));
        }
        public void TestModKeyword()
        {
            Assert.That("mod", ParsesAs("ModKeyword |mod|"));
        }
        public void TestAndKeyword()
        {
            Assert.That("and", ParsesAs("AndKeyword |and|"));
        }
        public void TestShlKeyword()
        {
            Assert.That("shl", ParsesAs("ShlKeyword |shl|"));
        }
        public void TestShrKeyword()
        {
            Assert.That("shr", ParsesAs("ShrKeyword |shr|"));
        }
    }
}
