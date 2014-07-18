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
using System.Reflection;
using System.Text;

namespace DGrok.Framework
{
    public class Catalog
    {
        private List<Category> _categories;
        private Dictionary<CategoryType, Category> _categoriesByType =
            new Dictionary<CategoryType, Category>();

        private Catalog()
        {
        }

        public IList<Category> Categories
        {
            get { return _categories; }
        }

        private void Add(CodeBaseActionProxy action, CategoryType categoryType)
        {
            Category category;
            if (_categoriesByType.ContainsKey(categoryType))
                category = _categoriesByType[categoryType];
            else
            {
                category = new Category(categoryType);
                _categoriesByType[categoryType] = category;
            }
            category.Items.Add(action);
        }
        private void BuildSortedList()
        {
            _categories = new List<Category>(_categoriesByType.Values);
            _categories.Sort(delegate(Category a, Category b)
            {
                string aName = a.CategoryType.ToString();
                string bName = b.CategoryType.ToString();
                return String.Compare(aName, bName, StringComparison.CurrentCultureIgnoreCase);
            });
            foreach (Category category in _categories)
                category.Sort();
        }
        public static Catalog Load()
        {
            Catalog catalog = new Catalog();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    object[] attributes =
                        type.GetCustomAttributes(typeof(CodeBaseActionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        CodeBaseActionAttribute attribute = (CodeBaseActionAttribute) attributes[0];
                        catalog.Add(new CodeBaseActionProxy(type), attribute.Category);
                    }
                }
            }
            catalog.BuildSortedList();
            return catalog;
        }
    }
}
