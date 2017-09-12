using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class UnitSectionNode
    {
        public override String ToCSharpCode()
        {
            return _contentListNode.ToCSharpCode();
        }
    }
}
