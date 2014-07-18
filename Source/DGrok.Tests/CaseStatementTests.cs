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
    public class CaseStatementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.CaseStatement; }
        }

        public void TestSimple()
        {
            Assert.That("case Foo of 1: end", ParsesAs(
                "CaseStatementNode",
                "  Case: CaseKeyword |case|",
                "  Expression: Identifier |Foo|",
                "  Of: OfKeyword |of|",
                "  SelectorList: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      Values: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Number |1|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Statement: (none)",
                "      Semicolon: (none)",
                "  Else: (none)",
                "  ElseStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestEmptyElse()
        {
            Assert.That("case Foo of 1: else end", ParsesAs(
                "CaseStatementNode",
                "  Case: CaseKeyword |case|",
                "  Expression: Identifier |Foo|",
                "  Of: OfKeyword |of|",
                "  SelectorList: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      Values: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Number |1|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Statement: (none)",
                "      Semicolon: (none)",
                "  Else: ElseKeyword |else|",
                "  ElseStatements: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestPopulatedElse()
        {
            Assert.That("case Foo of 1: else Foo; Bar; end", ParsesAs(
                "CaseStatementNode",
                "  Case: CaseKeyword |case|",
                "  Expression: Identifier |Foo|",
                "  Of: OfKeyword |of|",
                "  SelectorList: ListNode",
                "    Items[0]: CaseSelectorNode",
                "      Values: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Number |1|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Statement: (none)",
                "      Semicolon: (none)",
                "  Else: ElseKeyword |else|",
                "  ElseStatements: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |Foo|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |Bar|",
                "      Delimiter: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
    }
}
