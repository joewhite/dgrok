using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class MethodHeadingNode
    {
        // The method's signature
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public ");
            if (_returnTypeNode != null)
            {
                builder.Append(Mapper.getMappedType(_returnTypeNode.ToCSharpCode()) + " ");
            } else
            {
                builder.Append("void ");
            }

            builder.Append(_nameNode.ToCSharpCode().Replace(".", "_"));
            builder.Append("(");
            foreach (var _paramNode in _parameterListNode.ChildNodes)
            {
                ParameterNode paramNode = ((DelimitedItemNode<ParameterNode>)_paramNode).ItemNode;
                foreach (var _nameNode in paramNode.NameListNode.ChildNodes)
                {
                    Token nameNode = ((DelimitedItemNode<Token>)_nameNode).ItemNode;
                    builder.Append(Mapper.getMappedType(paramNode.TypeNode.ToCSharpCode()));
                    builder.Append(" " + nameNode.Text + ", ");
                }
                builder.Length -= 2;
                builder.Append(", ");
            }
            builder.Length -= 2;
            builder.Append(")");

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
