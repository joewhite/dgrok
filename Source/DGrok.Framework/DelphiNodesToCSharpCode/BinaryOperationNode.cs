using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class BinaryOperationNode
    {
        public override String ToCSharpCode()
        {
            // Check item is included in a set
            if (_operatorNode.Type == Framework.TokenType.InKeyword && _rightNode is SetLiteralNode)
            {
                return _itemInASetTemplate();
            }

            // Check type of item
            if (_operatorNode.Type == TokenType.IsKeyword)
            {
                return _itemIsTypeTemplate();
            }

            return _normalCase();
        }

        private String _normalCase()
        {
            bool hasSpace = true;
            if (_operatorNode.Type == Framework.TokenType.Dot)
            {
                hasSpace = false;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(_leftNode.ToCSharpCode());
            builder.Append(hasSpace ? " " : "");
            builder.Append(_operatorNode.ToCSharpCode());
            builder.Append(hasSpace ? " " : "");
            builder.Append(_rightNode.ToCSharpCode());

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }

        private String _itemInASetTemplate()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("new[] {");
            builder.Append(((SetLiteralNode)_rightNode).ItemListNode.ToCSharpCode(" "));
            builder.Append("}.Contains(");
            builder.Append(_leftNode.ToCSharpCode());
            builder.Append(")");

            return builder.ToString();
        }

        private String _itemIsTypeTemplate()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_leftNode.ToCSharpCode());
            builder.Append(" ");
            builder.Append(_operatorNode.ToCSharpCode());
            builder.Append(" ");
            builder.Append(Mapper.getMappedType(_rightNode.ToCSharpCode()));

            return builder.ToString();
        }
    }
}
