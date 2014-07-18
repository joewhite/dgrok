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
    public class EnumeratedTypeElementTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.EnumeratedTypeElement; }
        }

        public void TestName()
        {
            Assert.That("Foo", ParsesAs(
                "EnumeratedTypeElementNode",
                "  NameNode: Identifier |Foo|",
                "  EqualSignNode: (none)",
                "  ValueNode: (none)"));
        }
        public void TestValue()
        {
            Assert.That("Foo = 42", ParsesAs(
                "EnumeratedTypeElementNode",
                "  NameNode: Identifier |Foo|",
                "  EqualSignNode: EqualSign |=|",
                "  ValueNode: Number |42|"));
        }
    }
}
