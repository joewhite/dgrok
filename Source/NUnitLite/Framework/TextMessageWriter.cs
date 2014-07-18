// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;
using System.Text;
using System.Collections;
using NUnitLite.Constraints;

namespace NUnitLite.Framework
{
    public class TextMessageWriter : MessageWriter
    {
        #region Message Formats and Constants
        private static readonly int MAX_LINE_LENGTH = 78;

        private static readonly string Fmt_Connector = " {0} ";
        private static readonly string Fmt_Predicate = "{0} ";
        //private static readonly string Fmt_Label = "{0}";

        private static readonly string Fmt_Null = "null";
        private static readonly string Fmt_EmptyString = "<string.Empty>";

        private static readonly string Fmt_String = "\"{0}\"";
        private static readonly string Fmt_Char = "'{0}'";
        private static readonly string Fmt_ValueType = "{0}";
        private static readonly string Fmt_Default = "<{0}>";
        #endregion

        #region Constructors
        public TextMessageWriter() { }

        public TextMessageWriter(string userMessage, params object[] args)
        {
            this.WriteMessageLine(userMessage, args);
        }
        #endregion

        #region Public Methods - High Level
        /// <summary>
        /// Method to write single line  message with optional args, usually
        /// written to precede the general failure message, at a givel 
        /// indentation level.
        /// </summary>
        /// <param name="level">The indentation level of the message</param>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        public override void WriteMessageLine(int level, string message, params object[] args)
        {
            if (message != null)
            {
                while (level-- >= 0) Write("  ");

                if (args != null && args.Length > 0)
                    message = string.Format(message, args);

                WriteLine(message);
            }
        }

        /// <summary>
        /// Display Expected and Actual lines for a constraint. This
        /// is called by MessageWriter's default implementation of 
        /// WriteMessageTo and provides the generic two-line display. 
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        /// <param name="actual">The actual value causing the failure</param>
        public override void DisplayDifferences(Constraint constraint, object actual)
        {
            WriteExpectedLine(constraint);
            WriteActualLine(actual);
        }

        /// <summary>
        /// Display Expected and Actual lines for given values. This
        /// method may be called by constraints that need more control over
        /// the display of actual and expected values than is provided
        /// by the default implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        public override void DisplayDifferences(object expected, object actual)
        {
            WriteExpectedLine(expected);
            WriteActualLine(actual);
        }

        /// <summary>
        /// Display the expected and actual string values on separate lines.
        /// If the mismatch parameter is >=0, an additional line is displayed
        /// line containing a caret that points to the mismatch point.
        /// </summary>
        /// <param name="expected">The expected string value</param>
        /// <param name="actual">The actual string value</param>
        /// <param name="ignoreCase">True if case should be ignored comparing the strings</param>
        /// <param name="mismatch">The point at which the strings don't match or -1</param>
        public override void DisplayStringDifferences(string expected, string actual, int mismatch)
        {
            // Maximum string we can display without truncating
            int maxStringLength = MAX_LINE_LENGTH
                - Msgs.PrefixLength   // Allow for prefix
                - 2;                    // 2 quotation marks

            mismatch = MsgUtils.ClipExpectedAndActual(
                ref expected, ref actual, maxStringLength, mismatch, true );

            DisplayDifferences(expected, actual);
            if (mismatch >= 0)
                WriteCaretLine(mismatch);
        }
        #endregion

        #region Public Methods - Low Level
        public override void WriteConnector(string connector)
        {
            Write(Fmt_Connector, connector);
        }

        public override void WritePredicate(string predicate)
        {
            Write(Fmt_Predicate, predicate);
        }

        //public override void WriteLabel(string label)
        //{
        //    Write(Fmt_Label, label);
        //}

        public override void WriteExpectedValue(object obj)
        {
            WriteValue(obj);
        }

        public override void WriteActualValue(object obj)
        {
            WriteValue(obj);
        }

        public override void WriteValue(object obj)
        {
            if (obj == null)
                Write(Fmt_Null);
            else if (obj.GetType().IsArray)
                WriteArray((Array)obj);
            else if (obj is ICollection)
                WriteCollection((ICollection)obj);
            else if (obj is string)
                WriteString((string)obj);
            else if (obj is char)
                WriteChar((char)obj);
            else if (obj is double)
                WriteDouble((double)obj);
            else if (obj is float)
                WriteFloat((float)obj);
            else if (obj is decimal)
                WriteDecimal((decimal)obj);
            else if (obj.GetType().IsValueType)
                Write(Fmt_ValueType, obj);
            else
                Write(Fmt_Default, obj);
        }

        private void WriteArray(Array array)
        {
            int rank = array.Rank;
            int[] products = new int[rank];

            for (int product = 1, r = rank; --r >= 0; )
                products[r] = product *= array.GetLength(r);

            int count = 0;
            foreach (object obj in array)
            {
                if (count > 0)
                    Write(", ");

                bool startSegment = false;
                for (int r = 0; r < rank; r++)
                {
                    startSegment = startSegment || count % products[r] == 0;
                    if (startSegment) Write("< ");
                }

                WriteValue(obj);

                ++count;

                bool nextSegment = false;
                for (int r = 0; r < rank; r++)
                {
                    nextSegment = nextSegment || count % products[r] == 0;
                    if (nextSegment) Write(" >");
                }
            }
        }

        private void WriteCollection(ICollection collection)
        {
            int count = 0;
            Write("< ");
            foreach (object obj in collection)
            {
                if (count > 0)
                    Write(", ");
                WriteValue(obj);
                ++count;
            }
            Write(" >");
        }

        private void WriteString(string s)
        {
            if (s == string.Empty)
                Write(Fmt_EmptyString);
            else
                Write(Fmt_String, s);
        }

        private void WriteChar(char c)
        {
            Write(Fmt_Char, c);
        }

        private void WriteDouble(double d)
        {

            if (double.IsNaN(d) || double.IsInfinity(d))
                Write(d);
            else
            {
                string s = d.ToString("G17");

                if (s.IndexOf('.') > 0)
                    Write(s + "d");
                else
                    Write(s + ".0d");
            }
        }

        private void WriteFloat(float f)
        {
            if (float.IsNaN(f) || float.IsInfinity(f))
                Write(f);
            else
            {
                string s = f.ToString("G9");

                if (s.IndexOf('.') > 0)
                    Write(s + "f");
                else
                    Write(s + ".0f");
            }
        }

        private void WriteDecimal(Decimal d)
        {
            Write(d.ToString("G29") + "m");
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Write the generic 'Expected' line for a constraint
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        private void WriteExpectedLine(Constraint constraint)
        {
            Write(Msgs.Pfx_Expected);
            constraint.WriteDescriptionTo(this);
            WriteLine();
        }

        /// <summary>
        /// Write the generic 'Expected' line for a given value
        /// </summary>
        /// <param name="expected">The expected value</param>
        private void WriteExpectedLine(object expected)
        {
            Write(Msgs.Pfx_Expected);
            WriteExpectedValue(expected);
            WriteLine();
        }

        /// <summary>
        /// Write the generic 'Actual' line for a given value
        /// </summary>
        /// <param name="actual">The actual value causing a failure</param>
        private void WriteActualLine(object actual)
        {
            Write(Msgs.Pfx_Actual);
            WriteActualValue(actual);
            WriteLine();
        }

        private void WriteCaretLine(int mismatch)
        {
            // We subtract 2 for the initial 2 blanks and add back 1 for the initial quote
            WriteLine("  {0}^", new string('-', Msgs.PrefixLength + mismatch - 2 + 1));
        }
        #endregion
    }
}
