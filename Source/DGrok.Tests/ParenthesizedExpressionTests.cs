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
    public class ParenthesizedExpressionTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ParenthesizedExpression; }
        }

        public void TestNil()
        {
            Assert.That("(nil)", ParsesAs(
                "ParenthesizedExpressionNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  Expression: NilKeyword |nil|",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
        public void TestExpression()
        {
            Assert.That("(6 * 9)", ParsesAs(
                "ParenthesizedExpressionNode",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  Expression: BinaryOperationNode",
                "    Left: Number |6|",
                "    Operator: TimesSign |*|",
                "    Right: Number |9|",
                "  CloseParenthesis: CloseParenthesis |)|"));
        }
    }
}
