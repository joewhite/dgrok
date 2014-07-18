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
    public class CodeBaseActionProxy
    {
        private Type _type;

        public CodeBaseActionProxy(Type type)
        {
            _type = type;
        }

        public string Name
        {
            get { return _type.Name; }
        }

        public IList<Hit> Execute(CodeBase codeBase)
        {
            ICodeBaseAction action = (ICodeBaseAction) Activator.CreateInstance(_type);
            return action.Execute(codeBase);
        }
    }
}
