// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
