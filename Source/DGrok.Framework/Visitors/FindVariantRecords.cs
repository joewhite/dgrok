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
    [CodeBaseAction(CategoryType.DotNetCompatibility)]
    [Description("Variant records are not typesafe, and are not allowed in managed code.")]
    public class FindVariantRecords : Visitor
    {
        public override void VisitVariantSectionNode(VariantSectionNode node)
        {
            RecordTypeNode record = node.ParentNodeOfType<RecordTypeNode>();
            ITypeDeclaration typeDeclaration = record.ParentNodeOfType<ITypeDeclaration>();
            AddHit(node,
                AstNode.ToCode(typeDeclaration.FirstNameNode, record.RecordKeywordNode) + " ... " +
                AstNode.ToCode(node.CaseKeywordNode, node.OfKeywordNode));
            base.VisitVariantSectionNode(node);
        }
    }
}
