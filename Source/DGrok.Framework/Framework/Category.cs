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
using System.Reflection;
using System.Text;

namespace DGrok.Framework
{
    public class Category
    {
        private CategoryType _categoryType;
        private List<CodeBaseActionProxy> _items = new List<CodeBaseActionProxy>();

        public Category(CategoryType categoryType)
        {
            _categoryType = categoryType;
        }

        public CategoryType CategoryType
        {
            get { return _categoryType; }
        }
        public string Description
        {
            get
            {
                MemberInfo[] enumValues = typeof(CategoryType).GetMember(CategoryType.ToString());
                if (enumValues.Length <= 0)
                    return "";
                object[] attributes = enumValues[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length <= 0)
                    return "";
                return ((DescriptionAttribute) attributes[0]).Description;
            }
        }
        public IList<CodeBaseActionProxy> Items
        {
            get { return _items; }
        }

        public void Sort()
        {
            _items.Sort(delegate(CodeBaseActionProxy a, CodeBaseActionProxy b)
            {
                return String.Compare(a.Name, b.Name, StringComparison.CurrentCultureIgnoreCase);
            });
        }
    }
}
