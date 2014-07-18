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

        private bool DefineIsTrue(string compilerDirective)
        {
            return _defines.IsTrue(compilerDirective, new Location("", "", 0));
        }

        public void TestFalseIfUndefinedIfDef()
        {
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.False);
        }
        public void TestTrueIfUndefinedIfNDef()
        {
            Assert.That(DefineIsTrue("IFNDEF FOO"), Is.True);
        }
        [ExpectedException(typeof(PreprocessorException))]
        public void TestErrorIfUndefinedIf()
        {
            DefineIsTrue("IF Foo");
        }
        public void TestDefineDirectiveAsTrue()
        {
            _defines.DefineDirectiveAsTrue("IFDEF FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.True);
        }
        public void TestDefineDirectiveAsFalse()
        {
            _defines.DefineDirectiveAsFalse("IFDEF FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.False);
        }
        public void TestDefineSymbol()
        {
            _defines.DefineSymbol("FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.True);
            Assert.That(DefineIsTrue("IFNDEF FOO"), Is.False);
        }
        public void TestUndefineSymbol()
        {
            _defines.UndefineSymbol("FOO");
            Assert.That(DefineIsTrue("IFDEF FOO"), Is.False);
            Assert.That(DefineIsTrue("IFNDEF FOO"), Is.True);
        }
        public void TestNotCaseSensitive()
        {
            _defines.DefineDirectiveAsTrue("IFDEF FOO");
            Assert.That(DefineIsTrue("IfDef Foo"), Is.True);
        }
    }
}
