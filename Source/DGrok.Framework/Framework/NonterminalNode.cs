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
