// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Framework
{
    public class NullListener : TestListener
    {
        public void TestStarted(ITest test) { }

        public void TestFinished(TestResult result) { }
    }
}
