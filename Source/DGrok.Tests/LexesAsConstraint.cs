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
