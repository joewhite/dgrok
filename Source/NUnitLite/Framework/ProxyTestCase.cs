// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Reflection;

namespace NUnitLite.Framework
{
    /// <summary>
    /// ProxyTestCase class represents a test case that uses another,
    /// non-TestCase, object to represent the test fixture. All test
    /// methods, setup and teardown are methods on that object.
    /// </summary>
    public class ProxyTestCase : TestCase
    {
        private object fixture;
        private MethodInfo setup;
        private MethodInfo teardown;

        public ProxyTestCase(string name, object fixture) : base(name)
        {
            this.fixture = fixture;
            this.fullName = this.fixture.GetType().FullName + "." + name;

            foreach (MethodInfo method in fixture.GetType().GetMethods())
            {
                if ( Reflect.HasAttribute( method, typeof(SetUpAttribute) ) )
                    setup = method;

                if ( Reflect.HasAttribute( method, typeof(TearDownAttribute) ) )
                    teardown = method;
            }
        }

        protected override void SetUp()
        {
            if (setup != null)
            {
                Assert.That(HasValidSetUpTearDownSignature(setup), "Invalid SetUp method: must return void and have no arguments");
                InvokeMethod( setup );
            }
        }

        protected override void TearDown()
        {
            if (teardown != null)
            {
                Assert.That(HasValidSetUpTearDownSignature(teardown), "Invalid TearDown method: must return void and have no arguments");
                InvokeMethod( teardown );
            }
        }

        protected override void InvokeMethod(MethodInfo method, params object[] args)
        {
            method.Invoke(this.fixture, args);
        }

        protected override MethodInfo GetMethod(string name, BindingFlags flags, params Type[] argTypes)
        {
            return Reflect.GetMethod(fixture.GetType(), name, flags, argTypes);
        }

        private static bool HasValidSetUpTearDownSignature(MethodInfo method)
        {
            return method.ReturnType == typeof(void)
                && method.GetParameters().Length == 0; ;
        }
    }
}
