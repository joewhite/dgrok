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
                "  VisibilityNode: VisibilityNode",
                "    StrictSemikeywordNode: (none)",
                "    VisibilityKeywordNode: PublicSemikeyword |public|",
                "  ContentListNode: ListNode"));
        }
        public void TestContentsOnly()
        {
            Assert.That("FFoo: Integer;", ParsesAs(
                "VisibilitySectionNode",
                "  VisibilityNode: (none)",
                "  ContentListNode: ListNode",
                "    Items[0]: FieldSectionNode",
                "      ClassKeywordNode: (none)",
                "      VarKeywordNode: (none)",
                "      FieldListNode: ListNode",
                "        Items[0]: FieldDeclNode",
                "          NameListNode: ListNode",
                "            Items[0]: DelimitedItemNode",
                "              ItemNode: Identifier |FFoo|",
                "              DelimiterNode: (none)",
                "          ColonNode: Colon |:|",
                "          TypeNode: Identifier |Integer|",
                "          PortabilityDirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|"));
        }
    }
}
