// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public class Parser
    {
        private Dictionary<RuleType, Rule> _rules = new Dictionary<RuleType,Rule>();
        private IFrame _nextFrame;

        public Parser(IFrame frame)
        {
            _nextFrame = frame;
            #region AddOp
            AddTokenRule(RuleType.AddOp, TokenSets.AddOp);
            #endregion
            #region ArrayType
            AddRule(RuleType.ArrayType, LookAhead(TokenType.ArrayKeyword), delegate
            {
                AstNode array = ParseToken(TokenType.ArrayKeyword);
                AstNode openBracket = null;
                AstNode indexList = EmptyList();
                AstNode closeBracket = null;
                if (CanParseToken(TokenType.OpenBracket))
                {
                    openBracket = ParseToken(TokenType.OpenBracket);
                    indexList = ParseDelimitedList(RuleType.Type, TokenType.Comma);
                    closeBracket = ParseToken(TokenType.CloseBracket);
                }
                AstNode of = ParseToken(TokenType.OfKeyword);
                AstNode type = ParseRule(RuleType.Type);
                return new ArrayTypeNode(array, openBracket, indexList, closeBracket, of, type);
            });
            #endregion
            #region AssemblerStatement
            AddRule(RuleType.AssemblerStatement, LookAhead(TokenType.AsmKeyword), delegate
            {
                AstNode asm = ParseToken(TokenType.AsmKeyword);
                while (!CanParseToken(TokenType.EndKeyword))
                    MoveNext();
                AstNode end = ParseToken(TokenType.EndKeyword);
                return new AssemblerStatementNode(asm, end);
            });
            #endregion
            #region Atom
            AddRule(RuleType.Atom, TokenSets.Particle.LookAhead, delegate
            {
                AstNode node = ParseRule(RuleType.Particle);
                while (true)
                {
                    if (CanParseToken(TokenType.Dot))
                    {
                        AstNode dot = ParseToken(TokenType.Dot);
                        AstNode right = ParseRule(RuleType.ExtendedIdent);
                        node = new BinaryOperationNode(node, dot, right);
                    }
                    else if (CanParseToken(TokenType.Caret))
                    {
                        AstNode caret = ParseToken(TokenType.Caret);
                        node = new PointerDereferenceNode(node, caret);
                    }
                    else if (CanParseToken(TokenType.OpenBracket))
                    {
                        AstNode openDelimiter = ParseToken(TokenType.OpenBracket);
                        AstNode parameterList = ParseRule(RuleType.ExpressionList);
                        AstNode closeDelimiter = ParseToken(TokenType.CloseBracket);
                        node = new ParameterizedNode(node, openDelimiter, parameterList, closeDelimiter);
                    }
                    else if (CanParseToken(TokenType.OpenParenthesis))
                    {
                        AstNode openDelimiter = ParseToken(TokenType.OpenParenthesis);
                        AstNode parameterList;
                        if (CanParseRule(RuleType.ExpressionList))
                            parameterList = ParseRule(RuleType.ExpressionList);
                        else
                            parameterList = EmptyList();
                        AstNode closeDelimiter = ParseToken(TokenType.CloseParenthesis);
                        node = new ParameterizedNode(node, openDelimiter, parameterList, closeDelimiter);
                    }
                    else
                        break;
                }
                return node;
            });
            #endregion
            #region ClassOfType
            AddRule(RuleType.ClassOfType, delegate
            {
                return Peek(0) == TokenType.ClassKeyword && Peek(1) == TokenType.OfKeyword;
            }, delegate
            {
                AstNode theClass = ParseToken(TokenType.ClassKeyword);
                AstNode of = ParseToken(TokenType.OfKeyword);
                AstNode type = ParseRule(RuleType.QualifiedIdent);
                return new ClassOfNode(theClass, of, type);
            });
            #endregion
            #region ClassType
            AddRule(RuleType.ClassType, LookAhead(TokenType.ClassKeyword), delegate
            {
                AstNode theClass = ParseToken(TokenType.ClassKeyword);
                AstNode disposition = null;
                if (CanParseToken(TokenSets.ClassDisposition))
                    disposition = ParseToken(TokenSets.ClassDisposition);
                AstNode openParenthesis = null;
                AstNode inheritanceList = EmptyList();
                AstNode closeParenthesis = null;
                if (CanParseToken(TokenType.OpenParenthesis))
                {
                    openParenthesis = ParseToken(TokenType.OpenParenthesis);
                    inheritanceList = ParseDelimitedList(RuleType.QualifiedIdent, TokenType.Comma);
                    closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                }
                AstNode contents = EmptyList();
                AstNode end = null;
                if (!CanParseToken(TokenType.Semicolon))
                {
                    contents = ParseOptionalRuleList(RuleType.VisibilitySection);
                    end = ParseToken(TokenType.EndKeyword);
                }
                return new ClassTypeNode(theClass, disposition,
                    openParenthesis, inheritanceList, closeParenthesis, contents, end);
            });
            #endregion
            #region ConstantDecl
            AddRule(RuleType.ConstantDecl, TokenSets.Ident.LookAhead, delegate
            {
                AstNode name = ParseRule(RuleType.Ident);
                AstNode colon = null;
                AstNode type = null;
                if (CanParseToken(TokenType.Colon))
                {
                    colon = ParseToken(TokenType.Colon);
                    type = ParseRule(RuleType.Type);
                }
                AstNode equalSign = ParseToken(TokenType.EqualSign);
                AstNode value = ParseRule(RuleType.TypedConstant);
                AstNode portabilityDirectiveList = ParseOptionalRuleList(RuleType.PortabilityDirective);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new ConstantDeclNode(name, colon, type, equalSign, value,
                    portabilityDirectiveList, semicolon);
            });
            #endregion
            #region ConstSection
            AddRule(RuleType.ConstSection, TokenSets.ConstHeader.LookAhead, delegate
            {
                AstNode theConst = ParseToken(TokenSets.ConstHeader);
                AstNode constList = ParseRequiredRuleList(RuleType.ConstantDecl);
                return new ConstSectionNode(theConst, constList);
            });
            #endregion
            #region ContainsClause
            AddRule(RuleType.ContainsClause, LookAhead(TokenType.ContainsSemikeyword), delegate
            {
                AstNode contains = ParseToken(TokenType.ContainsSemikeyword);
                AstNode identList = ParseRule(RuleType.IdentList);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new ContainsClauseNode(contains, identList, semicolon);
            });
            #endregion
            #region Directive
            TokenSet parameterizedDirectives = new TokenSet("'dispid' or 'message'");
            parameterizedDirectives.Add(TokenType.DispIdSemikeyword);
            parameterizedDirectives.Add(TokenType.MessageSemikeyword);
            AddRule(RuleType.Directive, delegate{
                return
                    TokenSets.Directive.Contains(Peek(0)) ||
                    (Peek(0) == TokenType.Semicolon && TokenSets.Directive.Contains(Peek(1)));
            }, delegate
            {
                AstNode semicolon = null;
                if (CanParseToken(TokenType.Semicolon))
                    semicolon = ParseToken(TokenType.Semicolon);
                AstNode directive;
                AstNode value = null;
                AstNode data = EmptyList();
                if (CanParseToken(parameterizedDirectives))
                {
                    directive = ParseToken(parameterizedDirectives);
                    value = ParseRule(RuleType.Expression);
                }
                else if (CanParseToken(TokenType.ExternalSemikeyword))
                {
                    directive = ParseToken(TokenType.ExternalSemikeyword);
                    if (CanParseRule(RuleType.Expression))
                    {
                        value = ParseRule(RuleType.Expression);
                        data = ParseOptionalRuleList(RuleType.ExportsSpecifier);
                    }
                }
                else
                    directive = ParseToken(TokenSets.Directive);
                return new DirectiveNode(semicolon, directive, value, data);
            });
            #endregion
            #region EnumeratedType
            AddRule(RuleType.EnumeratedType, LookAhead(TokenType.OpenParenthesis), delegate
            {
                AstNode openParenthesis = ParseToken(TokenType.OpenParenthesis);
                AstNode itemList = ParseDelimitedList(RuleType.EnumeratedTypeElement, TokenType.Comma);
                AstNode closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                return new EnumeratedTypeNode(openParenthesis, itemList, closeParenthesis);
            });
            #endregion
            #region EnumeratedTypeElement
            AddRule(RuleType.EnumeratedTypeElement, TokenSets.Ident.LookAhead, delegate
            {
                AstNode name = ParseRule(RuleType.Ident);
                AstNode equalSign = null;
                AstNode value = null;
                if (CanParseToken(TokenType.EqualSign))
                {
                    equalSign = ParseToken(TokenType.EqualSign);
                    value = ParseRule(RuleType.Expression);
                }
                return new EnumeratedTypeElementNode(name, equalSign, value);
            });
            #endregion
            #region ExportsItem
            AddRule(RuleType.ExportsItem, TokenSets.Ident.LookAhead, delegate
            {
                AstNode name = ParseRule(RuleType.Ident);
                AstNode specifierList = ParseOptionalRuleList(RuleType.ExportsSpecifier);
                return new ExportsItemNode(name, specifierList);
            });
            #endregion
            #region ExportsSpecifier
            AddRule(RuleType.ExportsSpecifier, TokenSets.ExportsSpecifier.LookAhead, delegate
            {
                AstNode keyword = ParseToken(TokenSets.ExportsSpecifier);
                AstNode value = ParseRule(RuleType.Expression);
                return new ExportsSpecifierNode(keyword, value);
            });
            #endregion
            #region ExportsStatement
            AddRule(RuleType.ExportsStatement, LookAhead(TokenType.ExportsKeyword), delegate
            {
                AstNode exports = ParseToken(TokenType.ExportsKeyword);
                AstNode itemList = ParseDelimitedList(RuleType.ExportsItem, TokenType.Comma);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new ExportsStatementNode(exports, itemList, semicolon);
            });
            #endregion
            #region Expression
            AddRule(RuleType.Expression, TokenSets.Expression.LookAhead, delegate
            {
                AstNode node = ParseRule(RuleType.SimpleExpression);
                while (CanParseRule(RuleType.RelOp))
                {
                    AstNode theOperator = ParseRule(RuleType.RelOp);
                    AstNode right = ParseRule(RuleType.SimpleExpression);
                    node = new BinaryOperationNode(node, theOperator, right);
                }
                return node;
            });
            #endregion
            #region ExpressionList
            AddRule(RuleType.ExpressionList, TokenSets.Expression.LookAhead, delegate
            {
                return ParseDelimitedList(RuleType.Expression, TokenType.Comma);
            });
            #endregion
            #region ExpressionOrRange
            AddRule(RuleType.ExpressionOrRange, TokenSets.Expression.LookAhead, delegate
            {
                AstNode node = ParseRule(RuleType.SimpleExpression);
                if (CanParseToken(TokenType.DotDot))
                {
                    AstNode dotDot = ParseToken(TokenType.DotDot);
                    AstNode right = ParseRule(RuleType.SimpleExpression);
                    return new BinaryOperationNode(node, dotDot, right);
                }
                return node;
            });
            #endregion
            #region ExpressionOrRangeList
            AddRule(RuleType.ExpressionOrRangeList, TokenSets.Expression.LookAhead, delegate
            {
                return ParseDelimitedList(RuleType.ExpressionOrRange, TokenType.Comma);
            });
            #endregion
            #region ExtendedIdent
            AddRule(RuleType.ExtendedIdent, TokenSets.ExtendedIdent.LookAhead, delegate
            {
                Token token = (Token) ParseToken(TokenSets.ExtendedIdent);
                return token.WithTokenType(TokenType.Identifier);
            });
            #endregion
            #region Factor
            AddRule(RuleType.Factor, TokenSets.Expression.LookAhead, delegate
            {
                if (CanParseRule(RuleType.UnaryOperator))
                {
                    AstNode theOperator = ParseRule(RuleType.UnaryOperator);
                    AstNode operand = ParseRule(RuleType.Factor);
                    return new UnaryOperationNode(theOperator, operand);
                }
                else
                    return ParseRule(RuleType.Atom);
            });
            #endregion
            #region FieldDecl
            AddRule(RuleType.FieldDecl, delegate
            {
                return CanParseRule(RuleType.IdentList) && !CanParseRule(RuleType.Visibility);
            }, delegate
            {
                AstNode nameList = ParseRule(RuleType.IdentList);
                AstNode colon = ParseToken(TokenType.Colon);
                AstNode type = ParseRule(RuleType.Type);
                AstNode portabilityDirectiveList = ParseTokenList(TokenSets.PortabilityDirective);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new FieldDeclNode(nameList, colon, type, portabilityDirectiveList, semicolon);
            });
            #endregion
            #region FieldSection
            AddRule(RuleType.FieldSection, delegate
            {
                return
                    (Peek(0) == TokenType.VarKeyword) ||
                    (Peek(0) == TokenType.ClassKeyword && Peek(1) == TokenType.VarKeyword) ||
                    CanParseRule(RuleType.FieldDecl);
            }, delegate
            {
                AstNode theClass = null;
                AstNode var = null;
                if (CanParseToken(TokenType.ClassKeyword))
                {
                    theClass = ParseToken(TokenType.ClassKeyword);
                    var = ParseToken(TokenType.VarKeyword);
                }
                else if (CanParseToken(TokenType.VarKeyword))
                    var = ParseToken(TokenType.VarKeyword);
                AstNode fieldList = ParseOptionalRuleList(RuleType.FieldDecl);
                return new FieldSectionNode(theClass, var, fieldList);
            });
            #endregion
            #region FileType
            AddRule(RuleType.FileType, LookAhead(TokenType.FileKeyword), delegate
            {
                AstNode file = ParseToken(TokenType.FileKeyword);
                AstNode of = null;
                AstNode type = null;
                if (CanParseToken(TokenType.OfKeyword))
                {
                    of = ParseToken(TokenType.OfKeyword);
                    type = ParseRule(RuleType.QualifiedIdent);
                }
                return new FileTypeNode(file, of, type);
            });
            #endregion
            #region Goal
            Alternator goalAlternator = new Alternator();
            goalAlternator.AddRule(RuleType.Package);
            goalAlternator.AddRule(RuleType.Unit);
            AddRule(RuleType.Goal, goalAlternator.LookAhead, delegate
            {
                return goalAlternator.Execute(this);
            });
            #endregion
            #region GotoStatement
            AddRule(RuleType.GotoStatement, LookAhead(TokenType.GotoKeyword), delegate
            {
                AstNode theGoto = ParseToken(TokenType.GotoKeyword);
                AstNode labelId = ParseRule(RuleType.LabelId);
                return new GotoStatementNode(theGoto, labelId);
            });
            #endregion
            #region Ident
            AddRule(RuleType.Ident, TokenSets.Ident.LookAhead, delegate
            {
                Token token = ParseToken(TokenSets.Ident);
                return token.WithTokenType(TokenType.Identifier);
            });
            #endregion
            #region IdentList
            AddRule(RuleType.IdentList, TokenSets.Ident.LookAhead, delegate
            {
                return ParseDelimitedList(RuleType.Ident, TokenType.Comma);
            });
            #endregion
            #region ImplementationSection
            AddRule(RuleType.ImplementationSection, LookAhead(TokenType.ImplementationKeyword), delegate
            {
                AstNode implementation = ParseToken(TokenType.ImplementationKeyword);
                AstNode usesClause = null;
                if (CanParseRule(RuleType.UsesClause))
                    usesClause = ParseRule(RuleType.UsesClause);
                AstNode contents = EmptyList();
                return new UnitSectionNode(implementation, usesClause, contents);
            });
            #endregion
            #region InitSection
            AddRule(RuleType.InitSection, TokenSets.InitSection.LookAhead, delegate
            {
                AstNode initializationHeader = null;
                AstNode finalizationHeader = null;
                if (CanParseToken(TokenType.InitializationKeyword))
                {
                    initializationHeader = ParseToken(TokenType.InitializationKeyword);
                    if (CanParseToken(TokenType.FinalizationKeyword))
                        finalizationHeader = ParseToken(TokenType.FinalizationKeyword);
                }
                else if (CanParseToken(TokenType.BeginKeyword))
                    initializationHeader = ParseToken(TokenType.BeginKeyword);
                AstNode end = ParseToken(TokenType.EndKeyword);
                return new InitSectionNode(initializationHeader, null, finalizationHeader, null, end);
            });
            #endregion
            #region InterfaceDecl
            Alternator interfaceDeclAlternator = new Alternator();
            interfaceDeclAlternator.AddRule(RuleType.ConstSection);
            interfaceDeclAlternator.AddRule(RuleType.TypeSection);
            interfaceDeclAlternator.AddRule(RuleType.VarSection);
            interfaceDeclAlternator.AddRule(RuleType.MethodHeading);
            AddRule(RuleType.InterfaceDecl, interfaceDeclAlternator.LookAhead, delegate
            {
                return interfaceDeclAlternator.Execute(this);
            });
            #endregion
            #region InterfaceSection
            AddRule(RuleType.InterfaceSection, LookAhead(TokenType.InterfaceKeyword), delegate
            {
                AstNode theInterface = ParseToken(TokenType.InterfaceKeyword);
                AstNode usesClause = null;
                if (CanParseRule(RuleType.UsesClause))
                    usesClause = ParseRule(RuleType.UsesClause);
                AstNode contents = ParseOptionalRuleList(RuleType.InterfaceDecl);
                return new UnitSectionNode(theInterface, usesClause, contents);
            });
            #endregion
            #region InterfaceType
            AddRule(RuleType.InterfaceType, TokenSets.InterfaceType.LookAhead, delegate
            {
                AstNode theInterface = ParseToken(TokenSets.InterfaceType);
                AstNode openParenthesis = null;
                AstNode baseInterface = null;
                AstNode closeParenthesis = null;
                if (CanParseToken(TokenType.OpenParenthesis))
                {
                    openParenthesis = ParseToken(TokenType.OpenParenthesis);
                    baseInterface = ParseRule(RuleType.QualifiedIdent);
                    closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                }
                AstNode openBracket = null;
                AstNode guid = null;
                AstNode closeBracket = null;
                if (CanParseToken(TokenType.OpenBracket))
                {
                    openBracket = ParseToken(TokenType.OpenBracket);
                    guid = ParseRule(RuleType.Expression);
                    closeBracket = ParseToken(TokenType.CloseBracket);
                }
                AstNode methodAndPropertyList = ParseOptionalRuleList(RuleType.MethodOrProperty);
                AstNode end = ParseToken(TokenType.EndKeyword);
                return new InterfaceTypeNode(theInterface, openParenthesis, baseInterface, closeParenthesis,
                    openBracket, guid, closeBracket, methodAndPropertyList, end);
            });
            #endregion
            #region LabelDeclSection
            AddRule(RuleType.LabelDeclSection, LookAhead(TokenType.LabelKeyword), delegate
            {
                AstNode label = ParseToken(TokenType.LabelKeyword);
                AstNode labelList = ParseDelimitedList(RuleType.LabelId, TokenType.Comma);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new LabelDeclSectionNode(label, labelList, semicolon);
            });
            #endregion
            #region LabelId
            Alternator labelIdAlternator = new Alternator();
            labelIdAlternator.AddToken(TokenType.Number);
            labelIdAlternator.AddRule(RuleType.Ident);
            AddRule(RuleType.LabelId, TokenSets.LabelId.LookAhead, delegate
            {
                return labelIdAlternator.Execute(this);
            });
            #endregion
            #region MethodHeading
            AddRule(RuleType.MethodHeading, delegate
            {
                return
                    (Peek(0) == TokenType.ClassKeyword && TokenSets.MethodType.Contains(Peek(1))) ||
                    CanParseToken(TokenSets.MethodType);
            }, delegate
            {
                AstNode theClass = null;
                if (CanParseToken(TokenType.ClassKeyword))
                    theClass = ParseToken(TokenType.ClassKeyword);
                AstNode methodType = ParseToken(TokenSets.MethodType);
                AstNode name = ParseRule(RuleType.QualifiedIdent);
                if (CanParseToken(TokenType.EqualSign))
                {
                    AstNode interfaceMethod = name;
                    AstNode equalSign = ParseToken(TokenType.EqualSign);
                    AstNode implementationMethod = ParseRule(RuleType.Ident);
                    AstNode semicolon = ParseToken(TokenType.Semicolon);
                    return new MethodResolutionNode(methodType, interfaceMethod,
                        equalSign, implementationMethod, semicolon);
                }
                else
                {
                    AstNode openParenthesis = null;
                    AstNode parameterList = EmptyList();
                    AstNode closeParenthesis = null;
                    if (CanParseToken(TokenType.OpenParenthesis))
                    {
                        openParenthesis = ParseToken(TokenType.OpenParenthesis);
                        if (CanParseRule(RuleType.Parameter))
                            parameterList = ParseDelimitedList(RuleType.Parameter, TokenType.Semicolon);
                        closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                    }
                    AstNode colon = null;
                    AstNode returnType = null;
                    if (CanParseToken(TokenType.Colon))
                    {
                        colon = ParseToken(TokenType.Colon);
                        returnType = ParseRule(RuleType.MethodReturnType);
                    }
                    AstNode directiveList = ParseOptionalRuleList(RuleType.Directive);
                    AstNode semicolon = ParseToken(TokenType.Semicolon);
                    return new MethodHeadingNode(theClass, methodType, name,
                        openParenthesis, parameterList, closeParenthesis, colon, returnType,
                        directiveList, semicolon);
                }
            });
            #endregion
            #region MethodOrProperty
            Alternator methodOrPropertyAlternator = new Alternator();
            methodOrPropertyAlternator.AddRule(RuleType.MethodHeading);
            methodOrPropertyAlternator.AddRule(RuleType.Property);
            AddRule(RuleType.MethodOrProperty, methodOrPropertyAlternator.LookAhead, delegate
            {
                return methodOrPropertyAlternator.Execute(this);
            });
            #endregion
            #region MethodReturnType
            Alternator methodReturnTypeAlternator = new Alternator();
            methodReturnTypeAlternator.AddToken(TokenType.StringKeyword);
            methodReturnTypeAlternator.AddRule(RuleType.QualifiedIdent);
            AddRule(RuleType.MethodReturnType, methodReturnTypeAlternator.LookAhead, delegate
            {
                return methodReturnTypeAlternator.Execute(this);
            });
            #endregion
            #region MulOp
            AddTokenRule(RuleType.MulOp, TokenSets.MulOp);
            #endregion
            #region OpenArray
            Alternator openArrayAlternator = new Alternator();
            openArrayAlternator.AddRule(RuleType.QualifiedIdent);
            openArrayAlternator.AddToken(TokenType.ConstKeyword);
            openArrayAlternator.AddToken(TokenType.FileKeyword);
            openArrayAlternator.AddToken(TokenType.StringKeyword);
            AddRule(RuleType.OpenArray, LookAhead(TokenType.ArrayKeyword), delegate
            {
                AstNode array = ParseToken(TokenType.ArrayKeyword);
                AstNode of = ParseToken(TokenType.OfKeyword);
                AstNode type = openArrayAlternator.Execute(this);
                return new OpenArrayNode(array, of, type);
            });
            #endregion
            #region Package
            AddRule(RuleType.Package, LookAhead(TokenType.PackageSemikeyword), delegate
            {
                AstNode package = ParseToken(TokenType.PackageSemikeyword);
                AstNode name = ParseRule(RuleType.Ident);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                AstNode requiresClause = null;
                if (CanParseRule(RuleType.RequiresClause))
                    requiresClause = ParseRule(RuleType.RequiresClause);
                AstNode containsClause = null;
                if (CanParseRule(RuleType.ContainsClause))
                    containsClause = ParseRule(RuleType.ContainsClause);
                AstNode end = ParseToken(TokenType.EndKeyword);
                AstNode dot = ParseToken(TokenType.Dot);
                return new PackageNode(package, name, semicolon,
                    requiresClause, containsClause, end, dot);
            });
            #endregion
            #region PackedType
            AddRule(RuleType.PackedType, LookAhead(TokenType.PackedKeyword), delegate
            {
                AstNode packed = ParseToken(TokenType.PackedKeyword);
                AstNode type = ParseRule(RuleType.Type);
                return new PackedTypeNode(packed, type);
            });
            #endregion
            #region Parameter
            AddRule(RuleType.Parameter, TokenSets.Parameter.LookAhead, delegate
            {
                AstNode modifier = null;
                if (CanParseToken(TokenSets.ParameterModifier))
                    modifier = ParseToken(TokenSets.ParameterModifier);
                AstNode names = ParseRule(RuleType.IdentList);
                AstNode colon = null;
                AstNode type = null;
                if (CanParseToken(TokenType.Colon))
                {
                    colon = ParseToken(TokenType.Colon);
                    type = ParseRule(RuleType.ParameterType);
                }
                AstNode equalSign = null;
                AstNode defaultValue = null;
                if (CanParseToken(TokenType.EqualSign))
                {
                    equalSign = ParseToken(TokenType.EqualSign);
                    defaultValue = ParseRule(RuleType.Expression);
                }
                return new ParameterNode(modifier, names, colon, type, equalSign, defaultValue);
            });
            #endregion
            #region ParameterType
            Alternator parameterTypeAlternator = new Alternator();
            parameterTypeAlternator.AddRule(RuleType.QualifiedIdent);
            parameterTypeAlternator.AddRule(RuleType.OpenArray);
            parameterTypeAlternator.AddToken(TokenType.FileKeyword);
            parameterTypeAlternator.AddToken(TokenType.StringKeyword);
            AddRule(RuleType.ParameterType, parameterTypeAlternator.LookAhead, delegate
            {
                return parameterTypeAlternator.Execute(this);
            });
            #endregion
            #region ParenthesizedExpression
            AddRule(RuleType.ParenthesizedExpression, LookAhead(TokenType.OpenParenthesis), delegate
            {
                AstNode openParenthesis = ParseToken(TokenType.OpenParenthesis);
                AstNode expression = ParseRule(RuleType.Expression);
                AstNode closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                return new ParenthesizedExpressionNode(openParenthesis, expression, closeParenthesis);
            });
            #endregion
            #region Particle
            Alternator particleAlternator = new Alternator();
            particleAlternator.AddToken(TokenType.NilKeyword);
            particleAlternator.AddToken(TokenType.Number);
            particleAlternator.AddToken(TokenType.StringLiteral);
            particleAlternator.AddRule(RuleType.Ident);
            particleAlternator.AddRule(RuleType.ParenthesizedExpression);
            particleAlternator.AddRule(RuleType.SetLiteral);
            AddRule(RuleType.Particle, TokenSets.Particle.LookAhead, delegate
            {
                return particleAlternator.Execute(this);
            });
            #endregion
            #region PointerType
            AddRule(RuleType.PointerType, LookAhead(TokenType.Caret), delegate
            {
                AstNode caret = ParseToken(TokenType.Caret);
                AstNode type = ParseRule(RuleType.QualifiedIdent);
                return new PointerTypeNode(caret, type);
            });
            #endregion
            #region PortabilityDirective
            AddTokenRule(RuleType.PortabilityDirective, TokenSets.PortabilityDirective);
            #endregion
            #region ProcedureType
            AddRule(RuleType.ProcedureType, TokenSets.MethodType.LookAhead, delegate
            {
                AstNode methodType = ParseToken(TokenSets.MethodType);
                AstNode openParenthesis = null;
                AstNode parameterList = EmptyList();
                AstNode closeParenthesis = null;
                if (CanParseToken(TokenType.OpenParenthesis))
                {
                    openParenthesis = ParseToken(TokenType.OpenParenthesis);
                    if (CanParseRule(RuleType.Parameter))
                        parameterList = ParseDelimitedList(RuleType.Parameter, TokenType.Semicolon);
                    closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                }
                AstNode colon = null;
                AstNode returnType = null;
                if (CanParseToken(TokenType.Colon))
                {
                    colon = ParseToken(TokenType.Colon);
                    returnType = ParseRule(RuleType.MethodReturnType);
                }
                AstNode firstDirectives = ParseOptionalRuleList(RuleType.Directive);
                AstNode of = null;
                AstNode theObject = null;
                if (CanParseToken(TokenType.OfKeyword))
                {
                    of = ParseToken(TokenType.OfKeyword);
                    theObject = ParseToken(TokenType.ObjectKeyword);
                }
                AstNode secondDirectives = ParseOptionalRuleList(RuleType.Directive);
                return new ProcedureTypeNode(methodType, openParenthesis, parameterList, closeParenthesis,
                    colon, returnType, firstDirectives, of, theObject, secondDirectives);
            });
            #endregion
            #region Property
            AddRule(RuleType.Property, delegate
            {
                return
                    (Peek(0) == TokenType.ClassKeyword && Peek(1) == TokenType.PropertyKeyword) ||
                    (Peek(0) == TokenType.PropertyKeyword);
            }, delegate
            {
                AstNode theClass = null;
                if (CanParseToken(TokenType.ClassKeyword))
                    theClass = ParseToken(TokenType.ClassKeyword);
                AstNode property = ParseToken(TokenType.PropertyKeyword);
                AstNode name = ParseRule(RuleType.Ident);
                AstNode openBracket = null;
                AstNode parameterList = EmptyList();
                AstNode closeBracket = null;
                if (CanParseToken(TokenType.OpenBracket))
                {
                    openBracket = ParseToken(TokenType.OpenBracket);
                    parameterList = ParseDelimitedList(RuleType.Parameter, TokenType.Semicolon);
                    closeBracket = ParseToken(TokenType.CloseBracket);
                }
                AstNode colon = null;
                AstNode type = null;
                if (CanParseToken(TokenType.Colon))
                {
                    colon = ParseToken(TokenType.Colon);
                    type = ParseRule(RuleType.MethodReturnType);
                }
                AstNode directiveList = ParseOptionalRuleList(RuleType.PropertyDirective);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new PropertyNode(theClass, property, name, openBracket, parameterList, closeBracket,
                    colon, type, directiveList, semicolon);
            });
            #endregion
            #region PropertyDirective
            AddRule(RuleType.PropertyDirective, delegate
            {
                return
                    CanParseToken(TokenSets.ParameterizedPropertyDirective) ||
                    CanParseToken(TokenSets.ParameterlessPropertyDirective) ||
                    (Peek(0) == TokenType.Semicolon && Peek(1) == TokenType.DefaultSemikeyword);
            }, delegate
            {
                AstNode semicolon = null;
                AstNode directive;
                AstNode value = null;
                AstNode data = EmptyList();
                if (CanParseToken(TokenType.Semicolon))
                {
                    semicolon = ParseToken(TokenType.Semicolon);
                    directive = ParseToken(TokenType.DefaultSemikeyword);
                }
                else if (CanParseToken(TokenSets.ParameterizedPropertyDirective))
                {
                    directive = ParseToken(TokenSets.ParameterizedPropertyDirective);
                    value = ParseRule(RuleType.Expression);
                }
                else
                    directive = ParseToken(TokenSets.ParameterlessPropertyDirective);
                return new DirectiveNode(semicolon, directive, value, data);
            });
            #endregion
            #region QualifiedIdent
            AddRule(RuleType.QualifiedIdent, TokenSets.Ident.LookAhead, delegate
            {
                AstNode node = ParseRule(RuleType.Ident);
                while (CanParseToken(TokenType.Dot))
                {
                    AstNode dot = ParseToken(TokenType.Dot);
                    AstNode right = ParseRule(RuleType.ExtendedIdent);
                    node = new BinaryOperationNode(node, dot, right);
                }
                return node;
            });
            #endregion
            #region RaiseStatement
            AddRule(RuleType.RaiseStatement, LookAhead(TokenType.RaiseKeyword), delegate
            {
                AstNode raise = ParseToken(TokenType.RaiseKeyword);
                AstNode exception = null;
                AstNode at = null;
                AstNode address = null;
                if (CanParseRule(RuleType.Expression))
                {
                    exception = ParseRule(RuleType.Expression);
                    if (CanParseToken(TokenType.AtSemikeyword))
                    {
                        at = ParseToken(TokenType.AtSemikeyword);
                        address = ParseRule(RuleType.Expression);
                    }
                }
                return new RaiseStatementNode(raise, exception, at, address);
            });
            #endregion
            #region RecordFieldConstant
            AddRule(RuleType.RecordFieldConstant, delegate
            {
                return CanParseRule(RuleType.QualifiedIdent);
            }, delegate
            {
                AstNode name = ParseRule(RuleType.QualifiedIdent);
                AstNode colon = ParseToken(TokenType.Colon);
                AstNode value = ParseRule(RuleType.TypedConstant);
                return new RecordFieldConstantNode(name, colon, value);
            });
            #endregion
            #region RecordType
            AddRule(RuleType.RecordType, LookAhead(TokenType.RecordKeyword), delegate
            {
                AstNode record = ParseToken(TokenType.RecordKeyword);
                AstNode contents = ParseOptionalRuleList(RuleType.VisibilitySection);
                AstNode variantSection = null;
                AstNode end = ParseToken(TokenType.EndKeyword);
                return new RecordTypeNode(record, contents, variantSection, end);
            });
            #endregion
            #region RelOp
            AddTokenRule(RuleType.RelOp, TokenSets.RelOp);
            #endregion
            #region RequiresClause
            AddRule(RuleType.RequiresClause, LookAhead(TokenType.RequiresSemikeyword), delegate
            {
                AstNode requires = ParseToken(TokenType.RequiresSemikeyword);
                AstNode identList = ParseRule(RuleType.IdentList);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new RequiresClauseNode(requires, identList, semicolon);
            });
            #endregion
            #region SetLiteral
            AddRule(RuleType.SetLiteral, LookAhead(TokenType.OpenBracket), delegate
            {
                AstNode openBracket = ParseToken(TokenType.OpenBracket);
                AstNode itemList;
                if (CanParseRule(RuleType.ExpressionOrRangeList))
                    itemList = ParseRule(RuleType.ExpressionOrRangeList);
                else
                    itemList = EmptyList();
                AstNode closeBracket = ParseToken(TokenType.CloseBracket);
                return new SetLiteralNode(openBracket, itemList, closeBracket);
            });
            #endregion
            #region SetType
            AddRule(RuleType.SetType, LookAhead(TokenType.SetKeyword), delegate
            {
                AstNode set = ParseToken(TokenType.SetKeyword);
                AstNode of = ParseToken(TokenType.OfKeyword);
                AstNode type = ParseRule(RuleType.Type);
                return new SetOfNode(set, of, type);
            });
            #endregion
            #region SimpleExpression
            AddRule(RuleType.SimpleExpression, TokenSets.Expression.LookAhead, delegate
            {
                AstNode node = ParseRule(RuleType.Term);
                while (CanParseRule(RuleType.AddOp))
                {
                    AstNode theOperator = ParseRule(RuleType.AddOp);
                    AstNode right = ParseRule(RuleType.Term);
                    node = new BinaryOperationNode(node, theOperator, right);
                }
                return node;
            });
            #endregion
            #region StringType
            AddRule(RuleType.StringType, LookAhead(TokenType.StringKeyword), delegate
            {
                AstNode theString = ParseToken(TokenType.StringKeyword);
                if (CanParseToken(TokenType.OpenBracket))
                {
                    AstNode openBracket = ParseToken(TokenType.OpenBracket);
                    AstNode length = ParseRule(RuleType.Expression);
                    AstNode closeBracket = ParseToken(TokenType.CloseBracket);
                    return new StringOfLengthNode(theString, openBracket, length, closeBracket);
                }
                else
                    return theString;
            });
            #endregion
            #region Term
            AddRule(RuleType.Term, TokenSets.Expression.LookAhead, delegate
            {
                AstNode node = ParseRule(RuleType.Factor);
                while (CanParseRule(RuleType.MulOp))
                {
                    AstNode theOperator = ParseRule(RuleType.MulOp);
                    AstNode right = ParseRule(RuleType.Factor);
                    node = new BinaryOperationNode(node, theOperator, right);
                }
                return node;
            });
            #endregion
            #region Type
            Alternator typeAlternator = new Alternator();
            typeAlternator.AddRule(RuleType.ArrayType);
            typeAlternator.AddRule(RuleType.ClassOfType);
            typeAlternator.AddRule(RuleType.ClassType);
            typeAlternator.AddRule(RuleType.EnumeratedType);
            typeAlternator.AddRule(RuleType.ExpressionOrRange);
            typeAlternator.AddRule(RuleType.FileType);
            typeAlternator.AddRule(RuleType.InterfaceType);
            typeAlternator.AddRule(RuleType.PackedType);
            typeAlternator.AddRule(RuleType.PointerType);
            typeAlternator.AddRule(RuleType.ProcedureType);
            typeAlternator.AddRule(RuleType.RecordType);
            typeAlternator.AddRule(RuleType.SetType);
            typeAlternator.AddRule(RuleType.StringType);
            AddRule(RuleType.Type, typeAlternator.LookAhead, delegate
            {
                return typeAlternator.Execute(this);
            });
            #endregion
            #region TypedConstant
            AddRule(RuleType.TypedConstant, TokenSets.Expression.LookAhead, delegate
            {
                IFrame originalFrame = _nextFrame;
                try
                {
                    return ParseRule(RuleType.Expression);
                }
                catch (ParseException)
                {
                    _nextFrame = originalFrame;
                }
                AstNode openParenthesis;
                AstNode itemList;
                AstNode closeParenthesis;
                try
                {
                    openParenthesis = ParseToken(TokenType.OpenParenthesis);
                    itemList = ParseDelimitedList(RuleType.TypedConstant, TokenType.Comma);
                    closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                }
                catch (ParseException)
                {
                    _nextFrame = originalFrame;
                    openParenthesis = ParseToken(TokenType.OpenParenthesis);
                    itemList = ParseDelimitedList(RuleType.RecordFieldConstant, TokenType.Semicolon);
                    closeParenthesis = ParseToken(TokenType.CloseParenthesis);
                }
                return new ConstantListNode(openParenthesis, itemList, closeParenthesis);
            });
            #endregion
            #region TypeDecl
            AddRule(RuleType.TypeDecl, TokenSets.Ident.LookAhead, delegate
            {
                AstNode name = ParseRule(RuleType.Ident);
                AstNode equalSign = ParseToken(TokenType.EqualSign);
                if (TokenSets.ForwardableType.Contains(Peek(0)) && Peek(1) == TokenType.Semicolon)
                {
                    AstNode type = ParseToken(TokenSets.ForwardableType);
                    AstNode semicolon = ParseToken(TokenType.Semicolon);
                    return new TypeForwardDeclarationNode(name, equalSign, type, semicolon);
                }
                else
                {
                    AstNode typeKeyword = TryParseToken(TokenType.TypeKeyword);
                    AstNode type = ParseRule(RuleType.Type);
                    AstNode portabilityDirectiveList = ParseOptionalRuleList(RuleType.PortabilityDirective);
                    AstNode semicolon = ParseToken(TokenType.Semicolon);
                    return new TypeDeclNode(name, equalSign, typeKeyword, type,
                        portabilityDirectiveList, semicolon);
                }
            });
            #endregion
            #region TypeSection
            AddRule(RuleType.TypeSection, LookAhead(TokenType.TypeKeyword), delegate
            {
                AstNode type = ParseToken(TokenType.TypeKeyword);
                AstNode typeList = ParseRequiredRuleList(RuleType.TypeDecl);
                return new TypeSectionNode(type, typeList);
            });
            #endregion
            #region UnaryOperator
            AddTokenRule(RuleType.UnaryOperator, TokenSets.UnaryOperator);
            #endregion
            #region Unit
            AddRule(RuleType.Unit, LookAhead(TokenType.UnitKeyword), delegate
            {
                AstNode unit = ParseToken(TokenType.UnitKeyword);
                AstNode unitName = ParseRule(RuleType.Ident);
                AstNode portabilityDirectives = ParseTokenList(TokenSets.PortabilityDirective);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                AstNode interfaceSection = ParseRule(RuleType.InterfaceSection);
                AstNode implementationSection = ParseRule(RuleType.ImplementationSection);
                AstNode initSection = ParseRule(RuleType.InitSection);
                AstNode dot = ParseToken(TokenType.Dot);
                return new UnitNode(unit, unitName, portabilityDirectives, semicolon,
                    interfaceSection, implementationSection, initSection, dot);
            });
            #endregion
            #region UsedUnit
            AddRule(RuleType.UsedUnit, TokenSets.Ident.LookAhead, delegate
            {
                AstNode name = ParseRule(RuleType.Ident);
                AstNode theIn = null;
                AstNode fileName = null;
                if (CanParseToken(TokenType.InKeyword))
                {
                    theIn = ParseToken(TokenType.InKeyword);
                    fileName = ParseToken(TokenType.StringLiteral);
                }
                return new UsedUnitNode(name, theIn, fileName);
            });
            #endregion
            #region UsesClause
            AddRule(RuleType.UsesClause, LookAhead(TokenType.UsesKeyword), delegate
            {
                AstNode uses = ParseToken(TokenType.UsesKeyword);
                AstNode unitList = ParseDelimitedList(RuleType.UsedUnit, TokenType.Comma);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new UsesClauseNode(uses, unitList, semicolon);
            });
            #endregion
            #region VarDecl
            AddRule(RuleType.VarDecl, TokenSets.Ident.LookAhead, delegate
            {
                AstNode names = ParseRule(RuleType.IdentList);
                AstNode colon = ParseToken(TokenType.Colon);
                AstNode type = ParseRule(RuleType.Type);
                AstNode absolute = null;
                AstNode absoluteAddress = null;
                AstNode equalSign = null;
                AstNode value = null;
                if (CanParseToken(TokenType.AbsoluteSemikeyword))
                {
                    absolute = ParseToken(TokenType.AbsoluteSemikeyword);
                    absoluteAddress = ParseRule(RuleType.Expression);
                }
                else if (CanParseToken(TokenType.EqualSign))
                {
                    equalSign = ParseToken(TokenType.EqualSign);
                    value = ParseRule(RuleType.Expression);
                }
                AstNode portabilityDirectiveList = ParseOptionalRuleList(RuleType.PortabilityDirective);
                AstNode semicolon = ParseToken(TokenType.Semicolon);
                return new VarDeclNode(names, colon, type, absolute, absoluteAddress,
                    equalSign, value, portabilityDirectiveList, semicolon);
            });
            #endregion
            #region VarSection
            AddRule(RuleType.VarSection, LookAhead(TokenType.VarKeyword), delegate
            {
                AstNode var = ParseToken(TokenType.VarKeyword);
                AstNode varList = ParseRequiredRuleList(RuleType.VarDecl);
                return new VarSectionNode(var, varList);
            });
            #endregion
            #region Visibility
            AddRule(RuleType.Visibility, TokenSets.Visibility.LookAhead, delegate
            {
                AstNode strict = null;
                if (CanParseToken(TokenType.StrictSemikeyword))
                    strict = ParseToken(TokenType.StrictSemikeyword);
                AstNode visibility = ParseToken(TokenSets.VisibilitySingleWord);
                return new VisibilityNode(strict, visibility);
            });
            #endregion
            #region VisibilitySection
            AddRule(RuleType.VisibilitySection, delegate
            {
                return
                    CanParseRule(RuleType.Visibility) ||
                    CanParseRule(RuleType.VisibilitySectionContent);
            }, delegate
            {
                AstNode visibility = null;
                if (CanParseRule(RuleType.Visibility))
                    visibility = ParseRule(RuleType.Visibility);
                AstNode contents = ParseOptionalRuleList(RuleType.VisibilitySectionContent);
                return new VisibilitySectionNode(visibility, contents);
            });
            #endregion
            #region VisibilitySectionContent
            Alternator visibilitySectionContentAlternator = new Alternator();
            visibilitySectionContentAlternator.AddRule(RuleType.FieldSection);
            visibilitySectionContentAlternator.AddRule(RuleType.MethodOrProperty);
            visibilitySectionContentAlternator.AddRule(RuleType.ConstSection);
            visibilitySectionContentAlternator.AddRule(RuleType.TypeSection);
            AddRule(RuleType.VisibilitySectionContent, visibilitySectionContentAlternator.LookAhead, delegate
            {
                return visibilitySectionContentAlternator.Execute(this);
            });
            #endregion
        }

        private static IFrame FrameFromTokens(IEnumerable<Token> tokens)
        {
            IFrame firstFrame = EofFrame.Instance;
            IFrame previousFrame = null;
            foreach (Token token in tokens)
            {
                IFrame frame = new Frame(token);
                if (previousFrame != null)
                    previousFrame.Next = frame;
                else
                    firstFrame = frame;
                previousFrame = frame;
            }
            return firstFrame;
        }
        public static Parser FromFrame(IFrame frame)
        {
            return new Parser(frame);
        }
        public static Parser FromText(string text, CompilerDefines compilerDefines)
        {
            Lexer lexer = new Lexer(text);
            TokenFilter filter = new TokenFilter(lexer.Tokens, compilerDefines);
            return FromTokens(filter.Tokens);
        }
        private static Parser FromTokens(IEnumerable<Token> tokens)
        {
            return FromFrame(FrameFromTokens(tokens));
        }

        public bool AtEof
        {
            get { return _nextFrame.IsEof; }
        }

        private void AddRule(RuleType ruleType, Predicate<Parser> lookAhead, RuleDelegate evaluate)
        {
            _rules[ruleType] = new Rule(this, ruleType, lookAhead, evaluate);
        }
        private void AddTokenRule(RuleType ruleType, TokenSet tokenSet)
        {
            AddRule(ruleType, delegate { return CanParseToken(tokenSet); },
                delegate { return ParseToken(tokenSet); });
        }
        public bool CanParseRule(RuleType ruleType)
        {
            return GetRule(ruleType).CanParse();
        }
        public bool CanParseToken(TokenSet tokenSet)
        {
            return tokenSet.Contains(Peek(0));
        }
        private bool CanParseToken(TokenType tokenType)
        {
            return Peek(0) == tokenType;
        }
        private ListNode EmptyList()
        {
            return new ListNode(new AstNode[0]);
        }
        public ParseException Failure(string expected)
        {
            return new ParseException("Expected " + expected + " but was " + _nextFrame.DisplayName,
                _nextFrame.Offset);
        }
        public Rule GetRule(RuleType ruleType)
        {
            if (_rules.ContainsKey(ruleType))
                return _rules[ruleType];
            throw new ArgumentException("Parse rule " + ruleType + " not found");
        }
        private Predicate<Parser> LookAhead(TokenType tokenType)
        {
            return delegate { return CanParseToken(tokenType); };
        }
        private void MoveNext()
        {
            _nextFrame = _nextFrame.Next;
        }
        private ListNode ParseDelimitedList(RuleType itemRule, TokenType delimiterType)
        {
            List<AstNode> items = new List<AstNode>();
            do
            {
                AstNode item = ParseRule(itemRule);
                AstNode delimiter = null;
                if (CanParseToken(delimiterType))
                    delimiter = ParseToken(delimiterType);
                items.Add(new DelimitedItemNode(item, delimiter));
            } while (CanParseRule(itemRule));
            return new ListNode(items);
        }
        private ListNode ParseOptionalRuleList(RuleType ruleType)
        {
            List<AstNode> items = new List<AstNode>();
            while (CanParseRule(ruleType))
                items.Add(ParseRule(ruleType));
            return new ListNode(items);
        }
        private ListNode ParseRequiredRuleList(RuleType ruleType)
        {
            List<AstNode> items = new List<AstNode>();
            do
            {
                items.Add(ParseRule(ruleType));
            }
            while (CanParseRule(ruleType));
            return new ListNode(items);
        }
        public AstNode ParseRule(RuleType ruleType)
        {
            if (_rules.ContainsKey(ruleType))
                return _rules[ruleType].Execute();
            else
                throw new ArgumentException("Unrecognized parse rule " + ruleType);
        }
        public Token ParseToken(TokenSet tokenSet)
        {
            Token result = _nextFrame.ParseToken(tokenSet);
            _nextFrame = _nextFrame.Next;
            return result;
        }
        private Token ParseToken(TokenType tokenType)
        {
            return ParseToken(new TokenSet(tokenType));
        }
        private ListNode ParseTokenList(TokenSet tokenSet)
        {
            List<AstNode> nodes = new List<AstNode>();
            while (CanParseToken(tokenSet))
                nodes.Add(ParseToken(tokenSet));
            return new ListNode(nodes);
        }
        public TokenType Peek(int offset)
        {
            IFrame frame = _nextFrame;
            while (offset > 0)
            {
                frame = frame.Next;
                --offset;
            }
            return frame.TokenType;
        }
        private Token TryParseToken(TokenType tokenType)
        {
            if (CanParseToken(tokenType))
                return ParseToken(tokenType);
            else
                return null;
        }
    }
}
