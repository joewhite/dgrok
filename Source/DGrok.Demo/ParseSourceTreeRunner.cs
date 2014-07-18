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
using System.Threading;
using DGrok.Framework;

namespace DGrok.Demo
{
    public class ParseSourceTreeRunner
    {
        private bool _canceled = false;
        private ParseSourceTreeStatus _status = ParseSourceTreeStatus.CreateEmpty();

        public bool Canceled
        {
            get { return _canceled; }
            set { _canceled = value; }
        }
        public ParseSourceTreeStatus Status
        {
            get { return _status; }
        }

        public void BeginExecute(string startingDirectory, string fileMasks)
        {
            _canceled = false;
            ReportProgress("Initializing...");
            Thread thread = new Thread(delegate()
            {
                try
                {
                    IDictionary<string, Exception> results = Execute(startingDirectory, fileMasks);
                    _status = ParseSourceTreeStatus.CreateCompleted(results);
                }
                catch (Exception ex)
                {
                    _status = ParseSourceTreeStatus.CreateError(ex);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
        private IDictionary<string, Exception> Execute(string startingDirectory, string fileMasks)
        {
            ReportProgress("Building file list...");
            List<string> fileNames = new List<string>();
            foreach (string mask in fileMasks.Split(';'))
            {
                string[] searchResults =
                    Directory.GetFiles(startingDirectory, mask, SearchOption.AllDirectories);
                foreach (string fileName in searchResults)
                {
                    if (!ShouldIgnoreFile(fileName))
                        fileNames.Add(fileName);
                }
            }

            IDictionary<string, Exception> results = new Dictionary<string, Exception>();
            int remainingCount = fileNames.Count;
            int passingCount = 0;
            int failingCount = 0;
            foreach (string fileName in fileNames)
            {
                if (_canceled)
                    break;
                ReportProgress(String.Format("Working... {0} passing, {1} failing, {2} remaining. Current: {3}",
                    passingCount, failingCount, remainingCount, Path.GetFileName(fileName)));
                try
                {
                    string source = File.ReadAllText(fileName);
                    Parser parser = Parser.FromText(source, fileName, CompilerDefines.CreateStandard(),
                        new FileLoader());
                    parser.ParseRule(RuleType.Goal);
                    results[fileName] = null;
                    ++passingCount;
                }
                catch (Exception ex)
                {
                    results[fileName] = ex;
                    ++failingCount;
                }
                --remainingCount;
            }
            return results;
        }
        private void ReportProgress(string progress)
        {
            _status = ParseSourceTreeStatus.CreateRunning(progress);
        }
        private static bool ShouldIgnoreFile(string fileName)
        {
            return String.Equals(Path.GetExtension(fileName), ".dproj",
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
