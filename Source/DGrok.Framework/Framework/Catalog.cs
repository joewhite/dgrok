// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DGrok.Framework
{
    public class Catalog
    {
        private List<CodeBaseActionProxy> _items = new List<CodeBaseActionProxy>();

        private Catalog()
        {
        }

        public IList<CodeBaseActionProxy> Items
        {
            get { return _items; }
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
                        catalog.Items.Add(new CodeBaseActionProxy(type));
                }
            }
            catalog.Sort();
            return catalog;
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
