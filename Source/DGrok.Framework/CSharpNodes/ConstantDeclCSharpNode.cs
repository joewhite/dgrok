using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;

namespace DGrok.CSharpNodes
{
    public class ConstantDeclCSharpNode
    {
        private String _typeName;
        private String _constName;
        private String _value;

        public ConstantDeclCSharpNode(String typeName, String constName, String value)
        {
            _typeName = typeName;
            _constName = constName;
            _value = value;
        }

        public String ToCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("const ");
            if (_typeName != null)
            {
                builder.Append(_typeName + " ");
            }

            builder.Append(_constName);
            builder.Append(" = " + Mapper.formatValue(_value, _typeName));
            builder.Append(";");

            return builder.ToString() + "\n";
        }
    }
}
