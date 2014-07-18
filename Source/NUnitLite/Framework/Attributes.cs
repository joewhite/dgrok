// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Framework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TestFixtureAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class TestAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class SetUpAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class TearDownAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ExpectedExceptionAttribute : Attribute
    {
        private Type exceptionType;
        private string handler;

        public ExpectedExceptionAttribute() { }

        public ExpectedExceptionAttribute(Type exceptionType)
        {
            this.exceptionType = exceptionType;
        }

        public Type ExceptionType
        {
            get { return exceptionType; }
        }

        public string Handler
        {
            get { return handler; }
            set { handler = value; }
        }
    }
}
