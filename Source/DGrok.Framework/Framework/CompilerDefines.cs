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
    public class CompilerDefines
    {
        private Dictionary<string, bool> _dictionary;

        private CompilerDefines()
        {
            _dictionary = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static CompilerDefines CreateEmpty()
        {
            return new CompilerDefines();
        }
        public static CompilerDefines CreateStandard()
        {
            CompilerDefines defines = CreateEmpty();

            defines.DefineSymbol("MSWINDOWS");
            defines.DefineDirectiveAsTrue("IF Defined(MSWindows)");
            defines.DefineDirectiveAsTrue("IF DEFINED(CPU386)");
            defines.DefineDirectiveAsTrue("IF not GenericSafeArrays");
            defines.DefineDirectiveAsTrue("IF not GenericVariants");

            defines.UndefineSymbol("DEBUG");
            defines.UndefineSymbol("DEBUG_FUNCTIONS");
            defines.UndefineSymbol("DEBUG_MREWS");
            defines.UndefineSymbol("DECLARE_GPL");
            defines.UndefineSymbol("ELF");
            defines.UndefineSymbol("GLOBALALLOC");
            defines.UndefineSymbol("LINUX");
            defines.UndefineSymbol("MEMORY_DIAG");
            defines.UndefineSymbol("PC_MAPPED_EXCEPTIONS");
            defines.UndefineSymbol(" PC_MAPPED_EXCEPTIONS");
            defines.UndefineSymbol("PIC");
            defines.UndefineSymbol("PUREPASCAL");
            defines.UndefineSymbol("RANGECHECKINGOFF");
            defines.UndefineSymbol("TRIAL_EDITION");
            defines.DefineDirectiveAsFalse("IF Defined(Linux)");
            defines.DefineDirectiveAsFalse("IF Defined(PIC) Or Defined(PUREPASCAL)");
            defines.DefineDirectiveAsFalse("IF GenericOperations");
            defines.DefineDirectiveAsFalse("IF GenericVariants");
            defines.DefineDirectiveAsFalse("IFOPT R-");

            return defines;
        }
        public void DefineDirectiveAsFalse(string compilerDirective)
        {
            _dictionary[compilerDirective] = false;
        }
        public void DefineDirectiveAsTrue(string compilerDirective)
        {
            _dictionary[compilerDirective] = true;
        }
        public void DefineSymbol(string symbol)
        {
            DefineDirectiveAsTrue("IFDEF " + symbol);
            DefineDirectiveAsFalse("IFNDEF " + symbol);
        }
        public bool IsTrue(string compilerDirective, int offset)
        {
            if (_dictionary.ContainsKey(compilerDirective))
                return _dictionary[compilerDirective];
            throw new PreprocessorException("Compiler directive '" + compilerDirective +
                "' has not been defined as either true or false", offset);
        }
        public void UndefineSymbol(string symbol)
        {
            DefineDirectiveAsTrue("IFNDEF " + symbol);
            DefineDirectiveAsFalse("IFDEF " + symbol);
        }
    }
}
