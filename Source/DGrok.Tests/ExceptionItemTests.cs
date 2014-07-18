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
    public class ExceptionItemTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExceptionItem; }
        }

        public void TestEmpty()
        {
            Assert.That("on Exception do", ParsesAs(
                "ExceptionItemNode",
                "  On: OnSemikeyword |on|",
                "  Name: (none)",
                "  Colon: (none)",
                "  Type: Identifier |Exception|",
                "  Do: DoKeyword |do|",
                "  Statement: (none)",
                "  Semicolon: (none)"));
        }
        public void TestNamed()
        {
            Assert.That("on E: Exception do", ParsesAs(
                "ExceptionItemNode",
                "  On: OnSemikeyword |on|",
                "  Name: Identifier |E|",
                "  Colon: Colon |:|",
                "  Type: Identifier |Exception|",
                "  Do: DoKeyword |do|",
                "  Statement: (none)",
                "  Semicolon: (none)"));
        }
        public void TestStatement()
        {
            Assert.That("on Exception do Foo", ParsesAs(
                "ExceptionItemNode",
                "  On: OnSemikeyword |on|",
                "  Name: (none)",
                "  Colon: (none)",
                "  Type: Identifier |Exception|",
                "  Do: DoKeyword |do|",
                "  Statement: Identifier |Foo|",
                "  Semicolon: (none)"));
        }
        public void TestSemicolon()
        {
            Assert.That("on Exception do;", ParsesAs(
                "ExceptionItemNode",
                "  On: OnSemikeyword |on|",
                "  Name: (none)",
                "  Colon: (none)",
                "  Type: Identifier |Exception|",
                "  Do: DoKeyword |do|",
                "  Statement: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
