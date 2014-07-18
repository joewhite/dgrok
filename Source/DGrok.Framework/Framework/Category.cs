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
