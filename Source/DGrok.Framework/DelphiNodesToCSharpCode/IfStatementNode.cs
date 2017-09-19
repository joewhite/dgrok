using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class IfStatementNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            // If
            builder.Append(_ifKeywordNode.Text);
            builder.Append(" (");
            builder.Append(_conditionNode.ToCSharpCode());
            builder.Append(") ");
            builder.Append(_thenStatementNode.ToCSharpCode());

            if (_elseKeywordNode != null)
            {
                builder.Append("\n");
                builder.Append(_elseKeywordNode.Text + " ");
                builder.Append(_elseStatementNode.ToCSharpCode());
            }

            return builder.ToString();
        }
    }
}
