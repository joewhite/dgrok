// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Reflection;
using NUnitLite.Framework;

namespace NUnitLite.Constraints
{
    public class EqualConstraint : Constraint
    {
        private static IDictionary constraintHelpers = new Hashtable();

        private object expected;

        private ArrayList failurePoints;

        private bool ignoreCase;
        private bool compareAsCollection;

        #region Static Method to Add Equality Helpers - Experimental
        public static void SetConstraintForType(Type argumentType, Type constraintType)
        {
            constraintHelpers[argumentType] = constraintType;
        }
        #endregion

        #region Constructor
        public EqualConstraint(object expected)
        {
            this.expected = expected;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Flag the constraint to ignore case and return self
        /// </summary>
        public Constraint IgnoreCase
        {
            get
            {
                ignoreCase = true;
                return this;
            }
        }

        /// <summary>
        /// Flag the constraint to compare arrays as collections
        /// and return self.
        /// </summary>
        public Constraint AsCollection
        {
            get
            {
                compareAsCollection = true;
                return this;
            }
        }
        #endregion

        #region Public Methods
        public override bool Matches(object actual)
        {
            this.actual = actual;
            this.failurePoints = new ArrayList();

            return ObjectsEqual( expected, actual );
        }

        /// <summary>
        /// Write a failure message. Overridden to provide custom 
        /// failure messages for EqualConstraint.
        /// </summary>
        /// <param name="writer">The MessageWriter to write to</param>
        public override void WriteMessageTo(MessageWriter writer)
        {
            DisplayDifferences(writer, expected, actual, 0);
        }


        /// <summary>
        /// Write description of this constraint
        /// </summary>
        /// <param name="writer">The MessageWriter to write to</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteExpectedValue( expected );
        }

        private void DisplayDifferences(MessageWriter writer, object expected, object actual, int depth)
        {
            if (expected is string && actual is string)
                DisplayStringDifferences(writer, (string)expected, (string)actual);
            else if (expected is ICollection && actual is ICollection)
                DisplayCollectionDifferences(writer, (ICollection)expected, (ICollection)actual, depth);
            else
                writer.DisplayDifferences(expected, actual);
        }
        #endregion

        #region ObjectsEqual
        private bool ObjectsEqual(object expected, object actual)
        {
            if (expected == null && actual == null)
                return true;

            if (expected == null || actual == null)
                return false;

            Type expectedType = expected.GetType();
            Type actualType = actual.GetType();

            if (IsNumericType(expected) && IsNumericType(actual))
            {
                //
                // Convert to strings and compare result to avoid
                // issues with different types that have the same
                // expected
                //
                string sExpected = expected is decimal ? ((decimal)expected).ToString("G29") : expected.ToString();
                string sActual = actual is decimal ? ((decimal)actual).ToString("G29") : actual.ToString();
                return sExpected.Equals(sActual);
            }

            if (expected is string && actual is string)
            {
                return string.Compare((string)expected, (string)actual, ignoreCase) == 0;
            }

            foreach (Type type in constraintHelpers.Keys)
            {
                if (type.IsInstanceOfType(expected) && type.IsInstanceOfType(actual))
                {
                    Type constraintType = (Type)constraintHelpers[type];
                    Constraint constraint = (Constraint)Reflect.Construct(constraintType, expected);
                    return constraint.Matches(actual);
                }
            }

            if (expectedType.IsArray && actualType.IsArray && !compareAsCollection)
            {
                int rank = expectedType.GetArrayRank();

                if (rank != actualType.GetArrayRank())
                    return false;

                Array expectedArray = (Array)expected;
                Array actualArray = (Array)actual;

                for (int r = 1; r < rank; r++)
                    if (expectedArray.GetLength(r) != actualArray.GetLength(r))
                        return false;

                return CollectionsEqual((ICollection)expected, (ICollection)actual);
            }

            if (expected is ICollection && actual is ICollection)
                return CollectionsEqual((ICollection)expected, (ICollection)actual);

            return expected.Equals(actual);
        }

        private bool CollectionsEqual(ICollection expected, ICollection actual)
        {
            IEnumerator expectedEnum = expected.GetEnumerator();
            IEnumerator actualEnum = actual.GetEnumerator();

            int count;
            for (count = 0; expectedEnum.MoveNext() && actualEnum.MoveNext(); count++)
            {
                if (!ObjectsEqual(expectedEnum.Current, actualEnum.Current))
                    break;
            }

            if (count == expected.Count && count == actual.Count)
                return true;

            failurePoints.Insert(0, count);
            return false;
        }

        /// <summary>
        /// Checks the type of the object, returning true if
        /// the object is a numeric type.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>true if the object is a numeric type</returns>
        private bool IsNumericType(Object obj)
        {
            if (null != obj)
            {
                if (obj is byte) return true;
                if (obj is sbyte) return true;
                if (obj is decimal) return true;
                if (obj is double) return true;
                if (obj is float) return true;
                if (obj is int) return true;
                if (obj is uint) return true;
                if (obj is long) return true;
                if (obj is short) return true;
                if (obj is ushort) return true;

                if (obj is System.Byte) return true;
                if (obj is System.SByte) return true;
                if (obj is System.Decimal) return true;
                if (obj is System.Double) return true;
                if (obj is System.Single) return true;
                if (obj is System.Int32) return true;
                if (obj is System.UInt32) return true;
                if (obj is System.Int64) return true;
                if (obj is System.UInt64) return true;
                if (obj is System.Int16) return true;
                if (obj is System.UInt16) return true;
            }
            return false;
        }
        #endregion

        #region DisplayStringDifferences
        private void DisplayStringDifferences(MessageWriter writer, string expected, string actual)
        {
            int mismatch = MsgUtils.FindMismatchPosition(expected, actual, 0, this.ignoreCase);

            if (expected.Length == actual.Length)
                writer.WriteMessageLine(Msgs.StringsDiffer_1, expected.Length, mismatch);
            else
                writer.WriteMessageLine(Msgs.StringsDiffer_2, expected.Length, actual.Length, mismatch);

            writer.DisplayStringDifferences(expected, actual, mismatch);
        }
        #endregion

        #region DisplayCollectionDifferences
        /// <summary>
        /// Display the failure information for two collections that did not match.
        /// </summary>
        /// <param name="expected">The expected collection.</param>
        /// <param name="actual">The actual collection</param>
        /// <param name="failurePoint">Index of the failure point in the collections</param>
        /// <param name="ignoreCase">True if case is ignored in comparing string elements</param>
        private void DisplayCollectionDifferences(MessageWriter writer, ICollection expected, ICollection actual, int depth)
        {
            int failurePoint = failurePoints.Count > depth ? (int)failurePoints[depth] : -1;

            DisplayCollectionTypesAndSizes(writer, expected, actual, depth);

            if (failurePoint >= 0)
            {
                DisplayFailurePoint(writer, expected, actual, failurePoint, depth);
                if (failurePoint < expected.Count && failurePoint < actual.Count)
                    DisplayDifferences(
                        writer,
                        GetValueFromCollection(expected, failurePoint),
                        GetValueFromCollection(actual, failurePoint),
                        ++depth);
                else if (expected.Count < actual.Count)
                    DisplayExtraElements(writer, actual, failurePoint, 3);
                else
                    DisplayMissingElements(writer, expected, failurePoint, 3);
            }
        }

        /// <summary>
        /// Displays a single line showing the types and sizes of the expected
        /// and actual collections or arrays. If both are identical, the value is 
        /// only shown once.
        /// </summary>
        /// <param name="expected">The expected collection or array</param>
        /// <param name="actual">The actual collection or array</param>
        private void DisplayCollectionTypesAndSizes(MessageWriter writer, ICollection expected, ICollection actual, int indent)
        {
            string expectedType = MsgUtils.GetTypeRepresentation(expected);
            string actualType = MsgUtils.GetTypeRepresentation(actual);

            if (expectedType == actualType)
                writer.WriteMessageLine(indent, Msgs.CollectionType_1, expectedType);
            else
                writer.WriteMessageLine(indent, Msgs.CollectionType_2, expectedType, actualType);
        }

        /// <summary>
        /// Displays a single line showing the point in the expected and actual
        /// arrays at which the comparison failed. If the arrays have different
        /// structures or dimensions, both values are shown.
        /// </summary>
        /// <param name="expected">The expected array</param>
        /// <param name="actual">The actual array</param>
        /// <param name="failurePoint">Index of the failure point in the underlying collections</param>
        private void DisplayFailurePoint(MessageWriter writer, ICollection expected, ICollection actual, int failurePoint, int indent)
        {
            Array expectedArray = expected as Array;
            Array actualArray = actual as Array;

            int expectedRank = expectedArray != null ? expectedArray.Rank : 1;
            int actualRank = actualArray != null ? actualArray.Rank : 1;

            bool useOneIndex = expectedRank == actualRank;

            if (expectedArray != null && actualArray != null)
                for (int r = 1; r < expectedRank && useOneIndex; r++)
                    if (expectedArray.GetLength(r) != actualArray.GetLength(r))
                        useOneIndex = false;

            int[] expectedIndices = MsgUtils.GetArrayIndicesFromCollectionIndex(expected, failurePoint);
            if (useOneIndex)
            {
                writer.WriteMessageLine(indent, Msgs.ValuesDiffer_1, MsgUtils.GetArrayIndicesAsString(expectedIndices));
            }
            else
            {
                int[] actualIndices = MsgUtils.GetArrayIndicesFromCollectionIndex(actual, failurePoint);
                writer.WriteMessageLine(indent, Msgs.ValuesDiffer_2,
                    MsgUtils.GetArrayIndicesAsString(expectedIndices), MsgUtils.GetArrayIndicesAsString(actualIndices));
            }
        }

        private void DisplayMissingElements(MessageWriter writer, ICollection expected, int failurePoint, int max)
        {
            DisplayElements(writer, Msgs.Pfx_Missing, expected, failurePoint, max);
        }

        private void DisplayExtraElements(MessageWriter writer, ICollection actual, int failurePoint, int max)
        {
            DisplayElements(writer, Msgs.Pfx_Extra, actual, failurePoint, max);
        }

        /// <summary>
        /// Displays elements from a collection on a line
        /// </summary>
        /// <param name="label">Text to prefix the line with</param>
        /// <param name="list">The list of items to display</param>
        /// <param name="index">The index in the collection of the first element to display</param>
        /// <param name="max">The maximum number of elements to display</param>
        private void DisplayElements(MessageWriter writer, string prefix, ICollection collection, int index, int max)
        {
            writer.Write(prefix + "< ");

            if (collection == null)
                writer.Write("null");
            else if (collection.Count == 0)
                writer.Write("empty");
            else
            {
                for (int i = 0; i < max && index < collection.Count; i++)
                {
                    if (i > 0) writer.Write(", ");

                    writer.WriteValue(GetValueFromCollection(collection, index++));
                }

                if (index < collection.Count)
                    writer.Write("...");
            }

            writer.Write(" >");
        }

        private static object GetValueFromCollection(ICollection collection, int index)
        {
            Array array = collection as Array;

            if (array != null && array.Rank > 1)
                return array.GetValue(MsgUtils.GetArrayIndicesFromCollectionIndex(array, index));

            if (collection is IList)
                return ((IList)collection)[index];

            foreach (object obj in collection)
                if (--index < 0)
                    return obj;

            return null;
        }
        #endregion
    }
}
