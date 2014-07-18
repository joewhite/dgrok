// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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
