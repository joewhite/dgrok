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
    public class CodeBaseOptions
    {
        private CompilerDefines _compilerDefines;
        private string _fileMasks;
        private string _searchPaths;

        public CodeBaseOptions(string searchPaths, string fileMasks, CompilerDefines compilerDefines)
        {
            _searchPaths = searchPaths;
            _fileMasks = fileMasks;
            _compilerDefines = compilerDefines;
        }

        public CompilerDefines CompilerDefines
        {
            get { return _compilerDefines; }
        }
        public string FileMasks
        {
            get { return _fileMasks; }
        }
        public string SearchPaths
        {
            get { return _searchPaths; }
        }
    }
}
