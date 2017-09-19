using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class ForStatementNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(_forKeywordNode.ToCSharpCode());
            builder.Append("(");
            builder.Append("int " + _loopVariableNode.ToCSharpCode());
            builder.Append(" " + _colonEqualsNode.ToCSharpCode());
            builder.Append(_startingValueNode.ToCSharpCode() + ";");
            builder.Append(" " + _loopVariableNode.ToCSharpCode() + " <= ");
            builder.Append(_endingValueNode.ToCSharpCode() + ";");
            builder.Append(" " + _loopVariableNode.ToCSharpCode() + "++");
            builder.AppendLine(") {");
            builder.Append(_statementNode.ToCSharpCode());
            builder.AppendLine("}");

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
