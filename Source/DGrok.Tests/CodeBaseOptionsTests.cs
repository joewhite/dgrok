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
using NUnitLite.Constraints;
using NUnitLite.Framework;

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

        public void TestDelphiVersionDefine()
        {
            _options.DelphiVersionDefine = "VER199";
            Assert.That(_options.Clone().DelphiVersionDefine, Is.EqualTo("VER199"));
            AssertIsDefined("IFDEF VER199");
            AssertIsUndefined("IFNDEF VER199");
        }
        public void TestCustomDefines()
        {
            _options.CustomDefines = "FOO;BAR";
            Assert.That(_options.Clone().CustomDefines, Is.EqualTo("FOO;BAR"));
            AssertIsDefined("IFDEF FOO");
            AssertIsDefined("IFDEF BAR");
            AssertIsUndefined("IFNDEF FOO");
            AssertIsUndefined("IFNDEF BAR");
        }
        public void TestCompilerOptionsSetOn()
        {
            _options.CompilerOptionsSetOn = "IQ";
            Assert.That(_options.Clone().CompilerOptionsSetOn, Is.EqualTo("IQ"));
            AssertIsDefined("IFOPT I+");
            AssertIsDefined("IFOPT Q+");
            AssertIsUndefined("IFOPT I-");
            AssertIsUndefined("IFOPT Q-");
        }
        public void TestCompilerOptionsSetOff()
        {
            _options.CompilerOptionsSetOff = "IQ";
            Assert.That(_options.Clone().CompilerOptionsSetOff, Is.EqualTo("IQ"));
            AssertIsDefined("IFOPT I-");
            AssertIsDefined("IFOPT Q-");
            AssertIsUndefined("IFOPT I+");
            AssertIsUndefined("IFOPT Q+");
        }
        public void TestTrueIfConditions()
        {
            _options.TrueIfConditions = "IF Foo or Bar;IF Baz";
            Assert.That(_options.Clone().TrueIfConditions, Is.EqualTo("IF Foo or Bar;IF Baz"));
            AssertIsDefined("IF Foo or Bar");
            AssertIsDefined("IF Baz");
        }
        public void TestFalseIfConditions()
        {
            _options.FalseIfConditions = "IF Foo or Bar;IF Baz";
            Assert.That(_options.Clone().FalseIfConditions, Is.EqualTo("IF Foo or Bar;IF Baz"));
            AssertIsUndefined("IF Foo or Bar");
            AssertIsUndefined("IF Baz");
        }
    }
}
