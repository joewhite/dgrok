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
    public class Token : AstNode
    {
        private Location _location;
        private string _parsedText;
        private string _text;
        private TokenType _type;

        public Token(TokenType type, Location location, string text, string parsedText)
        {
            _type = type;
            _location = location;
            _text = text;
            _parsedText = parsedText;
        }

        public override IEnumerable<AstNode> ChildNodes
        {
            get { yield break; }
        }
        public override Location EndLocation
        {
            get { return new Location(Location.FileName, Location.FileSource, Location.Offset + Text.Length); }
        }
        public override Location Location
        {
            get { return _location; }
        }
        public string ParsedText
        {
            get { return _parsedText; }
        }
        public override IEnumerable<KeyValuePair<string, AstNode>> Properties
        {
            get { yield break; }
        }
        public string Text
        {
            get { return _text; }
        }
        public TokenType Type
        {
            get { return _type; }
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitToken(this);
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
            return new Token(newType, Location, Text, ParsedText);
        }
    }
}
