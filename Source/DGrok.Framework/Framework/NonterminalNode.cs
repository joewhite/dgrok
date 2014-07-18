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
    public abstract class NonterminalNode : AstNode
    {
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
