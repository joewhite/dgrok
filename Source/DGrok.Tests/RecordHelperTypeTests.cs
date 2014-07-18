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
    public class RecordHelperTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RecordHelperType; }
        }

        public void TestSimple()
        {
            Assert.That("record helper for TPoint end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: RecordKeyword |record|",
                "  Helper: HelperSemikeyword |helper|",
                "  OpenParenthesis: (none)",
                "  BaseHelperType: (none)",
                "  CloseParenthesis: (none)",
                "  For: ForKeyword |for|",
                "  Type: Identifier |TPoint|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestMethod()
        {
            Assert.That("record helper for TObject procedure Foo; end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: RecordKeyword |record|",
                "  Helper: HelperSemikeyword |helper|",
                "  OpenParenthesis: (none)",
                "  BaseHelperType: (none)",
                "  CloseParenthesis: (none)",
                "  For: ForKeyword |for|",
                "  Type: Identifier |TObject|",
                "  Contents: ListNode",
                "    Items[0]: VisibilitySectionNode",
                "      Visibility: (none)",
                "      Contents: ListNode",
                "        Items[0]: MethodHeadingNode",
                "          Class: (none)",
                "          MethodType: ProcedureKeyword |procedure|",
                "          Name: Identifier |Foo|",
                "          OpenParenthesis: (none)",
                "          ParameterList: ListNode",
                "          CloseParenthesis: (none)",
                "          Colon: (none)",
                "          ReturnType: (none)",
                "          DirectiveList: ListNode",
                "          Semicolon: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
    }
}
