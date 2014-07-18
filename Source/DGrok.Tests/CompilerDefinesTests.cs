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
    public class CompilerDefinesTests
    {
        private CompilerDefines _defines;

        [SetUp]
        public void SetUp()
        {
            _defines = CompilerDefines.CreateEmpty();
        }

        private bool DefineIsTrue(string compilerDirective)
        {
            return _defines.IsTrue(compilerDirective, new Location("", "", 0));
        }

        [Test]
        public void FalseIfUndefinedIfDef()
        {
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.False);
        }
        [Test]
        public void TrueIfUndefinedIfNDef()
        {
            Assert.That(DefineIsTrue("IFNDEF FOO"), Is.True);
        }
        [Test, ExpectedException(typeof(PreprocessorException))]
        public void ErrorIfUndefinedIf()
        {
            DefineIsTrue("IF Foo");
        }
        [Test]
        public void DefineDirectiveAsTrue()
        {
            _defines.DefineDirectiveAsTrue("IFDEF FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.True);
        }
        [Test]
        public void DefineDirectiveAsFalse()
        {
            _defines.DefineDirectiveAsFalse("IFDEF FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.False);
        }
        [Test]
        public void DefineSymbol()
        {
            _defines.DefineSymbol("FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.True);
            Assert.That(DefineIsTrue("IFNDEF FOO"), Is.False);
        }
        [Test]
        public void UndefineSymbol()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.False);
            Assert.That(DefineIsTrue("IFNDEF FOO"), Is.True);
        }
        [Test]
        public void NotCaseSensitive()
        {
            _defines.DefineDirectiveAsTrue("IFDEF FOO");
            Assert.That(DefineIsTrue("IfDef Foo"), Is.True);
        }
    }
}
