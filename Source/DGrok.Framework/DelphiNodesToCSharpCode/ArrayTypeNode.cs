using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;

namespace DGrok.DelphiNodes
{
    public partial class ArrayTypeNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("List<" + Mapper.getMappedType(_typeNode.ToCSharpCode()) + ">");

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
