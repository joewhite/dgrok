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
