// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Framework
{
    public class InvalidTestCase : TestCase
    {
        string message;

        public InvalidTestCase( string name, string message ) : base( name )
        {
            this.message = message;
        }

        protected override void Run(TestResult result, TestListener listener)
        {
            result.Failure(message, null);
        }
    }
}
