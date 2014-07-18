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
    public class CompilerDefinesTests
    {
        private CompilerDefines _defines;

        [SetUp]
        public void SetUp()
        {
            _defines = CompilerDefines.CreateEmpty();
        }

        [ExpectedException(typeof(PreprocessorException))]
        public void TestExceptionIfUndefinedExpression()
        {
            _defines.IsTrue("IFDEF FOO", 0);
        }
        public void TestDefineDirectiveAsTrue()
        {
            _defines.DefineDirectiveAsTrue("IFDEF FOO");
            Assert.That(_defines.IsTrue("IFDEF FOO", 0), Is.True);
        }
        public void TestDefineDirectiveAsFalse()
        {
            _defines.DefineDirectiveAsFalse("IFDEF FOO");
            Assert.That(_defines.IsTrue("IFDEF FOO", 0), Is.False);
        }
        public void TestDefineSymbol()
        {
            _defines.DefineSymbol("FOO");
            Assert.That(_defines.IsTrue("IFDEF FOO", 0), Is.True);
            Assert.That(_defines.IsTrue("IFNDEF FOO", 0), Is.False);
        }
        public void TestUndefineSymbol()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That(_defines.IsTrue("IFDEF FOO", 0), Is.False);
            Assert.That(_defines.IsTrue("IFNDEF FOO", 0), Is.True);
        }
        public void TestNotCaseSensitive()
        {
            _defines.DefineDirectiveAsTrue("IFDEF FOO");
            Assert.That(_defines.IsTrue("IfDef Foo", 0), Is.True);
        }
    }
}
