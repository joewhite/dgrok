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
