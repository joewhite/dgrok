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
using DGrok.Framework;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework.Constraints;

namespace DGrok.Tests
{
    [TestFixture]
    public class CompilerOptionsTests
    {
        private const string EntireAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private CompilerOptions _options;

        private void AssertIsOptionOff(char option, Constraint constraint)
        {
            Assert.That(_options.IsOptionOff(option), constraint, "IsOptionOff");
        }
        private void AssertIsOptionOn(char option, Constraint constraint)
        {
            Assert.That(_options.IsOptionOn(option), constraint, "IsOptionOff");
        }
        private void AssertOptionIsNeitherOnNorOff(char option)
        {
            AssertIsOptionOff(option, Is.False);
            AssertIsOptionOn(option, Is.False);
        }
        private void AssertOptionIsOff(char option)
        {
            AssertIsOptionOff(option, Is.True);
            AssertIsOptionOn(option, Is.False);
        }
        private void AssertOptionIsOn(char option)
        {
            AssertIsOptionOff(option, Is.False);
            AssertIsOptionOn(option, Is.True);
        }

        [SetUp]
        public void SetUp()
        {
            _options = new CompilerOptions();
        }

        [Test]
        public void WhenAllOptionsAreSetOff_EffectiveOptionsAreAllOff()
        {
            _options.OptionsSetOff = EntireAlphabet;
            for (char option = 'A'; option <= 'Z'; ++option)
                AssertOptionIsOff(option);
        }
        [Test]
        public void WhenAllOptionsAreSetOn_EffectiveOptionsAreAllOn()
        {
            _options.OptionsSetOn = EntireAlphabet;
            for (char option = 'A'; option <= 'Z'; ++option)
                AssertOptionIsOn(option);
        }
        [Test]
        public void OptionsSetOff_AreCaseInsensitive()
        {
            _options.OptionsSetOff = "a";
            AssertOptionIsOff('A');
        }
        [Test]
        public void OptionsSetOn_AreCaseInsensitive()
        {
            _options.OptionsSetOn = "a";
            AssertOptionIsOn('A');
        }
        [Test]
        public void IfOptA_DefaultsToNeitherOnNorOff()
        {
            AssertOptionIsNeitherOnNorOff('A');
        }
        [Test]
        public void IfOptB_DefaultsToOff()
        {
            AssertOptionIsOff('B');
        }
        [Test]
        public void IfOptC_DefaultsToOn()
        {
            AssertOptionIsOn('C');
        }
        [Test]
        public void IfOptD_DefaultsToOn()
        {
            AssertOptionIsOn('D');
        }
        [Test]
        public void IfOptE_DefaultsToOff()
        {
            AssertOptionIsOff('E');
        }
        [Test]
        public void IfOptF_DefaultsToOff()
        {
            AssertOptionIsOff('F');
        }
        [Test]
        public void IfOptG_DefaultsToOn()
        {
            AssertOptionIsOn('G');
        }
        [Test]
        public void IfOptH_DefaultsToOn()
        {
            AssertOptionIsOn('H');
        }
        [Test]
        public void IfOptI_DefaultsToOn()
        {
            AssertOptionIsOn('I');
        }
        [Test]
        public void IfOptJ_DefaultsToOff()
        {
            AssertOptionIsOff('J');
        }
        [Test]
        public void IfOptK_DefaultsToOff()
        {
            AssertOptionIsOff('K');
        }
        [Test]
        public void IfOptL_DefaultsToOn()
        {
            AssertOptionIsOn('L');
        }
        [Test]
        public void IfOptM_DefaultsToOff()
        {
            AssertOptionIsOff('M');
        }
        [Test]
        public void IfOptN_DefaultsToOn()
        {
            AssertOptionIsOn('N');
        }
        [Test]
        public void IfOptO_DefaultsToOn()
        {
            AssertOptionIsOn('O');
        }
        [Test]
        public void IfOptP_DefaultsToOn()
        {
            AssertOptionIsOn('P');
        }
        [Test]
        public void IfOptQ_DefaultsToOff()
        {
            AssertOptionIsOff('Q');
        }
        [Test]
        public void IfOptR_DefaultsToOff()
        {
            AssertOptionIsOff('R');
        }
        [Test]
        public void IfOptS_DefaultsToOff()
        {
            AssertOptionIsOff('S');
        }
        [Test]
        public void IfOptT_DefaultsToOff()
        {
            AssertOptionIsOff('T');
        }
        [Test]
        public void IfOptU_DefaultsToOff()
        {
            AssertOptionIsOff('U');
        }
        [Test]
        public void IfOptV_DefaultsToOn()
        {
            AssertOptionIsOn('V');
        }
        [Test]
        public void IfOptW_DefaultsToOff()
        {
            AssertOptionIsOff('W');
        }
        [Test]
        public void IfOptX_DefaultsToOn()
        {
            AssertOptionIsOn('X');
        }
        [Test]
        public void IfOptY_DefaultsToOn()
        {
            AssertOptionIsOn('Y');
        }
        [Test]
        public void IfOptZ_DefaultsToOff()
        {
            AssertOptionIsOff('Z');
        }
    }
}
