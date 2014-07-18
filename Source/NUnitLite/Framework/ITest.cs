// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

namespace NUnitLite.Framework
{
    public interface ITest
    {
        string Name { get; }
        string FullName { get; }
        int TestCaseCount { get; }

        TestResult Run();
        TestResult Run(TestListener listener);
    }
}
