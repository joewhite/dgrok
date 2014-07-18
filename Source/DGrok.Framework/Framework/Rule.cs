// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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
