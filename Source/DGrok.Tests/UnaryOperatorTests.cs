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
    public class UnaryOperatorTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.UnaryOperator; }
        }

        public void TestAndDoesNotParse()
        {
            AssertDoesNotParse("and");
        }
        public void TestNot()
        {
            Assert.That("not", ParsesAs("NotKeyword |not|"));
        }
        public void TestPlusSign()
        {
            Assert.That("+", ParsesAs("PlusSign |+|"));
        }
        public void TestMinusSign()
        {
            Assert.That("-", ParsesAs("MinusSign |-|"));
        }
        public void TestAtSign()
        {
            Assert.That("@", ParsesAs("AtSign |@|"));
        }
        public void TestInherited()
        {
            Assert.That("inherited", ParsesAs("InheritedKeyword |inherited|"));
        }
    }
}
