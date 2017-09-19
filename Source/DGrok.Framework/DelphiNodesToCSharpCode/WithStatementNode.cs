using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class WithStatementNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            // Hardcode for porting SQL builder
            String expressionCode = _expressionListNode.ToCSharpCode("");
            if (expressionCode.Equals("QMain.SQL"))
            {
                builder.Append("CurrentWithSql = WITHSQL.QMain;\n");
            }
            else if (expressionCode.Equals("QSub.SQL"))
            {
                builder.Append("CurrentWithSql = WITHSQL.QSub;\n");
            }
            else if (expressionCode.Equals("QSub2.SQL"))
            {
                builder.Append("CurrentWithSql = WITHSQL.QSub2;\n");
            }
            else
            {
                throw new Exception("Don't know the with expression: " + expressionCode);
            }

            builder.Append(_statementNode.ToCSharpCode());

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
