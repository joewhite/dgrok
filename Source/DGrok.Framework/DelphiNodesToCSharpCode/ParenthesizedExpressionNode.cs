using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class ParenthesizedExpressionNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_openParenthesisNode.ToCSharpCode());
            builder.Append(_expressionNode.ToCSharpCode());
            builder.Append(_closeParenthesisNode.ToCSharpCode());

            //builder.Append("//++++++++++++++ Implementing [" + this.GetType().Name + "]");
            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================\n");
            return builder.ToString();
        }
    }
}
