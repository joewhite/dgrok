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
    [Description("Avoid globals when possible. (When you can't avoid it, at least use 'class var' instead of unit globals.)")]
    public class FindGlobalVariables : Visitor
    {
        public override void VisitMethodImplementationNode(MethodImplementationNode node)
        {
            // do not visit children
        }
        public override void VisitVarDeclNode(VarDeclNode node)
        {
            foreach (DelimitedItemNode<Token> delimitedItemNode in node.NameListNode.Items)
            {
                Token nameToken = delimitedItemNode.ItemNode;
                AddHit(nameToken, nameToken.Text);
            }
            base.VisitVarDeclNode(node);
        }
    }
}
