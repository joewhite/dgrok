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
    public class CompilerDefines
    {
        private Dictionary<string, bool> _dictionary;

        private CompilerDefines()
        {
            _dictionary = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
        }

        public CompilerDefines Clone()
        {
            CompilerDefines clone = CompilerDefines.CreateEmpty();
            foreach (KeyValuePair<string, bool> pair in _dictionary)
                clone.DefineDirective(pair.Key, pair.Value);
            return clone;
        }
        public static CompilerDefines CreateEmpty()
        {
            return new CompilerDefines();
        }
        public static CompilerDefines CreateStandard()
        {
            CompilerDefines defines = CreateEmpty();

            // These lists come from the following sources:
            //  - Actual IFDEFs in the Delphi RTL source
            //  - http://www.blong.com/Tips/KylixTips.htm#Conditionals

            defines.DefineSymbol("CONDITIONALEXPRESSIONS");
            defines.DefineSymbol("CPU386");
            defines.DefineSymbol("MSWINDOWS");
            defines.DefineSymbol("WIN32");
            defines.DefineDirectiveAsTrue("IF not GenericSafeArrays");
            defines.DefineDirectiveAsTrue("IF not GenericVariants");

            // Delphi 1 for Win16
            defines.UndefineSymbol("WINDOWS");
            // C++Builder
            defines.UndefineSymbol("BCB");
            // Kylix
            defines.UndefineSymbol("DECLARE_GPL");
            defines.UndefineSymbol("ELF");
            defines.UndefineSymbol("LINUX");
            defines.UndefineSymbol("LINUX32");
            defines.UndefineSymbol("PC_MAPPED_EXCEPTIONS");
            defines.UndefineSymbol(" PC_MAPPED_EXCEPTIONS");
            defines.UndefineSymbol("PIC");
            defines.UndefineSymbol("POSIX");
            defines.UndefineSymbol("POSIX32");
            // Delphi for .NET
            defines.UndefineSymbol("CIL");
            defines.UndefineSymbol("CLR");
            defines.UndefineSymbol("MANAGEDCODE");
            // Miscellaneous
            defines.UndefineSymbol("DEBUG");
            defines.UndefineSymbol("DEBUG_FUNCTIONS");
            defines.UndefineSymbol("DEBUG_MREWS");
            defines.UndefineSymbol("GLOBALALLOC");
            defines.UndefineSymbol("MEMORY_DIAG");
            defines.UndefineSymbol("PUREPASCAL");
            defines.UndefineSymbol("TRIAL_EDITION");
            defines.UndefineSymbol("USEPASCALCODE");
            defines.DefineDirectiveAsFalse("IF Defined(PIC) Or Defined(PUREPASCAL)");
            defines.DefineDirectiveAsFalse("IF GenericOperations");
            defines.DefineDirectiveAsFalse("IF GenericVariants");
            defines.DefineDirectiveAsFalse("IFOPT R-");

            // Version tags. Not surprisingly, you should DefineSymbol() only one.
            defines.UndefineSymbol("VER80");  // Delphi 1
            defines.UndefineSymbol("VER90");  // Delphi 2
            defines.UndefineSymbol("VER93");  // C++Builder 1
            defines.UndefineSymbol("VER100"); // Delphi 3
            defines.UndefineSymbol("VER110"); // C++Builder 3
            defines.UndefineSymbol("VER120"); // Delphi 4
            defines.UndefineSymbol("VER125"); // C++Builder 4
            defines.UndefineSymbol("VER130"); // Delphi/C++Builder 5
            defines.UndefineSymbol("VER140"); // Delphi/C++Builder 6, Kylix 1, 2, & 3
            defines.UndefineSymbol("VER150"); // Delphi 7
            defines.UndefineSymbol("VER160"); // Delphi 8 for .NET
            defines.UndefineSymbol("VER170"); // Delphi 2005
            defines.UndefineSymbol("VER180"); // Delphi 2006
            // Need: VERxxx value for Delphi 2007 for Win32
            // Need: VERxxx value for CodeGear RAD Studio 2007

            return defines;
        }
        public void DefineDirective(string compilerDirective, bool isTrue)
        {
            _dictionary[compilerDirective] = isTrue;
        }
        public void DefineDirectiveAsFalse(string compilerDirective)
        {
            DefineDirective(compilerDirective, false);
        }
        public void DefineDirectiveAsTrue(string compilerDirective)
        {
            DefineDirective(compilerDirective, true);
        }
        public void DefineSymbol(string symbol)
        {
            if (String.IsNullOrEmpty(symbol))
                return;
            DefineDirectiveAsTrue("IFDEF " + symbol);
            DefineDirectiveAsTrue("IF Defined(" + symbol + ")");
            DefineDirectiveAsFalse("IFNDEF " + symbol);
            DefineDirectiveAsFalse("IF not Defined(" + symbol + ")");
        }
        public bool IsTrue(string compilerDirective, Location location)
        {
            if (_dictionary.ContainsKey(compilerDirective))
                return _dictionary[compilerDirective];
            if (compilerDirective.StartsWith("IFDEF ", StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (compilerDirective.StartsWith("IFNDEF ", StringComparison.InvariantCultureIgnoreCase))
                return true;
            throw new PreprocessorException("Compiler directive '" + compilerDirective +
                "' has not been defined as either true or false", location);
        }
        public void UndefineSymbol(string symbol)
        {
            DefineDirectiveAsTrue("IFNDEF " + symbol);
            DefineDirectiveAsTrue("IF not Defined(" + symbol + ")");
            DefineDirectiveAsFalse("IFDEF " + symbol);
            DefineDirectiveAsFalse("IF Defined(" + symbol + ")");
        }
    }
}
