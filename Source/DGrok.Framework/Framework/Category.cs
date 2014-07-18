// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
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
