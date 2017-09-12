using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;
using DGrok.CSharpNodes;

namespace DGrok.DelphiNodes
{
    public partial class ConstantDeclNode
    {
        public override String ToCSharpCode()
        {
            String typeName = _typeNode == null ? null : Mapper.getMappedType(_typeNode.ToCode());
            if (typeName == null)
            {
                typeName = Mapper.getTypeOfValue(_valueNode.ToCode());
            }
            String constName = _nameNode.ToCode();

            ConstantDeclCSharpNode cNode = new ConstantDeclCSharpNode(typeName, constName, _valueNode.ToCode());
            return cNode.ToCode();
        }
    }
}
