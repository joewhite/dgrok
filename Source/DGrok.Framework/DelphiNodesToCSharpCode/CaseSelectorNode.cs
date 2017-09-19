using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class CaseSelectorNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("case ");
            builder.Append(_valueListNode.ToCSharpCode(""));
            builder.Append(":\n");
            builder.Append(_statementNode.ToCSharpCode());
            builder.Append(" break;");

            //builder.Append("//++++++++++++++ Implementing [" + this.GetType().Name + "]");
            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
