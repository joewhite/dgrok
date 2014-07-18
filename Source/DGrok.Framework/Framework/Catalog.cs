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
