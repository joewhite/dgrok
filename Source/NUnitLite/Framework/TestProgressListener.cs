// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Framework
{
    public class TestProgressListener : TestListener
    {
        private string indent = "";

        public void TestStarted(ITest test)
        {
            Console.WriteLine(indent + test.Name);
            indent += "  ";
        }

        public void TestFinished(TestResult result)
        {
            indent = indent.Substring(2);
        }
    }
}
