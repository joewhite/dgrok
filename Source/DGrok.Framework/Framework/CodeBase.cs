// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DGrok.DelphiNodes;

namespace DGrok.Framework
{
    public class CodeBase
    {
        private CompilerDefines _compilerDefines;
        private Dictionary<string, Exception> _errors;
        private IFileLoader _fileLoader;
        private Dictionary<string, AstNode> _parsedFiles;
        private Dictionary<string, UnitNode> _units;

        public CodeBase(CompilerDefines compilerDefines, IFileLoader fileLoader)
        {
            _compilerDefines = compilerDefines;
            _fileLoader = fileLoader;
            _errors = new Dictionary<string, Exception>(StringComparer.InvariantCultureIgnoreCase);
            _parsedFiles = new Dictionary<string, AstNode>(StringComparer.InvariantCultureIgnoreCase);
            _units = new Dictionary<string, UnitNode>(StringComparer.InvariantCultureIgnoreCase);
        }

        public IEnumerable<KeyValuePair<string, Exception>> Errors
        {
            get { return SortedPairs(_errors); }
        }
        public int ParsedFileCount
        {
            get { return _parsedFiles.Count; }
        }
        public IList<string> ParsedFileNames
        {
            get { return SortedKeys(_parsedFiles); }
        }
        public IEnumerable<KeyValuePair<string, AstNode>> ParsedFiles
        {
            get { return SortedPairs(_parsedFiles); }
        }
        public IEnumerable<KeyValuePair<string, UnitNode>> UnitsByName
        {
            get { return SortedPairs(_units); }
        }

        public void AddError(string fileName, Exception exception)
        {
            _errors.Add(fileName, exception);
        }
        public void AddFile(string fileName, string text)
        {
            try
            {
                AddFileExpectingSuccess(fileName, text);
            }
            catch (IOException ex)
            {
                AddError(fileName, ex);
            }
            catch (DGrokException ex)
            {
                AddError(fileName, ex);
            }
        }
        public void AddFileExpectingSuccess(string fileName, string text)
        {
            Parser parser = Parser.FromText(text, fileName, _compilerDefines, _fileLoader);
            AstNode parseTree = parser.ParseRule(RuleType.Goal);
            AddParsedFile(fileName, parseTree);
        }
        public void AddParsedFile(string fileName, AstNode parseTree)
        {
            _parsedFiles.Add(fileName, parseTree);
            UnitNode unit = parseTree as UnitNode;
            if (unit != null)
                _units.Add(Path.GetFileNameWithoutExtension(fileName), unit);
        }
        public Exception ErrorByFileName(string fileName)
        {
            return _errors[fileName];
        }
        public AstNode ParsedFileByFileName(string fileName)
        {
            return _parsedFiles[fileName];
        }
        private IList<string> SortedKeys<T>(IDictionary<string, T> dictionary)
        {
            List<string> keys = new List<string>(dictionary.Keys);
            keys.Sort(StringComparer.InvariantCultureIgnoreCase);
            return keys;
        }
        private IEnumerable<KeyValuePair<string, T>> SortedPairs<T>(IDictionary<string, T> dictionary)
        {
            foreach (string key in SortedKeys(dictionary))
                yield return new KeyValuePair<string, T>(key, dictionary[key]);
        }
        public UnitNode UnitByName(string name)
        {
            return _units[name];
        }
    }
}
