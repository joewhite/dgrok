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

namespace DGrok.Tests
{
    [TestFixture]
    public class MulOpTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MulOp; }
        }

        [Test]
        public void PlusDoesNotParse()
        {
            AssertDoesNotParse("+");
        }
        [Test]
        public void TimesSign()
        {
            Assert.That("*", ParsesAs("TimesSign |*|"));
        }
        [Test]
        public void DivideBySign()
        {
            Assert.That("/", ParsesAs("DivideBySign |/|"));
        }
        [Test]
        public void DivKeyword()
        {
            Assert.That("div", ParsesAs("DivKeyword |div|"));
        }
        [Test]
        public void ModKeyword()
        {
            Assert.That("mod", ParsesAs("ModKeyword |mod|"));
        }
        [Test]
        public void AndKeyword()
        {
            Assert.That("and", ParsesAs("AndKeyword |and|"));
        }
        [Test]
        public void ShlKeyword()
        {
            Assert.That("shl", ParsesAs("ShlKeyword |shl|"));
        }
        [Test]
        public void ShrKeyword()
        {
            Assert.That("shr", ParsesAs("ShrKeyword |shr|"));
        }
    }
}
