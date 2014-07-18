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
using System.Text.RegularExpressions;

namespace DGrok.Framework
{
    public class TokenFilter
    {
        private enum DirectiveType
        {
            Unrecognized,
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
            _directiveTypes["APPTYPE"] = DirectiveType.Ignored;
            _directiveTypes["ASSERTIONS"] = DirectiveType.Ignored;
            _directiveTypes["AUTOBOX"] = DirectiveType.Ignored;
            _directiveTypes["BOOLEVAL"] = DirectiveType.Ignored;
            _directiveTypes["DEBUGINFO"] = DirectiveType.Ignored;
            _directiveTypes["DEFINE"] = DirectiveType.Define;
            _directiveTypes["DEFINITIONINFO"] = DirectiveType.Ignored;
            _directiveTypes["DENYPACKAGEUNIT"] = DirectiveType.Ignored;
            _directiveTypes["DESCRIPTION"] = DirectiveType.Ignored;
            _directiveTypes["DESIGNONLY"] = DirectiveType.Ignored;
            _directiveTypes["ENDREGION"] = DirectiveType.Ignored;
            _directiveTypes["EXTENDEDSYNTAX"] = DirectiveType.Ignored;
            _directiveTypes["EXTENSION"] = DirectiveType.Ignored;
            _directiveTypes["FINITEFLOAT"] = DirectiveType.Ignored;
            _directiveTypes["HINTS"] = DirectiveType.Ignored;
            _directiveTypes["I"] = DirectiveType.PossibleInclude;
            _directiveTypes["IMAGEBASE"] = DirectiveType.Ignored;
            _directiveTypes["IMPLICITBUILD"] = DirectiveType.Ignored;
            _directiveTypes["IMPORTEDDATA"] = DirectiveType.Ignored;
            _directiveTypes["INCLUDE"] = DirectiveType.Include;
            _directiveTypes["INLINE"] = DirectiveType.Ignored; // undocumented
            _directiveTypes["IOCHECKS"] = DirectiveType.Ignored;
            _directiveTypes["LIBPREFIX"] = DirectiveType.Ignored;
            _directiveTypes["LIBSUFFIX"] = DirectiveType.Ignored;
            _directiveTypes["LIBVERSION"] = DirectiveType.Ignored;
            _directiveTypes["LINK"] = DirectiveType.Ignored;
            _directiveTypes["LOCALSYMBOLS"] = DirectiveType.Ignored;
            _directiveTypes["LONGSTRINGS"] = DirectiveType.Ignored;
            _directiveTypes["MAXSTACKSIZE"] = DirectiveType.Ignored;
            _directiveTypes["MESSAGE"] = DirectiveType.Ignored;
            _directiveTypes["METHODINFO"] = DirectiveType.Ignored;
            _directiveTypes["MINENUMSIZE"] = DirectiveType.Ignored;
            _directiveTypes["MINSTACKSIZE"] = DirectiveType.Ignored;
            _directiveTypes["OBJEXPORTALL"] = DirectiveType.Ignored;
            _directiveTypes["OPENSTRINGS"] = DirectiveType.Ignored;
            _directiveTypes["OPTIMIZATION"] = DirectiveType.Ignored;
            _directiveTypes["OVERFLOWCHECKS"] = DirectiveType.Ignored;
            _directiveTypes["RANGECHECKS"] = DirectiveType.Ignored;
            _directiveTypes["REALCOMPATIBILITY"] = DirectiveType.Ignored;
            _directiveTypes["REFERENCEINFO"] = DirectiveType.Ignored;
            _directiveTypes["REGION"] = DirectiveType.Ignored;
            _directiveTypes["RESOURCE"] = DirectiveType.Ignored;
            _directiveTypes["RESOURCERESERVE"] = DirectiveType.Ignored;
            _directiveTypes["RUNONLY"] = DirectiveType.Ignored;
            _directiveTypes["SAFEDIVIDE"] = DirectiveType.Ignored;
            _directiveTypes["SETPEFLAGS"] = DirectiveType.Ignored;
            _directiveTypes["SOPREFIX"] = DirectiveType.Ignored; // undocumented
            _directiveTypes["SOSUFFIX"] = DirectiveType.Ignored; // undocumented
            _directiveTypes["SOVERSION"] = DirectiveType.Ignored; // undocumented
            _directiveTypes["STACKCHECKS"] = DirectiveType.Ignored; // undocumented
            _directiveTypes["STACKFRAMES"] = DirectiveType.Ignored;
            _directiveTypes["TYPEDADDRESS"] = DirectiveType.Ignored;
            _directiveTypes["TYPEINFO"] = DirectiveType.Ignored;
            _directiveTypes["UNDEF"] = DirectiveType.Undefine;
            _directiveTypes["UNSAFECODE"] = DirectiveType.Ignored;
            _directiveTypes["VARPROPSETTER"] = DirectiveType.Ignored; // undocumented
            _directiveTypes["VARSTRINGCHECKS"] = DirectiveType.Ignored;
            _directiveTypes["WARN"] = DirectiveType.Ignored;
            _directiveTypes["WARNINGS"] = DirectiveType.Ignored;
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
        private DirectiveType GetDirectiveType(string firstWord)
        {
            if (_directiveTypes.ContainsKey(firstWord))
                return _directiveTypes[firstWord];
            if (firstWord.Length == 1)
                return DirectiveType.Ignored;
            return DirectiveType.Unrecognized;
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
            switch (GetDirectiveType(firstWord))
            {
                case DirectiveType.Unrecognized:
                    if (ifDefStack.Peek() == IfDefTruth.True)
                    {
                        throw new LexException("Unrecognized compiler directive '" + firstWord + "'",
                            token.Location);
                    }
                    break;

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
                    if (ifDefStack.Peek() == IfDefTruth.True)
                        _compilerDefines.DefineSymbol(parameter);
                    break;

                case DirectiveType.Undefine:
                    if (ifDefStack.Peek() == IfDefTruth.True)
                        _compilerDefines.UndefineSymbol(parameter);
                    break;

                case DirectiveType.If:
                    HandleIf(ifDefStack, directive, token.Location);
                    break;

                case DirectiveType.Else:
                    HandleElse(ifDefStack);
                    break;

                case DirectiveType.ElseIf:
                    HandleElseIf(ifDefStack, directive, token.Location);
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
        private void HandleElseIf(Stack<IfDefTruth> ifDefStack, string directive, Location location)
        {
            IfDefTruth truth = ifDefStack.Pop();
            if (truth == IfDefTruth.True || truth == IfDefTruth.ForeverFalse)
                ifDefStack.Push(IfDefTruth.ForeverFalse);
            else
            {
                string trimmedDirective = directive.Substring(4);
                if (_compilerDefines.IsTrue(trimmedDirective, location))
                    ifDefStack.Push(IfDefTruth.True);
                else
                    ifDefStack.Push(IfDefTruth.InitiallyFalse);
            }
        }
        private void HandleIf(Stack<IfDefTruth> ifDefStack, string directive, Location location)
        {
            if (ifDefStack.Peek() == IfDefTruth.True)
            {
                if (_compilerDefines.IsTrue(directive, location))
                    ifDefStack.Push(IfDefTruth.True);
                else
                    ifDefStack.Push(IfDefTruth.InitiallyFalse);
            }
            else
                ifDefStack.Push(IfDefTruth.ForeverFalse);
        }
    }
}
