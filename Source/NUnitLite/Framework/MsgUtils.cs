// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Text;
using System.Collections;

namespace NUnitLite.Framework
{
    /// <summary>
    /// Static methods used in creating messages
    /// </summary>
    class MsgUtils
    {
        private static readonly string ELLIPSIS = "...";

        /// <summary>
        /// Returns the representation of a type as used in NUnitLite.
        /// This is the same as Type.ToString() except for arrays,
        /// which are displayed with their declared sizes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetTypeRepresentation(object obj)
        {
            Array array = obj as Array;
            if ( array == null )
                return obj.GetType().ToString();

            StringBuilder sb = new StringBuilder();
            Type elementType = array.GetType();
            int nest = 0;
            while (elementType.IsArray)
            {
                elementType = elementType.GetElementType();
                ++nest;
            }
            sb.Append(elementType.ToString());
            sb.Append('[');
            for (int r = 0; r < array.Rank; r++)
            {
                if (r > 0) sb.Append(',');
                sb.Append(array.GetLength(r));
            }
            sb.Append(']');

            while (--nest > 0)
                sb.Append("[]");

            return sb.ToString();
        }

        public static string ConvertWhiteSpace(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
        }

        /// <summary>
        /// Return the a string representation for a set of indices into an array
        /// </summary>
        /// <param name="indices">Array of indices for which a string is needed</param>
        public static string GetArrayIndicesAsString(int[] indices)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            for (int r = 0; r < indices.Length; r++)
            {
                if (r > 0) sb.Append(',');
                sb.Append(indices[r].ToString());
            }
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Get an array of indices representing the point in a collection or
        /// array corresponding to a single int index into the collection.
        /// </summary>
        /// <param name="collection">The collection to which the indices apply</param>
        /// <param name="index">Index in the collection</param>
        /// <returns>Array of indices</returns>
        public static int[] GetArrayIndicesFromCollectionIndex(ICollection collection, int index)
        {
            Array array = collection as Array;
            int rank = array == null ? 1 : array.Rank;
            int[] result = new int[rank];

            for (int r = array.Rank; --r > 0; )
            {
                int l = array.GetLength(r);
                result[r] = index % l;
                index /= l;
            }

            result[0] = index;
            return result;
        }

        public static int ClipExpectedAndActual(ref string expected, ref string actual,
            int maxStringLength, int mismatch, bool convertWhiteSpace)
        {
            if (expected.Length > maxStringLength || actual.Length > maxStringLength)
            {
                // We'll clip at least one end of the string
                int clipLength = maxStringLength - ELLIPSIS.Length;

                if (mismatch >= clipLength)
                {
                    // Need to clip both strings at start
                    int clipStart = mismatch - clipLength / 2;
                    mismatch = mismatch - clipStart + ELLIPSIS.Length;

                    // Clip the expected value at start and at end if needed
                    if (expected.Length - clipStart > maxStringLength)
                        expected = ELLIPSIS + expected.Substring(
                            clipStart, clipLength - ELLIPSIS.Length) + ELLIPSIS;
                    else
                        expected = ELLIPSIS + expected.Substring(clipStart);

                    // Clip the actual value at start and at end if needed
                    if (actual.Length - clipStart > maxStringLength)
                        actual = ELLIPSIS + actual.Substring(
                            clipStart, clipLength - ELLIPSIS.Length) + ELLIPSIS;
                    else
                        actual = ELLIPSIS + actual.Substring(clipStart) + ELLIPSIS;
                }
                else
                {
                    if (expected.Length > maxStringLength)
                        expected = expected.Substring(0, clipLength) + ELLIPSIS;

                    if (actual.Length > maxStringLength)
                        actual = actual.Substring(0, clipLength) + ELLIPSIS;
                }
            }

            if (convertWhiteSpace)
            {
                expected = ConvertWhiteSpace(expected);
                actual = ConvertWhiteSpace(actual);
                if (mismatch >= 0)
                    mismatch = FindMismatchPosition(expected, actual, mismatch, true);
            }

            return mismatch;
        }

        /// <summary>
        /// Shows the position two strings start to differ.  Comparison 
        /// starts at the start index.
        /// </summary>
        /// <param name="sExpected"></param>
        /// <param name="sActual"></param>
        /// <param name="iStart"></param>
        /// <returns>-1 if no mismatch found, or the index where mismatch found</returns>
        static public int FindMismatchPosition(string expected, string actual, int istart, bool ignoreCase)
        {
            int length = Math.Min(expected.Length, actual.Length);

            string s1 = ignoreCase ? expected.ToLower() : expected;
            string s2 = ignoreCase ? actual.ToLower() : actual;

            for (int i = istart; i < length; i++)
            {
                if (s1[i] != s2[i])
                    return i;
            }

            //
            // Strings have same content up to the length of the shorter string.
            // Mismatch occurs because string lengths are different, so show
            // that they start differing where the shortest string ends
            //
            if (expected.Length != actual.Length)
                return length;

            //
            // Same strings : We shouldn't get here
            //
            return -1;
        }
    }
}
