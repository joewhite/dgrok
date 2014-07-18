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
    public class SetTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.SetType; }
        }

        public void TestSetOfByte()
        {
            Assert.That("set of Byte", ParsesAs(
                "SetOfNode",
                "  Set: SetKeyword |set|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |Byte|"));
        }
    }
}
