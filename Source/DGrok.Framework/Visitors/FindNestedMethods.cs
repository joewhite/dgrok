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
