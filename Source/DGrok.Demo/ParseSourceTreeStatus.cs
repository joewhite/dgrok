// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Demo
{
    public class ParseSourceTreeStatus
    {
        private Exception _error;
        private bool _isRunning;
        private string _progress;
        private IDictionary<string, Exception> _results;

        private ParseSourceTreeStatus(bool isRunning, string progress,
            IDictionary<string, Exception> results, Exception error)
        {
            _isRunning = isRunning;
            _progress = progress;
            _results = results;
            _error = error;
        }

        public static ParseSourceTreeStatus CreateCompleted(IDictionary<string, Exception> results)
        {
            return new ParseSourceTreeStatus(false, null, results, null);
        }
        public static ParseSourceTreeStatus CreateEmpty()
        {
            return CreateCompleted(new Dictionary<string, Exception>());
        }
        public static ParseSourceTreeStatus CreateError(Exception error)
        {
            return new ParseSourceTreeStatus(false, null, null, error);
        }
        public static ParseSourceTreeStatus CreateRunning(string progress)
        {
            return new ParseSourceTreeStatus(true, progress, null, null);
        }

        public Exception Error
        {
            get { return _error; }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
        }
        public string Progress
        {
            get { return _progress; }
        }
        public IDictionary<string, Exception> Results
        {
            get { return _results; }
        }
    }
}
