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
    public abstract class AstNode
    {
        public abstract IEnumerable<KeyValuePair<string, AstNode>> Properties { get; }

        public AstNode FirstChild
        {
            get
            {
                foreach (KeyValuePair<string, AstNode> property in Properties)
                {
                    if (property.Value != null)
                        return property.Value;
                }
                return null;
            }
        }
        public Token FirstToken
        {
            get
            {
                AstNode node = this;
                for (; ; )
                {
                    AstNode child = node.FirstChild;
                    if (child == null)
                        return (Token) node;
                    node = child;
                }
            }
        }
        public AstNode LastChild
        {
            get
            {
                List<KeyValuePair<string, AstNode>> properties =
                    new List<KeyValuePair<string, AstNode>>(Properties);
                for (int i = properties.Count - 1; i >= 0; --i)
                {
                    AstNode node = properties[i].Value;
                    if (node != null)
                        return node;
                }
                return null;
            }
        }
        public Token LastToken
        {
            get
            {
                AstNode node = this;
                for (; ; )
                {
                    AstNode child = node.LastChild;
                    if (child == null)
                        return (Token) node;
                    node = child;
                }
            }
        }

        public abstract void Accept(Visitor visitor);
        public string Inspect()
        {
            StringBuilder builder = new StringBuilder();
            InspectTo(builder, 0);
            return builder.ToString();
        }
        public abstract void InspectTo(StringBuilder builder, int currentIndentCount);
        public string ToCode()
        {
            return ToCode(this, this);
        }
        public static string ToCode(AstNode firstNode, AstNode lastNode)
        {
            Location first = firstNode.FirstToken.Location;
            Location last = lastNode.LastToken.EndLocation;
            if (first.FileName != last.FileName)
                return "<code spans include files>";
            return first.FileSource.Substring(first.Offset, last.Offset - first.Offset);
        }
    }
}
