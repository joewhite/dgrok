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
    public class VisibilitySectionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VisibilitySection; }
        }

        public void TestVisibilityOnly()
        {
            Assert.That("public", ParsesAs(
                "VisibilitySectionNode",
                "  Visibility: VisibilityNode",
                "    Strict: (none)",
                "    Visibility: PublicSemikeyword |public|",
                "  Contents: ListNode"));
        }
        public void TestContentsOnly()
        {
            Assert.That("FFoo: Integer;", ParsesAs(
                "VisibilitySectionNode",
                "  Visibility: (none)",
                "  Contents: ListNode",
                "    Items[0]: FieldSectionNode",
                "      Class: (none)",
                "      Var: (none)",
                "      FieldList: ListNode",
                "        Items[0]: FieldDeclNode",
                "          NameList: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              Item: Identifier |FFoo|",
                "              Delimiter: (none)",
                "          Colon: Colon |:|",
                "          Type: Identifier |Integer|",
                "          PortabilityDirectiveList: ListNode",
                "          Semicolon: Semicolon |;|"));
        }
    }
}
