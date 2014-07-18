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
    public class TypeSectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.TypeSection; }
        }

        public void TestTypes()
        {
            Assert.That("type TFoo = Integer; TBar = Byte;", ParsesAs(
                "TypeSectionNode",
                "  Type: TypeKeyword |type|",
                "  TypeList: ListNode",
                "    Items[0]: TypeDeclNode",
                "      Name: Identifier |TFoo|",
                "      EqualSign: EqualSign |=|",
                "      TypeKeyword: (none)",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "    Items[1]: TypeDeclNode",
                "      Name: Identifier |TBar|",
                "      EqualSign: EqualSign |=|",
                "      TypeKeyword: (none)",
                "      Type: Identifier |Byte|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestBareTypeDoesNotParse()
        {
            AssertDoesNotParse("type");
        }
    }
}
