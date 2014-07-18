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
    public class AlternatorTests
    {
        private Parser.Alternator _alternator;

        [SetUp]
        public void SetUp()
        {
            _alternator = new Parser.Alternator();
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void DisplayTextWithNoAlternates()
        {
            _alternator.DisplayText();
        }
        [Test]
        public void DisplayTextWithOneAlternate()
        {
            _alternator.AddToken(new TokenSet("foo"));
            Assert.That(_alternator.DisplayText(), Is.EqualTo("foo"));
        }
        [Test]
        public void DisplayTextWithTwoAlternates()
        {
            _alternator.AddToken(new TokenSet("foo"));
            _alternator.AddToken(new TokenSet("bar"));
            Assert.That(_alternator.DisplayText(), Is.EqualTo("foo or bar"));
        }
        [Test]
        public void DisplayTextWithThreeAlternates()
        {
            _alternator.AddToken(new TokenSet("foo"));
            _alternator.AddToken(new TokenSet("bar"));
            _alternator.AddToken(new TokenSet("baz"));
            Assert.That(_alternator.DisplayText(), Is.EqualTo("foo, bar or baz"));
        }
    }
}
