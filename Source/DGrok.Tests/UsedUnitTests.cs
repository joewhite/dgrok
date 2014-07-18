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
                "  NameNode: Identifier |Foo|",
                "  InKeywordNode: (none)",
                "  FileNameNode: (none)"));
        }
        public void TestDottedName()
        {
            Assert.That("Foo.Bar", ParsesAs(
                "UsedUnitNode",
                "  NameNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |Bar|",
                "  InKeywordNode: (none)",
                "  FileNameNode: (none)"));
        }
        public void TestInClause()
        {
            Assert.That("Foo in 'Foo.pas'", ParsesAs(
                "UsedUnitNode",
                "  NameNode: Identifier |Foo|",
                "  InKeywordNode: InKeyword |in|",
                "  FileNameNode: StringLiteral |'Foo.pas'|"));
        }
    }
}
