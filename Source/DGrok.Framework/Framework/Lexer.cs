// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public class Lexer
    {
        private string _source;

        public Lexer(string source)
        {
            _source = source;
        }

        public IEnumerable<Token> Tokens
        {
            get
            {
                LexScanner scanner = new LexScanner(_source);
                int nextIndex = 0;
                for (nextIndex = 0; ; )
                {
                    Token token = scanner.NextToken();
                    if (token == null)
                        yield break;
                    yield return token;
                    nextIndex = token.NextIndex;
                }
            }
        }
    }
}
