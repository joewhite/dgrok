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
using System.Text;

namespace DGrok.Framework
{
    public enum RuleType
    {
        AddOp,
        ArrayType,
        AssemblerStatement,
        AssemblyAttribute,
        Atom,
        BareInherited,
        Block,
        CaseSelector,
        CaseStatement,
        ClassHelperType,
        ClassOfType,
        ClassType,
        ConstantDecl,
        ConstSection,
        Directive,
        EnumeratedType,
        EnumeratedTypeElement,
        ExceptionItem,
        ExportsItem,
        ExportsSpecifier,
        ExportsStatement,
        Expression,
        ExpressionList,
        ExpressionOrAssignment,
        ExpressionOrRange,
        ExpressionOrRangeList,
        ExtendedIdent,
        Factor,
        FancyBlock,
        FieldDecl,
        FieldSection,
        FileType,
        ForStatement,
        Goal,
        GotoStatement,
        Ident,
        IdentList,
        IfStatement,
        ImplementationDecl,
        ImplementationSection,
        InitSection,
        InterfaceDecl,
        InterfaceSection,
        InterfaceType,
        LabelDeclSection,
        LabelId,
        MethodHeading,
        MethodImplementation,
        MethodOrProperty,
        MethodReturnType,
        MulOp,
        OpenArray,
        Package,
        PackedType,
        Parameter,
        ParameterExpression,
        ParameterType,
        ParenthesizedExpression,
        Particle,
        PointerType,
        PortabilityDirective,
        ProcedureType,
        Program,
        Property,
        PropertyDirective,
        QualifiedIdent,
        RaiseStatement,
        RecordFieldConstant,
        RecordHelperType,
        RecordType,
        RelOp,
        RepeatStatement,
        RequiresClause,
        SetLiteral,
        SetType,
        SimpleExpression,
        SimpleStatement,
        Statement,
        StatementList,
        StringType,
        Term,
        TryStatement,
        Type,
        TypedConstant,
        TypeDecl,
        TypeSection,
        UnaryOperator,
        Unit,
        UsedUnit,
        UsesClause,
        VarDecl,
        VariantGroup,
        VariantSection,
        VarSection,
        Visibility,
        VisibilitySection,
        VisibilitySectionContent,
        WhileStatement,
        WithStatement,
    }
}
