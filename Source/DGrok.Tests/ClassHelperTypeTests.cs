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
    public class ClassHelperTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ClassHelperType; }
        }

        public void TestSimple()
        {
            Assert.That("class helper for TObject end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: ClassKeyword |class|",
                "  Helper: HelperSemikeyword |helper|",
                "  OpenParenthesis: (none)",
                "  BaseHelperType: (none)",
                "  CloseParenthesis: (none)",
                "  For: ForKeyword |for|",
                "  Type: Identifier |TObject|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestDescended()
        {
            Assert.That("class helper (TFooHelper) for TObject end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: ClassKeyword |class|",
                "  Helper: HelperSemikeyword |helper|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  BaseHelperType: Identifier |TFooHelper|",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  For: ForKeyword |for|",
                "  Type: Identifier |TObject|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestMethod()
        {
            Assert.That("class helper for TObject procedure Foo; end", ParsesAs(
                "TypeHelperNode",
                "  TypeKeyword: ClassKeyword |class|",
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
