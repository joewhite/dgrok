// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public abstract class AstNode
    {
        public abstract IEnumerable<KeyValuePair<string, AstNode>> Properties { get; }

        public abstract void Accept(Visitor visitor);
        public string Inspect()
        {
            StringBuilder builder = new StringBuilder();
            InspectTo(builder, 0);
            return builder.ToString();
        }
        public abstract void InspectTo(StringBuilder builder, int currentIndentCount);
    }
}
