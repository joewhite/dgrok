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
    public class IdentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Ident; }
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
        public void TestKeywordsDoNotParse()
        {
            foreach (TokenType keyword in TokenSets.Keyword)
                AssertDoesNotParse(keyword.ToString().Replace("Keyword", ""));
        }
    }
}
