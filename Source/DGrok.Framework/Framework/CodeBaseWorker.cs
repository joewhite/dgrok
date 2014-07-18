// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using DGrok.DelphiNodes;

namespace DGrok.Framework
{
    public class CodeBaseWorker
    {
        private BackgroundWorker _backgroundWorker;
        private CodeBase _codeBase;
        private List<string> _fileList = new List<string>();
        private CodeBaseOptions _options;

        private CodeBaseWorker(CodeBase codeBase, CodeBaseOptions options, BackgroundWorker backgroundWorker)
        {
            _codeBase = codeBase;
            _options = options;
            _backgroundWorker = backgroundWorker;
        }

        private void BuildFileList()
        {
            ReportProgress("Building file list...");
            foreach (string searchPath in _options.SearchPaths.Split(';'))
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
                foreach (string fileMask in _options.FileMasks.Split(';'))
                {
                    foreach (string fileName in Directory.GetFiles(directory, fileMask, option))
                    {
                        string extension = Path.GetExtension(fileName);
                        if (!String.Equals(extension, ".dproj", StringComparison.InvariantCultureIgnoreCase))
                            _fileList.Add(fileName);
                    }
                }
            }
        }
        private void CheckForCancel()
        {
            if (_backgroundWorker.CancellationPending)
                throw new CancelException();
        }
        public static CodeBase Execute(CodeBaseOptions options, BackgroundWorker backgroundWorker)
        {
            CodeBase codeBase = new CodeBase(options.CreateCompilerDefines(), new FileLoader());
            CodeBaseWorker worker = new CodeBaseWorker(codeBase, options, backgroundWorker);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                worker.ExecuteInternal();
            }
            catch (CancelException)
            {
            }
            stopwatch.Stop();
            codeBase.ParseDuration = stopwatch.Elapsed;
            return codeBase;
        }
        private void ExecuteInternal()
        {
            BuildFileList();
            ParseFiles();
        }
        private void ParseFile(int fileNumber, string fileName)
        {
            ReportProgress("Parsing " + fileNumber + " of " + _fileList.Count + ": " + fileName);
            try
            {
                string text = File.ReadAllText(fileName);
                _codeBase.AddFile(fileName, text);
            }
            catch (IOException ex)
            {
                _codeBase.AddError(fileName, ex);
            }
        }
        private void ParseFiles()
        {
            int fileNumber = 0;
            foreach (string fileName in _fileList)
            {
                fileNumber += 1;
                ParseFile(fileNumber, fileName);
            }
        }
        private void ReportProgress(string message)
        {
            CheckForCancel();
            _backgroundWorker.ReportProgress(0, message);
        }
    }
}
