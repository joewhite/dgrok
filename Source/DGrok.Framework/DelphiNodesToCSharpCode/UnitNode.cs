using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class UnitNode
    {
        public override String ToCSharpCode()
        {
            return _implementationSectionNode.ToCSharpCode();
        }
    }
}
