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
    public class ExtendedIdentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExtendedIdent; }
        }

        public void TestIdentifier()
        {
            Assert.That("Foo", ParsesAs("Identifier |Foo|"));
        }
        public void TestSemikeywords()
        {
            foreach (TokenType semikeyword in TokenSets.Semikeyword)
            {
                string word = semikeyword.ToString().Replace("Semikeyword", "");
                Assert.That(word, ParsesAs("Identifier |" + word + "|"));
            }
        }
        public void TestKeywords()
        {
            foreach (TokenType keyword in TokenSets.Keyword)
            {
                string word = keyword.ToString().Replace("Keyword", "");
                Assert.That(word, ParsesAs("Identifier |" + word + "|"));
            }
        }
    }
}
