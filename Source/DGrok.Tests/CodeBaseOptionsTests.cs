// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
