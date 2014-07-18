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
using DGrok.Framework;
using NUnit.Framework.Constraints;

namespace DGrok.Tests
{
    public class LexesAsConstraint : EqualConstraint
    {
        private Converter<IEnumerable<Token>, IEnumerable<Token>> _filter;

        public LexesAsConstraint(string[] expected, Converter<IEnumerable<Token>, IEnumerable<Token>> filter)
            : base(String.Join(Environment.NewLine, expected))
        {
            _filter = filter;
        }

        public override bool Matches(object actual)
        {
            string source = (string) actual;
            Lexer lexer = new Lexer(source, "");
            IEnumerable<Token> filteredTokens = _filter(lexer.Tokens);
            List<Token> lexedTokens = new List<Token>(filteredTokens);
            List<string> inspectedTokens = lexedTokens.ConvertAll<string>(
                delegate(Token token) { return token.Inspect(); });
            string actualString = String.Join(Environment.NewLine, inspectedTokens.ToArray());
            return base.Matches(actualString);
        }
    }
}
