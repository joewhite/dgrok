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
