using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;

namespace DGrok.DelphiNodes
{
    public partial class VarDeclNode
    {
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

            builder.Append(typeName +  " ");
            foreach (var _varName in _nameListNode.ChildNodes)
            {
                builder.Append(_varName.ToCSharpCode() + ", ");
            }

            builder.Length -= 2;

            if (!rawValue.Equals("WOMhRHdyOH__NULL"))
            {
                builder.Append(" = ");
                builder.Append(rawValue);
            }
            builder.Append(";");
            builder.Append("\n");

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
