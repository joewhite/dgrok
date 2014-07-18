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
    public class MethodSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodSection; }
        }

        public void TestMethod()
        {
            Assert.That("procedure Foo;", ParsesAs(
                "ListNode",
                "  Items[0]: MethodHeadingNode",
                "    Class: (none)",
                "    MethodType: ProcedureKeyword |procedure|",
                "    Name: Identifier |Foo|",
                "    OpenParenthesis: (none)",
                "    ParameterList: ListNode",
                "    CloseParenthesis: (none)",
                "    Colon: (none)",
                "    ReturnType: (none)",
                "    Semicolon: Semicolon |;|",
                "    DirectiveList: ListNode"));
        }
        public void TestProperty()
        {
            Assert.That("property Foo;", ParsesAs(
                "ListNode",
                "  Items[0]: PropertyNode",
                "    Class: (none)",
                "    Property: PropertyKeyword |property|",
                "    Name: Identifier |Foo|",
                "    OpenBracket: (none)",
                "    ParameterList: ListNode",
                "    CloseBracket: (none)",
                "    Colon: (none)",
                "    Type: (none)",
                "    Index: (none)",
                "    IndexValue: (none)",
                "    Read: (none)",
                "    ReadSpecifier: (none)",
                "    Write: (none)",
                "    WriteSpecifier: (none)",
                "    Stored: (none)",
                "    StoredSpecifier: (none)",
                "    Default: (none)",
                "    DefaultValue: (none)",
                "    Implements: (none)",
                "    ImplementsSpecifier: (none)",
                "    Semicolon: Semicolon |;|"));
        }
    }
}
