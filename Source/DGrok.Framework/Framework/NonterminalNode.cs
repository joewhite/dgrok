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
    public abstract class NonterminalNode : AstNode
    {
        public override Location EndLocation
        {
            get
            {
                List<AstNode> childNodeList = new List<AstNode>(ChildNodes);
                for (int i = childNodeList.Count - 1; i >= 0; --i)
                {
                    Location childEndLocation = childNodeList[i].EndLocation;
                    if (childEndLocation != null)
                        return childEndLocation;
                }
                return null;
            }
        }
        public override Location Location
        {
            get
            {
                foreach (AstNode childNode in ChildNodes)
                {
                    Location childLocation = childNode.Location;
                    if (childLocation != null)
                        return childLocation;
                }
                return null;
            }
        }

        public override void InspectTo(StringBuilder builder, int currentIndentCount)
        {
            string mangledTypeName = GetType().Name;
            string typeNameWithoutGenericMangling = mangledTypeName.Split('`')[0];
            builder.Append(typeNameWithoutGenericMangling);
            int childIndentCount = currentIndentCount + 1;
            foreach (KeyValuePair<string, AstNode> property in Properties)
            {
                builder.AppendLine();
                builder.Append(' ', childIndentCount * 2);
                builder.Append(property.Key);
                builder.Append(": ");
                if (property.Value != null)
                    property.Value.InspectTo(builder, childIndentCount);
                else
                    builder.Append("(none)");
            }
        }
    }
}
