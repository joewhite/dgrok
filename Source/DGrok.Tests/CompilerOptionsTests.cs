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
