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
using NUnitLite.Constraints;

namespace DGrok.Tests
{
    public class ParsesAsConstraint : EqualConstraint
    {
        private RuleType _ruleType;

        public ParsesAsConstraint(RuleType ruleType, string[] expected)
            : base(String.Join(Environment.NewLine, expected))
        {
            _ruleType = ruleType;
        }

        public override bool Matches(object actual)
        {
            string source = (string) actual;
            Parser parser = Parser.FromText(source, CompilerDefines.CreateStandard());
            string actualString = parser.ParseRule(_ruleType).Inspect();
            if (!parser.AtEof)
                throw new InvalidOperationException("Rule did not consume all input");
            return base.Matches(actualString);
        }
    }
}
