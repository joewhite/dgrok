// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

namespace NUnitLite.Framework
{
    public interface TestListener
    {
        void TestStarted(ITest test);
        void TestFinished(TestResult result);
    }
}
