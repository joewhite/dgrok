// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Collections;

namespace NUnitLite.Framework
{
    public enum ResultState
    {
        NotRun,
        Success,
        Failure,
        Error
    }

    public class TestResult
    {
        private ITest test;

        private ResultState resultState = ResultState.NotRun;

        private string message;
        private string stackTrace;

        private ArrayList results;

        public TestResult(ITest test)
        {
            this.test = test;
        }

        public ITest Test
        {
            get { return test; }
        }

        public ResultState ResultState
        {
            get { return resultState; }
        }

        public IList Results
        {
            get { return results; }
        }

        public bool IsSuiteResult
        {
            get { return results != null; }
        }

        public bool Executed
        {
            get { return resultState != ResultState.NotRun; }
        }

        public bool IsSuccess
        {
            get { return resultState == ResultState.Success; }
        }

        public bool IsFailure
        {
            get { return resultState == ResultState.Failure; }
        }

        public bool IsError
        {
            get { return resultState == ResultState.Error; }
        }

        public string Message
        {
            get { return message; }
        }

        public string StackTrace
        {
            get { return stackTrace; }
        }

        public void AddResult(TestResult result)
        {
            if (results == null)
                results = new ArrayList();
            results.Add(result);

            switch (result.ResultState)
            {
                case ResultState.Error:
                case ResultState.Failure:
                    this.Failure("Component test failure", null);
                    break;
                default:
                    break;
            }
        }

        public void Success()
        {
            this.resultState = ResultState.Success;
            this.message = null;
        }

        public void Failure(string message, string stackTrace)
        {
            this.resultState = ResultState.Failure;
            if (this.message == null || this.message == string.Empty)
                this.message = message;
            else
                this.message = this.message + Environment.NewLine + message;
            this.stackTrace = stackTrace;
        }

        public void Error(Exception ex)
        {
            this.resultState = ResultState.Error;
            this.message = ex.GetType().ToString() + " : " + ex.Message;
            this.stackTrace = ex.StackTrace;
        }

        public void NotRun(string message)
        {
            this.resultState = ResultState.NotRun;
            this.message = message;
        }

        public void RecordException(Exception ex)
        {
            if (ex is AssertionException)
                this.Failure(ex.Message, StackFilter.Filter(ex.StackTrace));
            else
                this.Error(ex);
        }
    }
}
