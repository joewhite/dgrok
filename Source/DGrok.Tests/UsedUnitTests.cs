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
    public class UsedUnitTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.UsedUnit; }
        }

        public void TestName()
        {
            Assert.That("Foo", ParsesAs(
                "UsedUnitNode",
                "  Name: Identifier |Foo|",
                "  In: (none)",
                "  FileName: (none)"));
        }
        public void TestDottedName()
        {
            Assert.That("Foo.Bar", ParsesAs(
                "UsedUnitNode",
                "  Name: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |Bar|",
                "  In: (none)",
                "  FileName: (none)"));
        }
        public void TestInClause()
        {
            Assert.That("Foo in 'Foo.pas'", ParsesAs(
                "UsedUnitNode",
                "  Name: Identifier |Foo|",
                "  In: InKeyword |in|",
                "  FileName: StringLiteral |'Foo.pas'|"));
        }
    }
}
