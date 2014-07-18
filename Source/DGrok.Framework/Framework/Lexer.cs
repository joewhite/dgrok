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
