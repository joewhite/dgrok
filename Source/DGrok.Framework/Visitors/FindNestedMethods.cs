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
using DGrok.DelphiNodes;
using DGrok.Framework;

namespace DGrok.Visitors
{
    [CodeBaseAction(CategoryType.BestPracticeViolations)]
    [Description("Nested methods can lead to much confusion. Use them with care, or not at all.")]
    public class FindNestedMethods : Visitor
    {
        private List<string> _methodHierarchy = new List<string>();

        public override void VisitMethodImplementationNode(MethodImplementationNode node)
        {
            _methodHierarchy.Add(node.MethodHeadingNode.NameNode.ToCode());
            try
            {
                if (_methodHierarchy.Count > 1)
                {
                    string path = String.Join(" -> ", _methodHierarchy.ToArray());
                    AddHit(node, path);
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
