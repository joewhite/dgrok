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
