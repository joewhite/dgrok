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
