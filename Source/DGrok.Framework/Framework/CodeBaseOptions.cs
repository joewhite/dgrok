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
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace DGrok.Framework
{
    public class CodeBaseOptions
    {
        public const string DefaultFileMasks = "*.pas;*.dpr;*.dpk;*.pp";

        private CompilerOptions _compilerOptions = new CompilerOptions();
        private string _customDefines = "";
        private string _delphiVersionDefine = "";
        private string _falseIfConditions = "";
        private string _fileMasks = DefaultFileMasks;
        private int _parserThreadCount = 1;
        private string _searchPaths = "";
        private string _trueIfConditions = "";

        public string CompilerOptionsSetOff
        {
            get { return _compilerOptions.OptionsSetOff; }
            set { _compilerOptions.OptionsSetOff = value; }
        }
        public string CompilerOptionsSetOn
        {
            get { return _compilerOptions.OptionsSetOn; }
            set { _compilerOptions.OptionsSetOn = value; }
        }
        public string CustomDefines
        {
            get { return _customDefines; }
            set { _customDefines = value; }
        }
        public string DelphiVersionDefine
        {
            get { return _delphiVersionDefine; }
            set { _delphiVersionDefine = value; }
        }
        public string FalseIfConditions
        {
            get { return _falseIfConditions; }
            set { _falseIfConditions = value; }
        }
        public string FileMasks
        {
            get { return _fileMasks; }
            set { _fileMasks = value; }
        }
        public int ParserThreadCount
        {
            get { return _parserThreadCount; }
            set { _parserThreadCount = value; }
        }
        public string SearchPaths
        {
            get { return _searchPaths; }
            set { _searchPaths = value; }
        }
        public string TrueIfConditions
        {
            get { return _trueIfConditions; }
            set { _trueIfConditions = value; }
        }

        public CodeBaseOptions Clone()
        {
            CodeBaseOptions result = new CodeBaseOptions();
            result.CompilerOptionsSetOff = CompilerOptionsSetOff;
            result.CompilerOptionsSetOn = CompilerOptionsSetOn;
            result.CustomDefines = CustomDefines;
            result.DelphiVersionDefine = DelphiVersionDefine;
            result.FalseIfConditions = FalseIfConditions;
            result.FileMasks = FileMasks;
            result.SearchPaths = SearchPaths;
            result.TrueIfConditions = TrueIfConditions;
            return result;
        }
        public CompilerDefines CreateCompilerDefines()
        {
            CompilerDefines result = CompilerDefines.CreateStandard();
            for (char option = 'A'; option <= 'Z'; ++option)
            {
                result.DefineDirective("IFOPT " + option + "-", _compilerOptions.IsOptionOff(option));
                result.DefineDirective("IFOPT " + option + "+", _compilerOptions.IsOptionOn(option));
            }
            foreach (string define in CustomDefines.Split(';'))
                result.DefineSymbol(define);
            result.DefineSymbol(DelphiVersionDefine);
            foreach (string condition in FalseIfConditions.Split(';'))
                result.DefineDirectiveAsFalse(condition);
            foreach (string condition in TrueIfConditions.Split(';'))
                result.DefineDirectiveAsTrue(condition);
            return result;
        }
        public List<string> ListFiles()
        {
            List<string> result = new List<string>();
            foreach (string searchPath in SearchPaths.Split(';'))
            {
                string directory;
                SearchOption option;
                if (Path.GetFileName(searchPath) == "**")
                {
                    directory = Path.GetDirectoryName(searchPath);
                    option = SearchOption.AllDirectories;
                }
                else
                {
                    directory = searchPath;
                    option = SearchOption.TopDirectoryOnly;
                }
                foreach (string fileMask in FileMasks.Split(';'))
                {
                    foreach (string fileName in Directory.GetFiles(directory, fileMask, option))
                    {
                        string extension = Path.GetExtension(fileName);
                        if (!String.Equals(extension, ".dproj", StringComparison.InvariantCultureIgnoreCase))
                            result.Add(fileName);
                    }
                }
            }
            return result;
        }
        public void LoadFromRegistry()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"DGrok\Demo"))
            {
                if (key == null)
                    return;
                CompilerOptionsSetOff = key.GetValue("CompilerOptionsSetOff", "").ToString();
                CompilerOptionsSetOn = key.GetValue("CompilerOptionsSetOn", "").ToString();
                CustomDefines = key.GetValue("CustomDefines", "").ToString();
                DelphiVersionDefine = key.GetValue("DelphiVersionDefine", "").ToString();
                FalseIfConditions = key.GetValue("FalseIfConditions", "").ToString();
                FileMasks = key.GetValue("FileMasks", DefaultFileMasks).ToString();
                ParserThreadCount = (int) key.GetValue("ParserThreadCount", Environment.ProcessorCount);
                SearchPaths = key.GetValue("SearchPaths", "").ToString();
                TrueIfConditions = key.GetValue("TrueIfConditions", "").ToString();
            }
        }
        public void SaveToRegistry()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"DGrok\Demo"))
            {
                key.SetValue("CompilerOptionsSetOff", CompilerOptionsSetOff);
                key.SetValue("CompilerOptionsSetOn", CompilerOptionsSetOn);
                key.SetValue("CustomDefines", CustomDefines);
                key.SetValue("DelphiVersionDefine", DelphiVersionDefine);
                key.SetValue("FalseIfConditions", FalseIfConditions);
                key.SetValue("FileMasks", FileMasks);
                key.SetValue("ParserThreadCount", ParserThreadCount);
                key.SetValue("SearchPaths", SearchPaths);
                key.SetValue("TrueIfConditions", TrueIfConditions);
            }
        }
    }
}
