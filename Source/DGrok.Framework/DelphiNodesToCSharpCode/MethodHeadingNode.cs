using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class MethodHeadingNode
    {

        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public ");
            builder.Append(Mapper.getMappedType(_returnTypeNode.ToCode()) + " ");
            builder.Append(_nameNode.ToCode().Replace(".", "_"));
            builder.Append("(");
            foreach (var _paramNode in _parameterListNode.ChildNodes)
            {
                ParameterNode paramNode = ((DelimitedItemNode<ParameterNode>)_paramNode).ItemNode;
                foreach (var _nameNode in paramNode.NameListNode.ChildNodes)
                {
                    Token nameNode = ((DelimitedItemNode<Token>)_nameNode).ItemNode;
                    builder.Append(Mapper.getMappedType(paramNode.TypeNode.ToCode()));
                    builder.Append(" " + nameNode.Text + ", ");
                }
                builder.Length -= 2;
                builder.Append(", ");
            }
            builder.Length -= 2;
            builder.Append(")");
            return builder.ToString();
        }
    }
}
