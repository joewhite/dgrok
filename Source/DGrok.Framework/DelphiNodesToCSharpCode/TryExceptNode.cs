using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Converter;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class TryExceptNode
    {
        public override String ToCSharpCode()
        {
            // TODO: handle else statements
            if (_elseKeywordNode != null)
            {
                throw new Exception("Handle me please...");
            }

            // Try
            StringBuilder builder = new StringBuilder();
            builder.Append(_tryKeywordNode.Text + "\n");
            builder.Append("{\n");
            builder.Append(_tryStatementListNode.ToCSharpCode("\n"));
            builder.Append("}\n");

            // Catch
            foreach (var exceptionItemNode in _exceptionItemListNode.Items)
            {
                String typeName = Mapper.getMappedType(((Token) exceptionItemNode.TypeNode).Text);
                String varName = exceptionItemNode.NameNode.Text;

                builder.Append("catch (" + typeName + " " + varName + ") ");
                builder.Append("{\n");
                builder.Append(exceptionItemNode.StatementNode.ToCSharpCode());
                builder.Append("}\n");
            }

            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}
