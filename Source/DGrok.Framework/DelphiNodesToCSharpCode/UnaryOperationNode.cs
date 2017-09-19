using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class UnaryOperationNode
    {
        public override String ToCSharpCode()
        {
            if (_operatorNode.Type == TokenType.NotKeyword)
            {
                return "(!" + _operandNode.ToCSharpCode() + ")";
            }

            if (_operatorNode.Type == TokenType.MinusSign)
            {
                return _operatorNode.ToCSharpCode() + _operandNode.ToCSharpCode();
            }

            throw new Exception("Unkown unary operator type: " + _operatorNode);

            //StringBuilder builder = new StringBuilder();
            //builder.Append("//+++++++++++++++++++++ implementing [" + this.GetType().Name + "] ...\n");
            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            //return builder.ToString();
        }
    }
}
