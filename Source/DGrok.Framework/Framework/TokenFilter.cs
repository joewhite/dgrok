// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DGrok.Framework
{
    public class TokenFilter
    {
        private enum DirectiveType
        {
            Ignored,
            PossibleInclude,
            Include,
            Define,
            Undefine,
            If,
            Else,
            ElseIf,
            EndIf,
        }
        private enum IfDefTruth
        {
            ForeverFalse,
            InitiallyFalse,
            True,
        }

        private CompilerDefines _compilerDefines;
        private IDictionary<string, DirectiveType> _directiveTypes;
        private IFileLoader _fileLoader;
        private static Regex _findFirstWordRegex = new Regex("^[A-Za-z]+", RegexOptions.Compiled);
        private IEnumerable<Token> _tokens;

        public TokenFilter(IEnumerable<Token> tokens, CompilerDefines compilerDefines,
            IFileLoader fileLoader)
        {
            _tokens = tokens;
            _compilerDefines = compilerDefines.Clone();
            _fileLoader = fileLoader;
            _directiveTypes =
                new Dictionary<string, DirectiveType>(StringComparer.InvariantCultureIgnoreCase);
            // Conditional-compilation directives
            _directiveTypes["IF"] = DirectiveType.If;
            _directiveTypes["IFDEF"] = DirectiveType.If;
            _directiveTypes["IFNDEF"] = DirectiveType.If;
            _directiveTypes["IFOPT"] = DirectiveType.If;
            _directiveTypes["IFNOPT"] = DirectiveType.If;
            _directiveTypes["ELSE"] = DirectiveType.Else;
            _directiveTypes["ELSEIF"] = DirectiveType.ElseIf;
            _directiveTypes["ENDIF"] = DirectiveType.EndIf;
            _directiveTypes["IFEND"] = DirectiveType.EndIf;
            // Delphi compiler directives
            _directiveTypes["ALIGN"] = DirectiveType.Ignored;
            _directiveTypes["BOOLEVAL"] = DirectiveType.Ignored;
            _directiveTypes["DEFINE"] = DirectiveType.Define;
            _directiveTypes["DENYPACKAGEUNIT"] = DirectiveType.Ignored;
            _directiveTypes["I"] = DirectiveType.PossibleInclude;
            _directiveTypes["IMAGEBASE"] = DirectiveType.Ignored;
            _directiveTypes["INCLUDE"] = DirectiveType.Include;
            _directiveTypes["LONGSTRINGS"] = DirectiveType.Ignored;
            _directiveTypes["MESSAGE"] = DirectiveType.Ignored;
            _directiveTypes["MINENUMSIZE"] = DirectiveType.Ignored;
            _directiveTypes["OPTIMIZATION"] = DirectiveType.Ignored;
            _directiveTypes["RANGECHECKS"] = DirectiveType.Ignored;
            _directiveTypes["STACKFRAMES"] = DirectiveType.Ignored;
            _directiveTypes["TYPEDADDRESS"] = DirectiveType.Ignored;
            _directiveTypes["UNDEF"] = DirectiveType.Undefine;
            _directiveTypes["VARPROPSETTER"] = DirectiveType.Ignored;
            _directiveTypes["WARN"] = DirectiveType.Ignored;
            _directiveTypes["WEAKPACKAGEUNIT"] = DirectiveType.Ignored;
            _directiveTypes["WRITEABLECONST"] = DirectiveType.Ignored;
            // Directives for generation of C++Builder .hpp files
            _directiveTypes["EXTERNALSYM"] = DirectiveType.Ignored;
            _directiveTypes["HPPEMIT"] = DirectiveType.Ignored;
            _directiveTypes["NODEFINE"] = DirectiveType.Ignored;
            _directiveTypes["NOINCLUDE"] = DirectiveType.Ignored;
        }

        public IEnumerable<Token> Tokens
        {
            get
            {
                Stack<IfDefTruth> ifDefStack = new Stack<IfDefTruth>();
                ifDefStack.Push(IfDefTruth.True);
                return Filter(ifDefStack, _tokens);
            }
        }

        private IEnumerable<Token> Filter(Stack<IfDefTruth> ifDefStack, IEnumerable<Token> sourceTokens)
        {
            foreach (Token token in sourceTokens)
            {
                switch (token.Type)
                {
                    case TokenType.SingleLineComment:
                    case TokenType.CurlyBraceComment:
                    case TokenType.ParenStarComment:
                        break;

                    case TokenType.CompilerDirective:
                        foreach (Token newToken in HandleCompilerDirective(ifDefStack, token))
                            yield return newToken;
                        break;

                    default:
                        if (ifDefStack.Peek() == IfDefTruth.True)
                            yield return token;
                        break;
                }
            }
        }
        private string FirstWordOf(string s)
        {
            Match match = _findFirstWordRegex.Match(s);
            return match.Value;
        }
        private DirectiveType GetDirectiveType(string firstWord, Location location)
        {
            if (_directiveTypes.ContainsKey(firstWord))
                return _directiveTypes[firstWord];
            if (firstWord.Length == 1)
                return DirectiveType.Ignored;
            throw new LexException("Unrecognized compiler directive '" + firstWord + "'", location);
        }
        private IEnumerable<Token> GetSourceTokensForInclude(Token token, string baseName)
        {
            string currentDirectory = token.Location.Directory;
            string fileName = _fileLoader.ExpandFileName(currentDirectory, baseName);
            string source = _fileLoader.Load(fileName);
            Lexer lexer = new Lexer(source, fileName);
            return lexer.Tokens;
        }
        private IEnumerable<Token> HandleCompilerDirective(Stack<IfDefTruth> ifDefStack, Token token)
        {
            string directive = token.ParsedText;
            string firstWord = FirstWordOf(directive);
            string parameter = directive.Substring(firstWord.Length).Trim();
            switch (GetDirectiveType(firstWord, token.Location))
            {
                case DirectiveType.Ignored:
                    break;

                case DirectiveType.PossibleInclude:
                    if (parameter.StartsWith("-") || parameter.StartsWith("+"))
                    {
                        // $I = $IOCHECKS. Ignore it.
                        break;
                    }
                    else
                    {
                        // $I = $INCLUDE. Handle it.
                        goto case DirectiveType.Include;
                    }

                case DirectiveType.Include:
                    IEnumerable<Token> sourceTokens = GetSourceTokensForInclude(token, parameter);
                    IEnumerable<Token> filteredTokens = Filter(ifDefStack, sourceTokens);
                    foreach (Token newToken in filteredTokens)
                        yield return newToken;
                    break;

                case DirectiveType.Define:
                    _compilerDefines.DefineSymbol(parameter);
                    break;

                case DirectiveType.Undefine:
                    _compilerDefines.UndefineSymbol(parameter);
                    break;

                case DirectiveType.If:
                    HandleIf(ifDefStack, token, directive);
                    break;

                case DirectiveType.Else:
                    HandleElse(ifDefStack);
                    break;

                case DirectiveType.ElseIf:
                    HandleElseIf(ifDefStack, token, directive);
                    break;

                case DirectiveType.EndIf:
                    ifDefStack.Pop();
                    break;
            }
        }
        private void HandleElse(Stack<IfDefTruth> ifDefStack)
        {
            IfDefTruth truth = ifDefStack.Pop();
            if (truth == IfDefTruth.InitiallyFalse)
                ifDefStack.Push(IfDefTruth.True);
            else
                ifDefStack.Push(IfDefTruth.ForeverFalse);
        }
        private void HandleElseIf(Stack<IfDefTruth> ifDefStack, Token token, string directive)
        {
            IfDefTruth truth = ifDefStack.Pop();
            if (truth == IfDefTruth.True || truth == IfDefTruth.ForeverFalse)
                ifDefStack.Push(IfDefTruth.ForeverFalse);
            else
            {
                string trimmedDirective = directive.Substring(4);
                if (_compilerDefines.IsTrue(trimmedDirective, token.Location))
                    ifDefStack.Push(IfDefTruth.True);
                else
                    ifDefStack.Push(IfDefTruth.InitiallyFalse);
            }
        }
        private void HandleIf(Stack<IfDefTruth> ifDefStack, Token token, string directive)
        {
            if (ifDefStack.Peek() == IfDefTruth.True)
            {
                if (_compilerDefines.IsTrue(directive, token.Location))
                    ifDefStack.Push(IfDefTruth.True);
                else
                    ifDefStack.Push(IfDefTruth.InitiallyFalse);
            }
            else
                ifDefStack.Push(IfDefTruth.ForeverFalse);
        }
    }
}
