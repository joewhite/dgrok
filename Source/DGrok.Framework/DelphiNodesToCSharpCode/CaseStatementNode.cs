using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class CaseStatementNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("switch ");
            builder.Append("(" + _expressionNode.ToCSharpCode() + ")\n");
            builder.Append("{\n");
            builder.Append(_selectorListNode.ToCSharpCode("\n"));
            builder.Append("}\n");
            builder.Append("//++++++++++++++ Implementing [" + this.GetType().Name + "]");

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
