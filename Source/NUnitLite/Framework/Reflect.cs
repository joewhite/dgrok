// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Reflection;

namespace NUnitLite.Framework
{
    public class Reflect
    {
        #region Types
        public static bool IsTestCaseClass(Type type)
        {
            return typeof(TestCase).IsAssignableFrom(type);
        }
        #endregion

        #region Interfaces

        /// <summary>
        /// Check to see if a type implements an interface.
        /// </summary>
        /// <param name="type">The type to examine</param>
        /// <param name="interfaceType">The interface type to check for</param>
        /// <returns>True if the interface is implemented by the type</returns>
        public static bool HasInterface(Type type, Type interfaceType)
        {
            // NOTE: IsAssignableForm fails so we look for the name
            return HasInterface( type, interfaceType.FullName );
        }

        /// <summary>
        /// Check to see if a type implements a named interface.
        /// </summary>
        /// <param name="fixtureType">The type to examine</param>
        /// <param name="interfaceName">The FullName of the interface to check for</param>
        /// <returns>True if the interface is implemented by the type</returns>
        public static bool HasInterface(Type fixtureType, string interfaceName)
        {
            foreach (Type type in fixtureType.GetInterfaces())
                if (type.FullName == interfaceName)
                    return true;
            return false;
        }
        #endregion

        #region Methods
        public static MethodInfo GetMethod(Type type, string name, params Type[] argTypes)
        {
            if (argTypes == null) argTypes = Type.EmptyTypes;
            return type.GetMethod(name, argTypes);
        }

        public static MethodInfo GetMethod(Type type, string name, BindingFlags flags, params Type[] argTypes)
        {
            if (argTypes == null) argTypes = Type.EmptyTypes;
            return type.GetMethod(name, flags, null, argTypes, null);
        }
        #endregion

        #region Construction
        public static bool HasConstructor(Type type, params Type[] argTypes)
        {
            return GetConstructor(type, argTypes) != null;
        }

        public static ConstructorInfo GetConstructor(Type type, params Type[] argTypes)
        {
            if (argTypes == null) argTypes = Type.EmptyTypes;
            return type.GetConstructor(argTypes);
        }

        public static object Construct(Type type, params object[] args)
        {
            Type[] argTypes;

            if (args == null)
            {
                args = new object[0];
                argTypes = Type.EmptyTypes;
            }
            else
            {
                argTypes = Type.GetTypeArray(args);
            }

            ConstructorInfo ctor = GetConstructor( type, argTypes );
            return ctor.Invoke(args);
        }

        public static ITest ConstructTestCase(MethodInfo method)
        {
            return ConstructTestCase(method, method.Name);
        }

        public static ITest ConstructTestCase(MethodInfo method, string name)
        {
            return ConstructTestCase(method.ReflectedType, name);
        }

        private static ITest ConstructTestCase(Type type, string name)
        {
            if (IsTestCaseClass(type))
                return (ITest)Construct( type, name );
            else
                return new ProxyTestCase( name, Construct( type ) );
        }
        #endregion

        #region Attributes
        public static bool HasAttribute(MemberInfo member, Type attr)
        {
            return member.GetCustomAttributes(attr, true).Length > 0;
        }

        public static Attribute GetAttribute(MemberInfo member, Type attr)
        {
            object[] attrs = member.GetCustomAttributes(typeof(ExpectedExceptionAttribute), false);
            return attrs.Length == 0 ? null : attrs[0] as Attribute;
        }
        #endregion

        #region Properties
        public static PropertyInfo GetSuiteProperty(Type type)
        {
            return type.GetProperty("Suite", typeof(ITest), Type.EmptyTypes);
        }
        #endregion
    }
}
