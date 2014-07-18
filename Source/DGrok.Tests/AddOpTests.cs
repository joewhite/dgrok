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
    public class AddOpTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.AddOp; }
        }

        public void TestTimesDoesNotParse()
        {
            AssertDoesNotParse("*");
        }
        public void TestPlusSign()
        {
            Assert.That("+", ParsesAs("PlusSign |+|"));
        }
        public void TestMinusSign()
        {
            Assert.That("-", ParsesAs("MinusSign |-|"));
        }
        public void TestOrKeyword()
        {
            Assert.That("or", ParsesAs("OrKeyword |or|"));
        }
        public void TestXorKeyword()
        {
            Assert.That("xor", ParsesAs("XorKeyword |xor|"));
        }
    }
}
