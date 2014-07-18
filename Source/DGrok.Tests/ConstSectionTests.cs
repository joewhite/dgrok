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
    public class ConstSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ConstSection; }
        }

        public void TestSimple()
        {
            Assert.That("const Foo = 24; Bar = 42;", ParsesAs(
                "ConstSectionNode",
                "  Const: ConstKeyword |const|",
                "  ConstList: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      Name: Identifier |Foo|",
                "      Colon: (none)",
                "      Type: (none)",
                "      EqualSign: EqualSign |=|",
                "      Value: Number |24|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "    Items[1]: ConstantDeclNode",
                "      Name: Identifier |Bar|",
                "      Colon: (none)",
                "      Type: (none)",
                "      EqualSign: EqualSign |=|",
                "      Value: Number |42|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestResourceString()
        {
            Assert.That("resourcestring Foo = 'Bar';", ParsesAs(
                "ConstSectionNode",
                "  Const: ResourceStringKeyword |resourcestring|",
                "  ConstList: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      Name: Identifier |Foo|",
                "      Colon: (none)",
                "      Type: (none)",
                "      EqualSign: EqualSign |=|",
                "      Value: StringLiteral |'Bar'|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestConstAloneDoesNotParse()
        {
            AssertDoesNotParse("const");
        }
    }
}
