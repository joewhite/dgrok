// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Collections;
using NUnitLite.Constraints;

namespace NUnitLite.Framework
{
    #region Is Helper Class
    public class Is
    {
        #region Constraints Without Arguments
        public static readonly Constraint Null = new EqualConstraint( null );

        public static readonly Constraint True = new EqualConstraint(true);

        public static readonly Constraint False = new EqualConstraint(false);

        public static readonly Constraint NaN = new EqualConstraint(double.NaN);

        public static readonly Constraint Empty = new EmptyConstraint();
        #endregion

        #region Constraints with an expected value

        #region Equality and Identity
        public static EqualConstraint EqualTo(object expected)
        {
            return new EqualConstraint(expected);
        }

        public static Constraint SameAs(object expected)
        {
            return new SameAsConstraint(expected);
        }
        #endregion

        #region Comparison Constraints
        public static Constraint GreaterThan(IComparable expected)
        {
            return new GreaterThanConstraint(expected);
        }

        public static Constraint GreaterThanOrEqualTo(IComparable expected)
        {
            return new GreaterThanOrEqualConstraint(expected);
        }

        public static Constraint AtLeast(IComparable expected)
        {
            return GreaterThanOrEqualTo(expected);
        }

        public static Constraint LessThan(IComparable expected)
        {
            return new LessThanConstraint(expected);
        }

        public static Constraint LessThanOrEqualTo(IComparable expected)
        {
            return new LessThanOrEqualConstraint(expected);
        }

        public static Constraint AtMost(IComparable expected)
        {
            return LessThanOrEqualTo(expected);
        }
        #endregion

        #region Type Constraints
        public static Constraint Type(Type expectedType)
        {
            return new ExactTypeConstraint(expectedType);
        }

        public static Constraint InstanceOfType(Type expectedType)
        {
            return new InstanceOfTypeConstraint(expectedType);
        }
        #endregion

        #region String Constraints
        public static StringConstraint StringContaining(string substring)
        {
            return new SubstringConstraint(substring);
        }

        public static StringConstraint StringStarting(string substring)
        {
            return new StartsWithConstraint(substring);
        }

        public static StringConstraint StringEnding(string substring)
        {
            return new EndsWithConstraint(substring);
        }
        #endregion

        #region Collection Constraints
        public static Constraint EquivalentTo(ICollection expected)
        {
            return new CollectionEquivalentConstraint(expected);
        }

        public static Constraint SubsetOf(ICollection expected)
        {
            return new CollectionSubsetConstraint(expected);
        }
        #endregion

        #endregion

        #region Prefix Operators
        public static ConstraintBuilder Not
        {
            get { return new ConstraintBuilder().Not; }
        }

        public static ConstraintBuilder All
        {
            get { return new ConstraintBuilder().All; }
        }
        #endregion
    }
    #endregion

    #region Contains Helper Class
    public class Contains
    {
        public static Constraint Substring(string substring)
        {
            return new SubstringConstraint(substring);
        }

        public static Constraint Item(object item)
        {
            return new CollectionContainsConstraint(item);
        }
    }
    #endregion
}
