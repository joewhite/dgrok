﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.DelphiNodes
{
    public partial class MethodImplementationNode
    {
        public override String ToCSharpCode()
        {
            StringBuilder builder = new StringBuilder();
            // These general methods should be converted manually
            if (_methodHeadingNode.NameNode.ToCode().ToLower().Equals("TDM.SetSqlConnection".ToLower()) ||
                _methodHeadingNode.NameNode.ToCode().ToLower().Equals("TDM.DataModuleCreate".ToLower()))
            {
                return "";
            }

            builder.Append(_methodHeadingNode.ToCSharpCode() + "\n");
            builder.Append("{\n");
            builder.Append(_fancyBlockNode.ToCSharpCode() + "\n");
            builder.Append("}\n");

            //builder.Append("//+++++++++++++++++++++ implementing [" + this.GetType().Name + "] ...\n");
            //System.Diagnostics.Debug.WriteLine("BEGIN DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine(builder.ToString());
            //System.Diagnostics.Debug.WriteLine("END DUMP [" + this.GetType().Name + "] NODE");
            //System.Diagnostics.Debug.WriteLine("======================================================");
            return builder.ToString();
        }
    }
}