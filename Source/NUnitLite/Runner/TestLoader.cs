// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Reflection;
using NUnitLite.Framework;

namespace NUnitLite.Runner
{
    public class TestLoader
    {
        public static Assembly DefaultAssembly;

        public static ITest Load(Assembly[] assemblies)
        {
            TestSuite suite = new TestSuite("Multiple Assemblies");

            foreach (Assembly assembly in assemblies)
                suite.AddTest(Load(assembly));

            return suite;
        }

        public static ITest Load(Assembly assembly)
        {
            TestSuite suite = new TestSuite(assembly.GetName().Name);

            foreach (Type type in assembly.GetTypes())
            {
                if ( Reflect.HasAttribute( type, typeof(TestFixtureAttribute) ) )
                    suite.AddTest(new TestSuite(type));
            }

            return suite;
        }

        public static ITest LoadSuite(Type type)
        {
            PropertyInfo suiteProperty = Reflect.GetSuiteProperty(type);
            if (suiteProperty != null)
                return (ITest)suiteProperty.GetValue(null, Type.EmptyTypes);

            return null;
        }

        public static ITest Load(Type type)
        {
            ITest test = TestLoader.LoadSuite(type);
            if (test == null)
                test = new TestSuite(type);

            return test;
        }

        public static ITest Load( string className )
        {
            Type type = Type.GetType(className);
            if ( type == null && className.IndexOf(',') == -1 )
                type = Type.GetType(className + "," + DefaultAssembly.GetName().Name );

            if (type == null)
                throw new TestRunnerException("Unable to load class " + className);

            return Load(type);
        }

        /// <summary>
        /// Loads a test suite containing all the test classes specified
        /// </summary>
        /// <param name="tests">String array containing the names of the test classes to load</param>
        /// <returns>A suite containing all the tests</returns>
        public static ITest Load(string[] tests)
        {
            TestSuite suite = new TestSuite("Test Fixtures");
            foreach (string name in tests)
                suite.AddTest(TestLoader.Load(name));

            return suite;
        }
   }
}
