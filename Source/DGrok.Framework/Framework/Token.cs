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
    public class Token : AstNode
    {
        private string _parsedText;
        private int _startIndex;
        private string _text;
        private TokenType _type;

        public Token(TokenType type, int startIndex, string text)
            : this(type, startIndex, text, "") { }
        public Token(TokenType type, int startIndex, string text, string parsedText)
        {
            _type = type;
            _startIndex = startIndex;
            _text = text;
            _parsedText = parsedText;
        }

        public override IEnumerable<KeyValuePair<string, AstNode>> Properties
        {
            get { yield break; }
        }
        public int NextIndex
        {
            get { return StartIndex + Text.Length; }
        }
        public string ParsedText
        {
            get { return _parsedText; }
        }
        public int StartIndex
        {
            get { return _startIndex; }
        }
        public string Text
        {
            get { return _text; }
        }
        public TokenType Type
        {
            get { return _type; }
        }

        public override void InspectTo(StringBuilder builder, int currentIndentCount)
        {
            builder.Append(Type.ToString());
            builder.Append(" |");
            builder.Append(Text);
            builder.Append("|");
            if (!String.IsNullOrEmpty(ParsedText))
            {
                builder.Append(", parsed=|");
                builder.Append(ParsedText);
                builder.Append("|");
            }
        }
        public Token WithTokenType(TokenType newType)
        {
            return new Token(newType, StartIndex, Text);
        }
    }
}
