using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class BlockNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n{\n");
            builder.Append(_statementListNode.ToCSharpCode("\n"));
            builder.Append("\n}");
            return builder.ToString();
        }
    }
}
