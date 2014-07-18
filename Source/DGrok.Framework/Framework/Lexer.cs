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
        private string _fileName;
        private string _source;

        public Lexer(string source, string fileName)
        {
            _source = source;
            _fileName = fileName;
        }

        public IEnumerable<Token> Tokens
        {
            get
            {
                LexScanner scanner = new LexScanner(_source, _fileName);
                for (; ; )
                {
                    Token token = scanner.NextToken();
                    if (token == null)
                        yield break;
                    yield return token;
                }
            }
        }
    }
}
