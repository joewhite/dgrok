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
        private List<string> _fileList;
        private CodeBaseOptions _options;

        private CodeBaseWorker(CodeBase codeBase, CodeBaseOptions options, BackgroundWorker backgroundWorker)
        {
            _codeBase = codeBase;
            _options = options;
            _backgroundWorker = backgroundWorker;
        }

        private void BuildFileList()
        {
            CheckForCancel();
            _backgroundWorker.ReportProgress(0, "Building file list...");
            _fileList = _options.ListFiles();
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
        private void ParseFile(string fileName)
        {
            CheckForCancel();
            string text = File.ReadAllText(fileName);
            _codeBase.AddFile(fileName, text);
        }
        private void ParseFiles()
        {
            ConsumerScheduler<string> scheduler = new ConsumerScheduler<string>(_backgroundWorker,
                _fileList, ParseFile, _options.ParserThreadCount, "Parsing files");
            scheduler.Execute();
        }
    }
}
