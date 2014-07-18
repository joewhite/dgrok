// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public class Rule
    {
        private RuleDelegate _evaluate;
        private Predicate<Parser> _lookAhead;
        private Parser _parser;
        private RuleType _ruleType;

        public Rule(Parser parser, RuleType ruleType, Predicate<Parser> lookAhead, RuleDelegate evaluate)
        {
            _parser = parser;
            _ruleType = ruleType;
            _lookAhead = lookAhead;
            _evaluate = evaluate;
        }

        public bool CanParse()
        {
            return _lookAhead(_parser);
        }
        public AstNode Execute()
        {
            if (!CanParse())
                throw _parser.Failure(_ruleType.ToString());
            return _evaluate();
        }
    }
}
