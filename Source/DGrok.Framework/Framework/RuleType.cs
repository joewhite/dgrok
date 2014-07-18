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
