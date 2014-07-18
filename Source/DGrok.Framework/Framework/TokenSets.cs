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
    public static class TokenSets
    {
        private static TokenSet _addOp;
        private static TokenSet _block;
        private static TokenSet _classDisposition;
        private static TokenSet _constHeader;
        private static TokenSet _directive;
        private static TokenSet _exportsSpecifier;
        private static TokenSet _expression;
        private static TokenSet _extendedIdent;
        private static TokenSet _forDirection;
        private static TokenSet _forwardableType;
        private static TokenSet _ident;
        private static TokenSet _initSection;
        private static TokenSet _interfaceType;
        private static TokenSet _keyword;
        private static TokenSet _labelId;
        private static TokenSet _methodType;
        private static TokenSet _mulOp;
        private static TokenSet _parameter;
        private static TokenSet _parameterizedPropertyDirective;
        private static TokenSet _parameterlessPropertyDirective;
        private static TokenSet _parameterModifier;
        private static TokenSet _particle;
        private static TokenSet _portabilityDirective;
        private static TokenSet _program;
        private static TokenSet _relOp;
        private static TokenSet _semikeyword;
        private static TokenSet _simpleParameterType;
        private static TokenSet _unaryOperator;
        private static TokenSet _uses;
        private static TokenSet _varHeader;
        private static TokenSet _visibility;
        private static TokenSet _visibilitySingleWord;

        static TokenSets()
        {
            _addOp = new TokenSet("addition operator");
            _addOp.Add(TokenType.PlusSign);
            _addOp.Add(TokenType.MinusSign);
            _addOp.Add(TokenType.OrKeyword);
            _addOp.Add(TokenType.XorKeyword);

            _block = new TokenSet("block");
            _block.Add(TokenType.BeginKeyword);
            _block.Add(TokenType.AsmKeyword);

            _classDisposition = new TokenSet("class disposition");
            _classDisposition.Add(TokenType.AbstractSemikeyword);
            _classDisposition.Add(TokenType.SealedSemikeyword);

            _constHeader = new TokenSet("const section");
            _constHeader.Add(TokenType.ConstKeyword);
            _constHeader.Add(TokenType.ResourceStringKeyword);

            _directive = new TokenSet("directive");
            _directive.Add(TokenType.AbstractSemikeyword);
            _directive.Add(TokenType.AssemblerSemikeyword);
            _directive.Add(TokenType.CdeclSemikeyword);
            _directive.Add(TokenType.DispIdSemikeyword);
            _directive.Add(TokenType.DynamicSemikeyword);
            _directive.Add(TokenType.ExportSemikeyword);
            _directive.Add(TokenType.ExternalSemikeyword);
            _directive.Add(TokenType.FarSemikeyword);
            _directive.Add(TokenType.FinalSemikeyword);
            _directive.Add(TokenType.ForwardSemikeyword);
            _directive.Add(TokenType.InlineKeyword);
            _directive.Add(TokenType.LocalSemikeyword);
            _directive.Add(TokenType.MessageSemikeyword);
            _directive.Add(TokenType.NearSemikeyword);
            _directive.Add(TokenType.OverloadSemikeyword);
            _directive.Add(TokenType.OverrideSemikeyword);
            _directive.Add(TokenType.PascalSemikeyword);
            _directive.Add(TokenType.RegisterSemikeyword);
            _directive.Add(TokenType.ReintroduceSemikeyword);
            _directive.Add(TokenType.SafecallSemikeyword);
            _directive.Add(TokenType.StaticSemikeyword);
            _directive.Add(TokenType.StdcallSemikeyword);
            _directive.Add(TokenType.VarArgsSemikeyword);
            _directive.Add(TokenType.VirtualSemikeyword);
            // also includes PortabilityDirective, see below

            _exportsSpecifier = new TokenSet("'index' or 'name'");
            _exportsSpecifier.Add(TokenType.IndexSemikeyword);
            _exportsSpecifier.Add(TokenType.NameSemikeyword);

            _forDirection = new TokenSet("'to' or 'downto'");
            _forDirection.Add(TokenType.ToKeyword);
            _forDirection.Add(TokenType.DownToKeyword);

            _forwardableType = new TokenSet("forwardable type");
            _forwardableType.Add(TokenType.ClassKeyword);
            _forwardableType.Add(TokenType.DispInterfaceKeyword);
            _forwardableType.Add(TokenType.InterfaceKeyword);

            _initSection = new TokenSet("initialization section");
            _initSection.Add(TokenType.AsmKeyword);
            _initSection.Add(TokenType.BeginKeyword);
            _initSection.Add(TokenType.InitializationKeyword);
            _initSection.Add(TokenType.EndKeyword);

            _interfaceType = new TokenSet("interface type");
            _interfaceType.Add(TokenType.InterfaceKeyword);
            _interfaceType.Add(TokenType.DispInterfaceKeyword);

            _keyword = new TokenSet("keyword");
            _keyword.Add(TokenType.AndKeyword);
            _keyword.Add(TokenType.ArrayKeyword);
            _keyword.Add(TokenType.AsKeyword);
            _keyword.Add(TokenType.AsmKeyword);
            _keyword.Add(TokenType.BeginKeyword);
            _keyword.Add(TokenType.CaseKeyword);
            _keyword.Add(TokenType.ClassKeyword);
            _keyword.Add(TokenType.ConstKeyword);
            _keyword.Add(TokenType.ConstructorKeyword);
            _keyword.Add(TokenType.DestructorKeyword);
            _keyword.Add(TokenType.DispInterfaceKeyword);
            _keyword.Add(TokenType.DivKeyword);
            _keyword.Add(TokenType.DoKeyword);
            _keyword.Add(TokenType.DownToKeyword);
            _keyword.Add(TokenType.ElseKeyword);
            _keyword.Add(TokenType.EndKeyword);
            _keyword.Add(TokenType.ExceptKeyword);
            _keyword.Add(TokenType.ExportsKeyword);
            _keyword.Add(TokenType.FileKeyword);
            _keyword.Add(TokenType.FinalizationKeyword);
            _keyword.Add(TokenType.FinallyKeyword);
            _keyword.Add(TokenType.ForKeyword);
            _keyword.Add(TokenType.FunctionKeyword);
            _keyword.Add(TokenType.GotoKeyword);
            _keyword.Add(TokenType.IfKeyword);
            _keyword.Add(TokenType.ImplementationKeyword);
            _keyword.Add(TokenType.InKeyword);
            _keyword.Add(TokenType.InheritedKeyword);
            _keyword.Add(TokenType.InitializationKeyword);
            _keyword.Add(TokenType.InlineKeyword);
            _keyword.Add(TokenType.InterfaceKeyword);
            _keyword.Add(TokenType.IsKeyword);
            _keyword.Add(TokenType.LabelKeyword);
            _keyword.Add(TokenType.LibraryKeyword);
            _keyword.Add(TokenType.ModKeyword);
            _keyword.Add(TokenType.NilKeyword);
            _keyword.Add(TokenType.NotKeyword);
            _keyword.Add(TokenType.ObjectKeyword);
            _keyword.Add(TokenType.OfKeyword);
            _keyword.Add(TokenType.OrKeyword);
            _keyword.Add(TokenType.PackedKeyword);
            _keyword.Add(TokenType.ProcedureKeyword);
            _keyword.Add(TokenType.ProgramKeyword);
            _keyword.Add(TokenType.PropertyKeyword);
            _keyword.Add(TokenType.RaiseKeyword);
            _keyword.Add(TokenType.RecordKeyword);
            _keyword.Add(TokenType.RepeatKeyword);
            _keyword.Add(TokenType.ResourceStringKeyword);
            _keyword.Add(TokenType.SetKeyword);
            _keyword.Add(TokenType.ShlKeyword);
            _keyword.Add(TokenType.ShrKeyword);
            _keyword.Add(TokenType.StringKeyword);
            _keyword.Add(TokenType.ThenKeyword);
            _keyword.Add(TokenType.ThreadVarKeyword);
            _keyword.Add(TokenType.ToKeyword);
            _keyword.Add(TokenType.TryKeyword);
            _keyword.Add(TokenType.TypeKeyword);
            _keyword.Add(TokenType.UnitKeyword);
            _keyword.Add(TokenType.UntilKeyword);
            _keyword.Add(TokenType.UsesKeyword);
            _keyword.Add(TokenType.VarKeyword);
            _keyword.Add(TokenType.WhileKeyword);
            _keyword.Add(TokenType.WithKeyword);
            _keyword.Add(TokenType.XorKeyword);

            _methodType = new TokenSet("method heading");
            _methodType.Add(TokenType.ConstructorKeyword);
            _methodType.Add(TokenType.DestructorKeyword);
            _methodType.Add(TokenType.FunctionKeyword);
            _methodType.Add(TokenType.ProcedureKeyword);
            _methodType.Add(TokenType.OperatorSemikeyword);

            _mulOp = new TokenSet("multiplication operator");
            _mulOp.Add(TokenType.TimesSign);
            _mulOp.Add(TokenType.DivideBySign);
            _mulOp.Add(TokenType.DivKeyword);
            _mulOp.Add(TokenType.AndKeyword);
            _mulOp.Add(TokenType.ModKeyword);
            _mulOp.Add(TokenType.ShlKeyword);
            _mulOp.Add(TokenType.ShrKeyword);

            _parameterizedPropertyDirective = new TokenSet("property directive");
            _parameterizedPropertyDirective.Add(TokenType.DefaultSemikeyword);
            _parameterizedPropertyDirective.Add(TokenType.DispIdSemikeyword);
            _parameterizedPropertyDirective.Add(TokenType.ImplementsSemikeyword);
            _parameterizedPropertyDirective.Add(TokenType.IndexSemikeyword);
            _parameterizedPropertyDirective.Add(TokenType.ReadSemikeyword);
            _parameterizedPropertyDirective.Add(TokenType.StoredSemikeyword);
            _parameterizedPropertyDirective.Add(TokenType.WriteSemikeyword);

            _parameterlessPropertyDirective = new TokenSet("property directive");
            _parameterlessPropertyDirective.Add(TokenType.NoDefaultSemikeyword);
            _parameterlessPropertyDirective.Add(TokenType.ReadOnlySemikeyword);
            _parameterlessPropertyDirective.Add(TokenType.WriteOnlySemikeyword);

            _parameterModifier = new TokenSet("parameter modifier");
            _parameterModifier.Add(TokenType.ConstKeyword);
            _parameterModifier.Add(TokenType.OutSemikeyword);
            _parameterModifier.Add(TokenType.VarKeyword);

            _portabilityDirective = new TokenSet("portability directive");
            _portabilityDirective.Add(TokenType.PlatformSemikeyword);
            _portabilityDirective.Add(TokenType.DeprecatedSemikeyword);
            _portabilityDirective.Add(TokenType.LibraryKeyword);
            _portabilityDirective.Add(TokenType.ExperimentalSemikeyword);

            _program = new TokenSet("program");
            _program.Add(TokenType.ProgramKeyword);
            _program.Add(TokenType.LibraryKeyword);

            _relOp = new TokenSet("relational operator");
            _relOp.Add(TokenType.EqualSign);
            _relOp.Add(TokenType.GreaterThan);
            _relOp.Add(TokenType.LessThan);
            _relOp.Add(TokenType.LessOrEqual);
            _relOp.Add(TokenType.GreaterOrEqual);
            _relOp.Add(TokenType.NotEqual);
            _relOp.Add(TokenType.InKeyword);
            _relOp.Add(TokenType.IsKeyword);
            _relOp.Add(TokenType.AsKeyword);

            _semikeyword = new TokenSet("semikeyword");
            _semikeyword.Add(TokenType.AbsoluteSemikeyword);
            _semikeyword.Add(TokenType.AbstractSemikeyword);
            _semikeyword.Add(TokenType.AssemblerSemikeyword);
            _semikeyword.Add(TokenType.AssemblySemikeyword);
            _semikeyword.Add(TokenType.AtSemikeyword);
            _semikeyword.Add(TokenType.AutomatedSemikeyword);
            _semikeyword.Add(TokenType.CdeclSemikeyword);
            _semikeyword.Add(TokenType.ContainsSemikeyword);
            _semikeyword.Add(TokenType.DefaultSemikeyword);
            _semikeyword.Add(TokenType.DeprecatedSemikeyword);
            _semikeyword.Add(TokenType.DispIdSemikeyword);
            _semikeyword.Add(TokenType.DynamicSemikeyword);
            _semikeyword.Add(TokenType.ExperimentalSemikeyword);
            _semikeyword.Add(TokenType.ExportSemikeyword);
            _semikeyword.Add(TokenType.ExternalSemikeyword);
            _semikeyword.Add(TokenType.FarSemikeyword);
            _semikeyword.Add(TokenType.FinalSemikeyword);
            _semikeyword.Add(TokenType.ForwardSemikeyword);
            _semikeyword.Add(TokenType.HelperSemikeyword);
            _semikeyword.Add(TokenType.ImplementsSemikeyword);
            _semikeyword.Add(TokenType.IndexSemikeyword);
            _semikeyword.Add(TokenType.LocalSemikeyword);
            _semikeyword.Add(TokenType.MessageSemikeyword);
            _semikeyword.Add(TokenType.NameSemikeyword);
            _semikeyword.Add(TokenType.NearSemikeyword);
            _semikeyword.Add(TokenType.NoDefaultSemikeyword);
            _semikeyword.Add(TokenType.OnSemikeyword);
            _semikeyword.Add(TokenType.OperatorSemikeyword);
            _semikeyword.Add(TokenType.OutSemikeyword);
            _semikeyword.Add(TokenType.OverloadSemikeyword);
            _semikeyword.Add(TokenType.OverrideSemikeyword);
            _semikeyword.Add(TokenType.PackageSemikeyword);
            _semikeyword.Add(TokenType.PascalSemikeyword);
            _semikeyword.Add(TokenType.PlatformSemikeyword);
            _semikeyword.Add(TokenType.PrivateSemikeyword);
            _semikeyword.Add(TokenType.ProtectedSemikeyword);
            _semikeyword.Add(TokenType.PublicSemikeyword);
            _semikeyword.Add(TokenType.PublishedSemikeyword);
            _semikeyword.Add(TokenType.ReadSemikeyword);
            _semikeyword.Add(TokenType.ReadOnlySemikeyword);
            _semikeyword.Add(TokenType.RegisterSemikeyword);
            _semikeyword.Add(TokenType.ReintroduceSemikeyword);
            _semikeyword.Add(TokenType.RequiresSemikeyword);
            _semikeyword.Add(TokenType.ResidentSemikeyword);
            _semikeyword.Add(TokenType.SafecallSemikeyword);
            _semikeyword.Add(TokenType.SealedSemikeyword);
            _semikeyword.Add(TokenType.StaticSemikeyword);
            _semikeyword.Add(TokenType.StdcallSemikeyword);
            _semikeyword.Add(TokenType.StoredSemikeyword);
            _semikeyword.Add(TokenType.StrictSemikeyword);
            _semikeyword.Add(TokenType.UnsafeSemikeyword);
            _semikeyword.Add(TokenType.VarArgsSemikeyword);
            _semikeyword.Add(TokenType.VirtualSemikeyword);
            _semikeyword.Add(TokenType.WriteSemikeyword);
            _semikeyword.Add(TokenType.WriteOnlySemikeyword);

            _unaryOperator = new TokenSet("unary operator");
            _unaryOperator.Add(TokenType.AtSign);
            _unaryOperator.Add(TokenType.InheritedKeyword);
            _unaryOperator.Add(TokenType.MinusSign);
            _unaryOperator.Add(TokenType.NotKeyword);
            _unaryOperator.Add(TokenType.PlusSign);

            _uses = new TokenSet("uses clause");
            _uses.Add(TokenType.UsesKeyword);
            _uses.Add(TokenType.ContainsSemikeyword);

            _varHeader = new TokenSet("var section");
            _varHeader.Add(TokenType.VarKeyword);
            _varHeader.Add(TokenType.ThreadVarKeyword);

            _visibilitySingleWord = new TokenSet("'private', 'protected', 'public', or 'published'");
            _visibilitySingleWord.Add(TokenType.PrivateSemikeyword);
            _visibilitySingleWord.Add(TokenType.ProtectedSemikeyword);
            _visibilitySingleWord.Add(TokenType.PublicSemikeyword);
            _visibilitySingleWord.Add(TokenType.PublishedSemikeyword);

            _ident = new TokenSet("identifier");
            _ident.Add(TokenType.Identifier);
            _ident.AddRange(_semikeyword);

            _directive.AddRange(_portabilityDirective);
            _particle = new TokenSet("expression");
            _particle.Add(TokenType.FileKeyword);
            _particle.Add(TokenType.NilKeyword);
            _particle.Add(TokenType.Number);
            _particle.Add(TokenType.OpenBracket);
            _particle.Add(TokenType.OpenParenthesis);
            _particle.Add(TokenType.StringKeyword);
            _particle.Add(TokenType.StringLiteral);
            _particle.AddRange(_ident);
            _expression = new TokenSet("expression");
            _expression.AddRange(_particle);
            _expression.AddRange(_unaryOperator);
            _extendedIdent = new TokenSet("identifier (including keyword)");
            _extendedIdent.AddRange(_ident);
            _extendedIdent.AddRange(_keyword);
            _labelId = new TokenSet("label");
            _labelId.Add(TokenType.Number);
            _labelId.AddRange(_ident);
            _parameter = new TokenSet("parameter");
            _parameter.AddRange(_ident);
            _parameter.AddRange(_parameterModifier);
            _simpleParameterType = new TokenSet("parameter type");
            _simpleParameterType.Add(TokenType.FileKeyword);
            _simpleParameterType.Add(TokenType.StringKeyword);
            _simpleParameterType.AddRange(_ident);
            _visibility = new TokenSet("visibility specifier");
            _visibility.Add(TokenType.StrictSemikeyword);
            _visibility.AddRange(_visibilitySingleWord);
        }

        public static TokenSet AddOp
        {
            get { return _addOp; }
        }
        public static TokenSet Block
        {
            get { return _block; }
        }
        public static TokenSet ClassDisposition
        {
            get { return _classDisposition; }
        }
        public static TokenSet ConstHeader
        {
            get { return _constHeader; }
        }
        public static TokenSet Directive
        {
            get { return _directive; }
        }
        public static TokenSet ExportsSpecifier
        {
            get { return _exportsSpecifier; }
        }
        public static TokenSet Expression
        {
            get { return _expression; }
        }
        public static TokenSet ExtendedIdent
        {
            get { return _extendedIdent; }
        }
        public static TokenSet ForDirection
        {
            get { return _forDirection; }
        }
        public static TokenSet ForwardableType
        {
            get { return _forwardableType; }
        }
        public static TokenSet Ident
        {
            get { return _ident; }
        }
        public static TokenSet InitSection
        {
            get { return _initSection; }
        }
        public static TokenSet InterfaceType
        {
            get { return _interfaceType; }
        }
        public static TokenSet Keyword
        {
            get { return _keyword; }
        }
        public static TokenSet LabelId
        {
            get { return _labelId; }
        }
        public static TokenSet MethodType
        {
            get { return _methodType; }
        }
        public static TokenSet MulOp
        {
            get { return _mulOp; }
        }
        public static TokenSet Parameter
        {
            get { return _parameter; }
        }
        public static TokenSet ParameterizedPropertyDirective
        {
            get { return _parameterizedPropertyDirective; }
        }
        public static TokenSet ParameterlessPropertyDirective
        {
            get { return _parameterlessPropertyDirective; }
        }
        public static TokenSet ParameterModifier
        {
            get { return _parameterModifier; }
        }
        public static TokenSet Particle
        {
            get { return _particle; }
        }
        public static TokenSet PortabilityDirective
        {
            get { return _portabilityDirective; }
        }
        public static TokenSet Program
        {
            get { return _program; }
        }
        public static TokenSet RelOp
        {
            get { return _relOp; }
        }
        public static TokenSet Semikeyword
        {
            get { return _semikeyword; }
        }
        public static TokenSet SimpleParameterType
        {
            get { return _simpleParameterType; }
        }
        public static TokenSet UnaryOperator
        {
            get { return _unaryOperator; }
        }
        public static TokenSet Uses
        {
            get { return _uses; }
        }
        public static TokenSet VarHeader
        {
            get { return _varHeader; }
        }
        public static TokenSet Visibility
        {
            get { return _visibility; }
        }
        public static TokenSet VisibilitySingleWord
        {
            get { return _visibilitySingleWord; }
        }
    }
}
