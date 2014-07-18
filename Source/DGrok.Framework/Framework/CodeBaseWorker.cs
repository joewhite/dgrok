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
