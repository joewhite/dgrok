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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    internal class LexScanner
    {
        private class Match
        {
            private int _length;
            private string _parsedText;
            private TokenType _tokenType;

            public Match(TokenType tokenType, int length)
                : this(tokenType, length, "") { }
            public Match(TokenType tokenType, int length, string parsedText)
            {
                _tokenType = tokenType;
                _length = length;
                _parsedText = parsedText;
            }

            public int Length
            {
                get { return _length; }
            }
            public string ParsedText
            {
                get { return _parsedText; }
            }
            public TokenType TokenType
            {
                get { return _tokenType; }
            }
        }

        private string _fileName;
        private int _index;
        private string _source;
        private Dictionary<string, TokenType> _wordTypes;

        public LexScanner(string source, string fileName)
        {
            _source = source;
            _fileName = fileName;
            _wordTypes = new Dictionary<string, TokenType>(StringComparer.InvariantCultureIgnoreCase);
            AddWordTypes(TokenSets.Semikeyword, "Semikeyword".Length);
            AddWordTypes(TokenSets.Keyword, "Keyword".Length);
        }

        private Location Location
        {
            get { return new Location(_fileName, _source, _index); }
        }

        private void AddWordTypes(IEnumerable<TokenType> tokenTypes, int suffixLength)
        {
            foreach (TokenType tokenType in tokenTypes)
            {
                string tokenString = tokenType.ToString();
                string baseWord = tokenString.Substring(0, tokenString.Length - suffixLength);
                _wordTypes[baseWord.ToLowerInvariant()] = tokenType;
            }
        }
        private Match AmpersandIdentifier()
        {
            if (Peek(0) != '&' || !IsWordLeadChar(Peek(1)))
                return null;
            int offset = 2;
            while (IsWordContinuationChar(Peek(offset)))
                ++offset;
            return new Match(TokenType.Identifier, offset);
        }
        private Match BareWord()
        {
            if (!IsWordLeadChar(Peek(0)))
                return null;
            int offset = 1;
            while (IsWordContinuationChar(Peek(offset)))
                ++offset;
            string word = _source.Substring(_index, offset);
            TokenType tokenType;
            if (_wordTypes.ContainsKey(word))
                tokenType = _wordTypes[word];
            else
                tokenType = TokenType.Identifier;
            return new Match(tokenType, offset);
        }
        private bool CanRead(int offset)
        {
            return _index + offset < _source.Length;
        }
        private Match CurlyBraceComment()
        {
            if (Peek(0) != '{')
                return null;
            int offset = 1;
            while (CanRead(offset) && Peek(offset) != '}')
                ++offset;
            ++offset;
            if (Peek(1) == '$')
            {
                string parsedText = _source.Substring(_index + 2, offset - 3).TrimEnd();
                return new Match(TokenType.CompilerDirective, offset, parsedText);
            }
            else
                return new Match(TokenType.CurlyBraceComment, offset);
        }
        private Match DotDot()
        {
            if (Peek(0) == '.' && Peek(1) == '.')
                return new Match(TokenType.DotDot, 2);
            return null;
        }
        private Match DoubleQuotedApostrophe()
        {
            if (Peek(0) == '"' && Peek(1) == '\'' && Peek(2) == '"')
                return new Match(TokenType.StringLiteral, 3);
            return null;
        }
        private Match EqualityOrAssignmentOperator()
        {
            switch (Peek(0))
            {
                case ':':
                    if (Peek(1) == '=')
                        return new Match(TokenType.ColonEquals, 2);
                    break;
                case '<':
                    switch (Peek(1))
                    {
                        case '=': return new Match(TokenType.LessOrEqual, 2);
                        case '>': return new Match(TokenType.NotEqual, 2);
                    }
                    return new Match(TokenType.LessThan, 1);
                case '=':
                    return new Match(TokenType.EqualSign, 1);
                case '>':
                    if (Peek(1) == '=')
                        return new Match(TokenType.GreaterOrEqual, 2);
                    return new Match(TokenType.GreaterThan, 1);
            }
            return null;
        }
        private Match HexNumber()
        {
            if (Peek(0) != '$')
                return null;
            int offset = 1;
            while (IsHexDigit(Peek(offset)))
                ++offset;
            return new Match(TokenType.Number, offset);
        }
        private bool IsHexDigit(char ch)
        {
            return
                (ch >= '0' && ch <= '9') ||
                (ch >= 'A' && ch <= 'F') ||
                (ch >= 'a' && ch <= 'f');
        }
        private bool IsWordContinuationChar(char ch)
        {
            return (Char.IsLetterOrDigit(ch) || ch == '_');
        }
        private bool IsWordLeadChar(char ch)
        {
            return (Char.IsLetter(ch) || ch == '_');
        }
        private Match NextMatch()
        {
            return
                BareWord() ??
                EqualityOrAssignmentOperator() ??
                Number() ??
                StringLiteral() ??
                SingleLineComment() ??
                CurlyBraceComment() ??
                ParenStarComment() ??
                DotDot() ??
                SingleCharacter() ??
                HexNumber() ??
                AmpersandIdentifier() ??
                DoubleQuotedApostrophe();
        }
        public Token NextToken()
        {
            while (_index < _source.Length && Char.IsWhiteSpace(_source[_index]))
                ++_index;
            if (_index >= _source.Length)
                return null;

            Match match = NextMatch();
            if (match == null)
                throw new LexException("Unrecognized character '" + _source[_index] + "'", Location);
            string text = _source.Substring(_index, match.Length);
            Token result = new Token(match.TokenType, Location, text, match.ParsedText);
            _index += match.Length;
            return result;
        }
        private Match Number()
        {
            if (!Char.IsNumber(Peek(0)))
                return null;
            int offset = 1;
            while (Char.IsNumber(Peek(offset)))
                ++offset;
            if (Peek(offset) == '.' && Peek(offset + 1) != '.')
            {
                ++offset;
                while (Char.IsNumber(Peek(offset)))
                    ++offset;
            }
            if (Peek(offset) == 'e' || Peek(offset) == 'E')
            {
                ++offset;
                if (Peek(offset) == '+' || Peek(offset) == '-')
                    ++offset;
                while (Char.IsNumber(Peek(offset)))
                    ++offset;
            }
            return new Match(TokenType.Number, offset);
        }
        private Match ParenStarComment()
        {
            if (Peek(0) != '(' || Peek(1) != '*')
                return null;
            int offset = 2;
            while (CanRead(offset) && !(Read(offset) == '*' && Peek(offset + 1) == ')'))
                ++offset;
            offset += 2;
            if (Peek(2) == '$')
            {
                string parsedText = _source.Substring(_index + 3, offset - 5).TrimEnd();
                return new Match(TokenType.CompilerDirective, offset, parsedText);
            }
            else
                return new Match(TokenType.ParenStarComment, offset);
        }
        private char Peek(int offset)
        {
            if (CanRead(offset))
                return Read(offset);
            return Char.MaxValue;
        }
        private char Read(int offset)
        {
            return _source[_index + offset];
        }
        private Match SingleCharacter()
        {
            char ch = _source[_index];
            switch (ch)
            {
                case '(': return new Match(TokenType.OpenParenthesis, 1);
                case ')': return new Match(TokenType.CloseParenthesis, 1);
                case '*': return new Match(TokenType.TimesSign, 1);
                case '+': return new Match(TokenType.PlusSign, 1);
                case ',': return new Match(TokenType.Comma, 1);
                case '-': return new Match(TokenType.MinusSign, 1);
                case '.': return new Match(TokenType.Dot, 1);
                case '/': return new Match(TokenType.DivideBySign, 1);
                case ':': return new Match(TokenType.Colon, 1);
                case ';': return new Match(TokenType.Semicolon, 1);
                case '@': return new Match(TokenType.AtSign, 1);
                case '[': return new Match(TokenType.OpenBracket, 1);
                case ']': return new Match(TokenType.CloseBracket, 1);
                case '^': return new Match(TokenType.Caret, 1);
            }
            return null;
        }
        private Match SingleLineComment()
        {
            if (Peek(0) != '/' || Peek(1) != '/')
                return null;
            int offset = 2;
            while (CanRead(offset) && Read(offset) != '\r' && Read(offset) != '\n')
                ++offset;
            return new Match(TokenType.SingleLineComment, offset);
        }
        private Match StringLiteral()
        {
            char firstChar = Read(0);
            if (firstChar != '\'' && firstChar != '#')
                return null;
            int offset = 0;
            while (Peek(offset) == '\'' || Peek(offset) == '#')
            {
                if (Peek(offset) == '\'')
                {
                    ++offset;
                    while (Read(offset) != '\'')
                        ++offset;
                    ++offset;
                }
                else
                {
                    ++offset;
                    if (Read(offset) == '$')
                        ++offset;
                    while (Char.IsLetterOrDigit(Peek(offset)))
                        ++offset;
                }
            }
            return new Match(TokenType.StringLiteral, offset);
        }
    }
}
