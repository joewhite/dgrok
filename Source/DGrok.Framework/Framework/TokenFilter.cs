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
        private static Regex _findFirstWordRegex = new Regex("^[A-Za-z]+", RegexOptions.Compiled);
        private IEnumerable<Token> _tokens;

        public TokenFilter(IEnumerable<Token> tokens, CompilerDefines compilerDefines)
        {
            _tokens = tokens;
            _compilerDefines = compilerDefines;
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
            _directiveTypes["DEFINE"] = DirectiveType.Ignored;
            _directiveTypes["DENYPACKAGEUNIT"] = DirectiveType.Ignored;
            _directiveTypes["IMAGEBASE"] = DirectiveType.Ignored;
            _directiveTypes["LONGSTRINGS"] = DirectiveType.Ignored;
            _directiveTypes["MESSAGE"] = DirectiveType.Ignored;
            _directiveTypes["MINENUMSIZE"] = DirectiveType.Ignored;
            _directiveTypes["OPTIMIZATION"] = DirectiveType.Ignored;
            _directiveTypes["RANGECHECKS"] = DirectiveType.Ignored;
            _directiveTypes["STACKFRAMES"] = DirectiveType.Ignored;
            _directiveTypes["TYPEDADDRESS"] = DirectiveType.Ignored;
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

                foreach (Token token in _tokens)
                {
                    switch (token.Type)
                    {
                        case TokenType.SingleLineComment:
                        case TokenType.CurlyBraceComment:
                        case TokenType.ParenStarComment:
                            break;

                        case TokenType.CompilerDirective:
                            string directive = token.ParsedText;
                            string firstWord = FirstWordOf(directive);
                            IfDefTruth truth;
                            switch (GetDirectiveType(firstWord, token.StartIndex))
                            {
                                case DirectiveType.Ignored:
                                    break;

                                case DirectiveType.If:
                                    if (ifDefStack.Peek() == IfDefTruth.True)
                                    {
                                        if (_compilerDefines.IsTrue(directive, token.StartIndex))
                                            ifDefStack.Push(IfDefTruth.True);
                                        else
                                            ifDefStack.Push(IfDefTruth.InitiallyFalse);
                                    }
                                    else
                                        ifDefStack.Push(IfDefTruth.ForeverFalse);
                                    break;

                                case DirectiveType.Else:
                                    truth = ifDefStack.Pop();
                                    if (truth == IfDefTruth.InitiallyFalse)
                                        ifDefStack.Push(IfDefTruth.True);
                                    else
                                        ifDefStack.Push(IfDefTruth.ForeverFalse);
                                    break;

                                case DirectiveType.ElseIf:
                                    truth = ifDefStack.Pop();
                                    if (truth == IfDefTruth.True || truth == IfDefTruth.ForeverFalse)
                                        ifDefStack.Push(IfDefTruth.ForeverFalse);
                                    else
                                    {
                                        string trimmedDirective = directive.Substring(4);
                                        if (_compilerDefines.IsTrue(trimmedDirective, token.StartIndex))
                                            ifDefStack.Push(IfDefTruth.True);
                                        else
                                            ifDefStack.Push(IfDefTruth.InitiallyFalse);
                                    }
                                    break;

                                case DirectiveType.EndIf:
                                    ifDefStack.Pop();
                                    break;
                            }
                            break;

                        default:
                            if (ifDefStack.Peek() == IfDefTruth.True)
                                yield return token;
                            break;
                    }
                }
            }
        }

        private string FirstWordOf(string s)
        {
            Match match = _findFirstWordRegex.Match(s);
            return match.Value;
        }
        private DirectiveType GetDirectiveType(string firstWord, int offset)
        {
            if (_directiveTypes.ContainsKey(firstWord))
                return _directiveTypes[firstWord];
            if (firstWord.Length == 1)
                return DirectiveType.Ignored;
            throw new LexException("Unrecognized compiler directive '" + firstWord + "'", offset);
        }
    }
}
