﻿using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;

namespace DGrok.DelphiNodes
{
    public partial class ConstantDeclNode
    {
        // A statement that declares a constant
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            String rawValue = "WOMhRHdyOH__NULL";
            if (_valueNode != null)
            {
                rawValue = _valueNode.ToCSharpCode();
            }

            String typeName = _typeNode == null ? null : Mapper.getMappedType(_typeNode.ToCSharpCode());
            if (typeName == null)
            {
                typeName = Mapper.getTypeOfValue(rawValue);
            }

            builder.Append("const " + typeName + " ");

            String constName = _nameNode.ToCSharpCode();
            builder.Append(constName);

            if (!rawValue.Equals("WOMhRHdyOH__NULL"))
            {
                builder.Append(" = ");
                builder.Append(rawValue);
            }
            builder.Append(";");

            return builder.ToString();
        }
    }
}
