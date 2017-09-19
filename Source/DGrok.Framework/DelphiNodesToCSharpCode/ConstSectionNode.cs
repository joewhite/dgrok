using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class ConstSectionNode
    {
        // A group of ConstantDeclNode
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_constListNode.ToCSharpCode("\n"));

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");

            return builder.ToString();
        }
    }
}
