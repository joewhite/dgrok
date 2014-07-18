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
    public partial class Parser
    {
        public class Alternator
        {
            #region IAlternate
            private interface IAlternate
            {
                string DisplayText();
                bool LookAhead(Parser parser);
                AstNode TryParse(Parser parser);
            }
            private class TokenSetAlternate : IAlternate
            {
                private ITokenSet _tokenSet;

                public TokenSetAlternate(ITokenSet tokenSet)
                {
                    _tokenSet = tokenSet;
                }

                public string DisplayText()
                {
                    return _tokenSet.Name;
                }
                public bool LookAhead(Parser parser)
                {
                    return parser.CanParseToken(_tokenSet);
                }
                public AstNode TryParse(Parser parser)
                {
                    if (parser.CanParseToken(_tokenSet))
                        return parser.ParseToken(_tokenSet);
                    return null;
                }
            }
            private class RuleAlternate : IAlternate
            {
                private RuleType _ruleType;

                public RuleAlternate(RuleType ruleType)
                {
                    _ruleType = ruleType;
                }

                public string DisplayText()
                {
                    return _ruleType.ToString();
                }
                public bool LookAhead(Parser parser)
                {
                    return parser.CanParseRule(_ruleType);
                }
                public AstNode TryParse(Parser parser)
                {
                    if (parser.CanParseRule(_ruleType))
                        return parser.ParseRuleInternal(_ruleType);
                    return null;
                }
            }
            #endregion

            private List<IAlternate> _alternates = new List<IAlternate>();

            public Alternator()
            {
            }

            public void AddRule(RuleType ruleType)
            {
                _alternates.Add(new RuleAlternate(ruleType));
            }
            public void AddToken(ITokenSet tokenSet)
            {
                _alternates.Add(new TokenSetAlternate(tokenSet));
            }
            public void AddToken(TokenType tokenType)
            {
                AddToken(new SingleTokenTokenSet(tokenType));
            }
            public string DisplayText()
            {
                if (_alternates.Count == 0)
                    throw new InvalidOperationException("Alternation requires at least one alternative");

                StringBuilder builder = new StringBuilder();
                for (int index = 0; index < _alternates.Count; ++index)
                {
                    if (index > 0)
                    {
                        if (index == _alternates.Count - 1)
                            builder.Append(" or ");
                        else
                            builder.Append(", ");
                    }
                    builder.Append(_alternates[index].DisplayText());
                }
                return builder.ToString();
            }
            public AstNode Execute(Parser parser)
            {
                foreach (IAlternate alternate in _alternates)
                {
                    AstNode result = alternate.TryParse(parser);
                    if (result != null)
                        return result;
                }
                throw parser.Failure(DisplayText());
            }
            public bool LookAhead(Parser parser)
            {
                foreach (IAlternate alternate in _alternates)
                {
                    if (alternate.LookAhead(parser))
                        return true;
                }
                return false;
            }
        }
    }
}
