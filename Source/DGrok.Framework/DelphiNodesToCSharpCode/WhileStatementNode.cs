using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class WhileStatementNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("while");
            builder.Append("(");
            builder.Append(_conditionNode.ToCSharpCode());
            builder.Append(")");
            builder.Append(_statementNode.ToCSharpCode());

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
