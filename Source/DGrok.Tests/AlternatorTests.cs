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
    public class AlternatorTests
    {
        private Parser.Alternator _alternator;

        [SetUp]
        public void SetUp()
        {
            _alternator = new Parser.Alternator();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDisplayTextWithNoAlternates()
        {
            _alternator.DisplayText();
        }
        public void TestDisplayTextWithOneAlternate()
        {
            _alternator.AddToken(new TokenSet("foo"));
            Assert.That(_alternator.DisplayText(), Is.EqualTo("foo"));
        }
        public void TestDisplayTextWithTwoAlternates()
        {
            _alternator.AddToken(new TokenSet("foo"));
            _alternator.AddToken(new TokenSet("bar"));
            Assert.That(_alternator.DisplayText(), Is.EqualTo("foo or bar"));
        }
        public void TestDisplayTextWithThreeAlternates()
        {
            _alternator.AddToken(new TokenSet("foo"));
            _alternator.AddToken(new TokenSet("bar"));
            _alternator.AddToken(new TokenSet("baz"));
            Assert.That(_alternator.DisplayText(), Is.EqualTo("foo, bar or baz"));
        }
    }
}
