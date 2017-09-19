using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    // A method call
    public partial class ParameterizedNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_leftNode.ToCSharpCode());
            builder.Append(_openDelimiterNode.ToCSharpCode());
            builder.Append(_parameterListNode.ToCSharpCode(""));
            builder.Append(_closeDelimiterNode.ToCSharpCode());

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================\n");
            return builder.ToString();
        }
    }
}
