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
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class CodeBaseOptionsTests
    {
        private CodeBaseOptions _options;

        [SetUp]
        public void SetUp()
        {
            _options = new CodeBaseOptions();
        }

        private void AssertDefined(string directive, Constraint constraint)
        {
            CompilerDefines defines = _options.CreateCompilerDefines();
            Assert.That(defines.IsTrue(directive, null), constraint, directive);
        }
        private void AssertIsDefined(string directive)
        {
            AssertDefined(directive, Is.True);
        }
        private void AssertIsUndefined(string directive)
        {
            AssertDefined(directive, Is.False);
        }

        [Test]
        public void DelphiVersionDefine()
        {
            _options.DelphiVersionDefine = "VER199";
            Assert.That(_options.Clone().DelphiVersionDefine, Is.EqualTo("VER199"));
            AssertIsDefined("IFDEF VER199");
            AssertIsUndefined("IFNDEF VER199");
        }
        [Test]
        public void CustomDefines()
        {
            _options.CustomDefines = "FOO;BAR";
            Assert.That(_options.Clone().CustomDefines, Is.EqualTo("FOO;BAR"));
            AssertIsDefined("IFDEF FOO");
            AssertIsDefined("IFDEF BAR");
            AssertIsUndefined("IFNDEF FOO");
            AssertIsUndefined("IFNDEF BAR");
        }
        [Test]
        public void CompilerOptionsSetOn()
        {
            _options.CompilerOptionsSetOn = "IQ";
            Assert.That(_options.Clone().CompilerOptionsSetOn, Is.EqualTo("IQ"));
            AssertIsDefined("IFOPT I+");
            AssertIsDefined("IFOPT Q+");
            AssertIsUndefined("IFOPT I-");
            AssertIsUndefined("IFOPT Q-");
        }
        [Test]
        public void CompilerOptionsSetOff()
        {
            _options.CompilerOptionsSetOff = "IQ";
            Assert.That(_options.Clone().CompilerOptionsSetOff, Is.EqualTo("IQ"));
            AssertIsDefined("IFOPT I-");
            AssertIsDefined("IFOPT Q-");
            AssertIsUndefined("IFOPT I+");
            AssertIsUndefined("IFOPT Q+");
        }
        [Test]
        public void TrueIfConditions()
        {
            _options.TrueIfConditions = "IF Foo or Bar;IF Baz";
            Assert.That(_options.Clone().TrueIfConditions, Is.EqualTo("IF Foo or Bar;IF Baz"));
            AssertIsDefined("IF Foo or Bar");
            AssertIsDefined("IF Baz");
        }
        [Test]
        public void FalseIfConditions()
        {
            _options.FalseIfConditions = "IF Foo or Bar;IF Baz";
            Assert.That(_options.Clone().FalseIfConditions, Is.EqualTo("IF Foo or Bar;IF Baz"));
            AssertIsUndefined("IF Foo or Bar");
            AssertIsUndefined("IF Baz");
        }
        [Test]
        public void IfOptBDefaultsToFalse()
        {
            AssertIsDefined("IFOPT B-");
            AssertIsUndefined("IFOPT B+");
        }
        [Test]
        public void IfOptCDefaultsToTrue()
        {
            AssertIsUndefined("IFOPT C-");
            AssertIsDefined("IFOPT C+");
        }
    }
}
