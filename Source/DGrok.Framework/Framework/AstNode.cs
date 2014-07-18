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
        private AstNode _parentNode;

        public abstract IEnumerable<AstNode> ChildNodes { get;}
        public abstract IEnumerable<KeyValuePair<string, AstNode>> Properties { get; }

        public abstract Location EndLocation { get; }
        public abstract Location Location { get; }
        public AstNode ParentNode
        {
            get { return _parentNode; }
        }

        public abstract void Accept(Visitor visitor);
        internal void BuildParentReferences(AstNode myParent)
        {
            _parentNode = myParent;
            foreach (AstNode childNode in ChildNodes)
                childNode.BuildParentReferences(this);
        }
        public string Inspect()
        {
            StringBuilder builder = new StringBuilder();
            InspectTo(builder, 0);
            return builder.ToString();
        }
        public abstract void InspectTo(StringBuilder builder, int currentIndentCount);
        public T ParentNodeOfType<T>()
            where T : class
        {
            T result = this as T;
            if (result != null)
                return result;
            return ParentNode.ParentNodeOfType<T>();
        }
        public string ToCode()
        {
            return ToCode(this, this);
        }
        public static string ToCode(AstNode firstNode, AstNode lastNode)
        {
            Location first = firstNode.Location;
            Location last = lastNode.EndLocation;
            if (first.FileName != last.FileName)
                return "<code spans include files>";
            return first.FileSource.Substring(first.Offset, last.Offset - first.Offset);
        }
    }
}
