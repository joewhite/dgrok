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
    public class RelOpTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RelOp; }
        }

        public void TestEqualSign()
        {
            Assert.That("=", ParsesAs("EqualSign |=|"));
        }
        public void TestGreaterThan()
        {
            Assert.That(">", ParsesAs("GreaterThan |>|"));
        }
        public void TestLessThan()
        {
            Assert.That("<", ParsesAs("LessThan |<|"));
        }
        public void TestLessOrEqual()
        {
            Assert.That("<=", ParsesAs("LessOrEqual |<=|"));
        }
        public void TestGreaterOrEqual()
        {
            Assert.That(">=", ParsesAs("GreaterOrEqual |>=|"));
        }
        public void TestNotEqual()
        {
            Assert.That("<>", ParsesAs("NotEqual |<>|"));
        }
        public void TestInKeyword()
        {
            Assert.That("in", ParsesAs("InKeyword |in|"));
        }
        public void TestIsKeyword()
        {
            Assert.That("is", ParsesAs("IsKeyword |is|"));
        }
        public void TestAsKeyword()
        {
            Assert.That("as", ParsesAs("AsKeyword |as|"));
        }
    }
}
