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
