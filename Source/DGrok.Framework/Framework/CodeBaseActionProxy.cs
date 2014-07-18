// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DGrok.Framework
{
    public class CodeBaseActionProxy
    {
        private string _description = "";
        private Type _type;

        public CodeBaseActionProxy(Type type)
        {
            _type = type;
            object[] attributes = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                _description = ((DescriptionAttribute) attributes[0]).Description;
        }

        public string Description
        {
            get { return _description; }
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
