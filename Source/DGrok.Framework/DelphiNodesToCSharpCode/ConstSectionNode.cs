using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class ConstSectionNode
    {
        public override String ToCSharpCode()
        {
            return _constListNode.ToCSharpCode();
        }
    }
}
