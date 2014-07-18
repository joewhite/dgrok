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
    public class RecordTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.RecordType; }
        }

        public void TestEmptyRecord()
        {
            Assert.That("record end", ParsesAs(
                "RecordTypeNode",
                "  Record: RecordKeyword |record|",
                "  Contents: ListNode",
                "  VariantSection: (none)",
                "  End: EndKeyword |end|"));
        }
        public void TestContents()
        {
            Assert.That("record procedure Foo; end", ParsesAs(
                "RecordTypeNode",
                "  Record: RecordKeyword |record|",
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
                "  VariantSection: (none)",
                "  End: EndKeyword |end|"));
        }
    }
}
