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
    public abstract class ParserTestCase
    {
        protected abstract RuleType RuleType { get; }

        protected void AssertDoesNotParse(string source)
        {
            Parser parser = CreateParser(source);
            try
            {
                parser.ParseRule(RuleType);
                Assert.Fail("Expected a ParseException, but none was thrown");
            }
            catch (ParseException)
            {
                Pass();
            }
        }
        public static Parser CreateParser(string source)
        {
            return Parser.FromText(source, "", CompilerDefines.CreateEmpty(),
                new MemoryFileLoader());
        }
        protected Constraint ParsesAs(params string[] expected)
        {
            return new ParsesAsConstraint(RuleType, expected);
        }
        protected void Pass()
        {
        }

        public void TestEmptyStringDoesNotParse()
        {
            AssertDoesNotParse("");
        }
    }
}
