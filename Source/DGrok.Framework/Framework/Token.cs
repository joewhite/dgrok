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
