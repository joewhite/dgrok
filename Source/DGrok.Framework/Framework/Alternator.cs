// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
