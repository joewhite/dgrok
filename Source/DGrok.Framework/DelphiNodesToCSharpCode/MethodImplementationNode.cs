using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class MethodImplementationNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_methodHeadingNode.ToCSharpCode() + "\n");
            builder.Append(_fancyBlockNode.ToCSharpCode() + "\n");
            builder.Append(_semicolonNode.ToCSharpCode() + "\n");
            return builder.ToString();
        }
    }
}
