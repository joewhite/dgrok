// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.DelphiNodes;
using DGrok.Framework;

namespace DGrok.Visitors
{
    [CodeBaseAction]
    public class FindNestedMethods : Visitor
    {
        private List<string> _methodHierarchy = new List<string>();

        public override void VisitMethodImplementationNode(MethodImplementationNode node)
        {
            _methodHierarchy.Add(node.MethodHeading.Name.ToCode());
            try
            {
                if (_methodHierarchy.Count > 1)
                {
                    string path = String.Join(" -> ", _methodHierarchy.ToArray());
                    Hits.Add(new Hit(node.FirstToken.Location, path));
                }
                base.VisitMethodImplementationNode(node);
            }
            finally
            {
                _methodHierarchy.RemoveAt(_methodHierarchy.Count - 1);
            }
        }
    }
}
