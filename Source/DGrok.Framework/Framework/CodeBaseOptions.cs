// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DGrok.Framework
{
    public class CodeBaseOptions
    {
        public const string DefaultFileMasks = "*.pas;*.dpr;*.dpk;*.pp";

        private string _compilerOptionsSetOff = "";
        private string _compilerOptionsSetOn = "";
        private string _customDefines = "";
        private string _delphiVersionDefine = "";
        private string _falseIfConditions = "";
        private string _fileMasks = DefaultFileMasks;
        private string _searchPaths = "";
        private string _trueIfConditions = "";

        public string CompilerOptionsSetOff
        {
            get { return _compilerOptionsSetOff; }
            set { _compilerOptionsSetOff = value; }
        }
        public string CompilerOptionsSetOn
        {
            get { return _compilerOptionsSetOn; }
            set { _compilerOptionsSetOn = value; }
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
            foreach (char option in CompilerOptionsSetOff)
            {
                result.DefineDirectiveAsFalse("IFOPT " + option + "+");
                result.DefineDirectiveAsTrue("IFOPT " + option + "-");
            }
            foreach (char option in CompilerOptionsSetOn)
            {
                result.DefineDirectiveAsTrue("IFOPT " + option + "+");
                result.DefineDirectiveAsFalse("IFOPT " + option + "-");
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
                key.SetValue("SearchPaths", SearchPaths);
                key.SetValue("TrueIfConditions", TrueIfConditions);
            }
        }
    }
}
