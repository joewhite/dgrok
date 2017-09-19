using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class SetLiteralNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");

            builder.Append("//TODO: implement [" + this.GetType().Name + "] in convert tool.\n");
            return builder.ToString();
        }
    }
}
