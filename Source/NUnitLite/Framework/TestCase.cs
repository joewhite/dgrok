// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Reflection;

namespace NUnitLite.Framework
{
    public class TestCase : ITest
    {
        #region Instance Variables
        private string name;
        protected string fullName;
        #endregion

        #region Constructor
        public TestCase(string name) //: base( name  ) 
        {
            this.name = name;
            this.fullName = this.GetType().FullName + "." + name;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
        }

        public string FullName
        {
            get { return fullName; }
        }

        public int TestCaseCount
        {
            get { return 1; }
        }
        #endregion

        #region Public Methods
        public TestResult Run()
        {
            return Run( new NullListener() );
        }

        public TestResult Run(TestListener listener)
        {
            listener.TestStarted(this);

            TestResult result = new TestResult(this);
            Run(result, listener);

            listener.TestFinished(result);

            return result;
        }
        #endregion

        #region Protected Methods
        protected virtual void SetUp() { }

        protected virtual void TearDown() { }

        protected virtual void Run(TestResult result, TestListener listener)
        {
            try
            {
                RunBare();
                result.Success();
            }
            catch (NUnitLiteException nex)
            {
                result.RecordException(nex.InnerException);
            }
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                result.RecordException(ex);
            }
        }

        protected void RunBare()
        {
            SetUp();
            try
            {
                RunTest();
            }
            finally
            {
                TearDown();
            }
        }

        protected virtual void RunTest()
        {
            MethodInfo method = GetMethod( this.Name, BindingFlags.Public | BindingFlags.Instance );

            try
            {
                InvokeMethod( method );
                ProcessNoException(method);
            }
            catch (TargetInvocationException ex)
            {
                ProcessException(method, ex.InnerException);
            }
        }

        protected virtual MethodInfo GetMethod(string name, BindingFlags flags, params Type[] argTypes)
        {
            return Reflect.GetMethod(this.GetType(), name, flags, argTypes);
        }

        protected virtual void InvokeMethod(MethodInfo method, params object[] args)
        {
            method.Invoke(this, args);
        }
        #endregion

        #region Private Methods       
        private static void ProcessNoException(MethodInfo method)
        {
            ExpectedExceptionAttribute exceptionAttribute =
                (ExpectedExceptionAttribute)Reflect.GetAttribute(method, typeof(ExpectedExceptionAttribute));

            if (exceptionAttribute != null)
                Assert.Fail("Expected Exception of type <{0}>, but none was thrown", exceptionAttribute.ExceptionType);
        }

        private void ProcessException(MethodInfo method, Exception caughtException)
        {
            ExpectedExceptionAttribute exceptionAttribute =
                (ExpectedExceptionAttribute)Reflect.GetAttribute(method, typeof(ExpectedExceptionAttribute));

            if (exceptionAttribute == null)
                throw new NUnitLiteException("", caughtException);

            Type expectedType = exceptionAttribute.ExceptionType;
            if ( expectedType != null && expectedType != caughtException.GetType() )
                Assert.Fail("Expected Exception of type <{0}>, but was <{1}>", exceptionAttribute.ExceptionType, caughtException.GetType());

            MethodInfo handler = GetExceptionHandler(method.ReflectedType, exceptionAttribute.Handler);

            if (handler != null)
            {
                try
                {
                    InvokeMethod( handler, caughtException );
                }
                catch (TargetInvocationException ex)
                {
                    throw new NUnitLiteException("", ex.InnerException);
                }
            }
        }

        private MethodInfo GetExceptionHandler(Type type, string handlerName)
        {
            if (handlerName == null && Reflect.HasInterface( type, typeof(IExpectException) ) )
                handlerName = "HandleException";

            if (handlerName == null)
                return null;

            MethodInfo handler = GetMethod(handlerName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                new Type[] { typeof(Exception) });

            if (handler == null)
                Assert.Fail("The specified exception handler {0} was not found", handlerName);

            return handler;
        }
        #endregion
    }
}
