// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using NUnitLite.Constraints;

namespace NUnitLite.Framework
{
    public class Assert
    {
        public static void Null(object expression)
        {
            Assert.That(expression, Is.Null);
        }

        public static void NotNull(object expression)
        {
            Assert.That(expression, Is.Not.Null);
        }

        public static void True(object expression)
        {
            Assert.That(expression, Is.True);
        }

        public static void False(object expression)
        {
            Assert.That(expression, Is.False);
        }

        public static void AreEqual(object expected, object actual)
        {
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that a boolean expression is true
        /// </summary>
        /// <param name="expression">Expression that must be true for the match to succeed</param>
        public static void That(bool expression)
        {
            Assert.That(expression, Is.True, null);
        }

        /// <summary>
        /// Assert that a boolean expression is true, providing a user message for failures
        /// </summary>
        /// <param name="expression">Expression that must be true for the match to succeed</param>
        /// <param name="message">User message to display if the match fails</param>
        public static void That(bool expression, string message)
        {
            Assert.That(expression, Is.True, message);
        }

        /// <summary>
        /// Assert that some expected satisfies a constraint
        /// </summary>
        /// <param name="actual">The expected to be matched</param>
        /// <param name="constraint">The constraint to use</param>
        public static void That(object actual, Constraint constraint)
        {
            Assert.That(actual, constraint, null);
        }

        /// <summary>
        /// Assert that some expected satisfies a constraint, providing a user message to 
        /// display in case of failure.
        /// </summary>
        /// <param name="label">A label to display in front of the expected in case of failure</param>
        /// <param name="actual">The expected to be matched</param>
        /// <param name="constraint">The constraint to use</param>
        /// <param name="message">User message to display if the match fails</param>
        public static void That(object actual, Constraint constraint, string message)
        {
            if (!constraint.Matches(actual))
            {
                TextMessageWriter writer = new TextMessageWriter( message );
                constraint.WriteMessageTo(writer);

                Fail(writer.ToString());
            }
        }

        /// <summary>
        /// Assert that some expected satisfies a constraint, providing a user message 
        /// and substitution arguments to display in case of failure.
        /// </summary>
        /// <param name="label">A label to display in front of the expected in case of failure</param>
        /// <param name="actual">The expected to be matched</param>
        /// <param name="constraint">The constraint to use</param>
        /// <param name="message">User message to display if the match fails</param>
        /// <param name="args">Parameters to be used in formatting the message</param>
        public static void That(object actual, Constraint constraint, string message, params object[] args)
        {
            if (!constraint.Matches(actual))
            {
                TextMessageWriter writer = new TextMessageWriter( FormatMessage(message, args) );
                constraint.WriteMessageTo(writer);

                Fail(writer.ToString());
            }
        }

        /// <summary>
        /// Throw an assertion exception with a message and optional arguments
        /// </summary>
        /// <param name="message">The message, possibly with format placeholders</param>
        /// <param name="args">Arguments used in formatting the string</param>
        public static void Fail(string message, params object[] args)
        {
            throw new AssertionException(FormatMessage( message, args ) );
        }

        /// <summary>
        /// Formats a message with any user-supplied arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        private static string FormatMessage( string message, params object[] args )
        {
            if (message == null)
                return string.Empty;
            else if (args != null && args.Length > 0)
                return string.Format(message, args);
            else
                return message;
        }
    }
}
