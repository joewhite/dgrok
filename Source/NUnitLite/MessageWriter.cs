// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;
using System.Collections;
using NUnitLite.Constraints;

namespace NUnitLite
{
    public abstract class MessageWriter : StringWriter
    {
        /// <summary>
        /// Method to write single line  message with optional args, usually
        /// written to precede the general failure message.
        /// </summary>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        public void WriteMessageLine(string message, params object[] args)
        {
            WriteMessageLine(0, message, args);
        }

        /// <summary>
        /// Method to write single line  message with optional args, usually
        /// written to precede the general failure message, at a givel 
        /// indentation level.
        /// </summary>
        /// <param name="level">The indentation level of the message</param>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        public abstract void WriteMessageLine(int level, string message, params object[] args);

        /// <summary>
        /// Display Expected and Actual lines for a constraint. This
        /// is called by MessageWriter's default implementation of 
        /// WriteMessageTo and provides the generic two-line display. 
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        /// <param name="actual">The actual value causing the failure</param>
        public abstract void DisplayDifferences(Constraint constraint, object actual);

        /// <summary>
        /// Display Expected and Actual lines for given values. This
        /// method may be called by constraints that need more control over
        /// the display of actual and expected values than is provided
        /// by the default implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        public abstract void DisplayDifferences(object expected, object actual);

        /// <summary>
        /// Display the expected and actual string values on separate lines.
        /// If the mismatch parameter is >=0, an additional line is displayed
        /// line containing a caret that points to the mismatch point.
        /// </summary>
        /// <param name="expected">The expected string value</param>
        /// <param name="actual">The actual string value</param>
        /// <param name="ignoreCase">True if case should be ignored comparing the strings</param>
        /// <param name="mismatch">The point at which the strings don't match or -1</param>
        public abstract void DisplayStringDifferences(string expected, string actual, int mismatch);

        /// <summary>
        /// Writes the text for a connector.
        /// </summary>
        /// <param name="connector">The connector.</param>
        public abstract void WriteConnector(string connector);

        /// <summary>
        /// Writes the text for a predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public abstract void WritePredicate(string predicate);

        /// <summary>
        /// Writes the text for an expected value.
        /// </summary>
        /// <param name="value">The expected value.</param>
        public abstract void WriteExpectedValue(object value);

        /// <summary>
        /// Writes the text for an actual value.
        /// </summary>
        /// <param name="value">The actual value.</param>
        public abstract void WriteActualValue(object value);

        /// <summary>
        /// Writes the text for a generalized value.
        /// </summary>
        /// <param name="value">The value.</param>
        public abstract void WriteValue(object value);
    }
}
