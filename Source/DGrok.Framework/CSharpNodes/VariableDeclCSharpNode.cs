using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.CSharpNodes
{
    public class VariableDeclCSharpNode
    {
        private String _typeName;
        private String _variableName;
        private String _value;

        public VariableDeclCSharpNode(String typeName, String variableName, String value)
        {
            _typeName = typeName;
            _variableName = variableName;
            _value = value;
        }

        public String ToCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_typeName + " " + _variableName);
            if (_value != null)
            {
                builder.Append(" = " + _value);
            }

            builder.Append(";");

            return builder.ToString() + "\n";
        }
    }
}
