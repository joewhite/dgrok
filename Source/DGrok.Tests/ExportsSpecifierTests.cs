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
    public class ExportsSpecifierTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ExportsSpecifier; }
        }

        public void TestIndex()
        {
            Assert.That("index 42", ParsesAs(
                "ParameterizedDirectiveNode",
                "  Keyword: IndexSemikeyword |index|",
                "  Value: Number |42|"));
        }
        public void TestName()
        {
            Assert.That("name 'Foo'", ParsesAs(
                "ParameterizedDirectiveNode",
                "  Keyword: NameSemikeyword |name|",
                "  Value: StringLiteral |'Foo'|"));
        }
    }
}
