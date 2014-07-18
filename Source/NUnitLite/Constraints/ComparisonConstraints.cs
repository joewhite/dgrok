// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Constraints
{
    public abstract class ComparisonConstraint : Constraint
    {
        protected IComparable expected;
        protected bool ltOK = false;
        protected bool eqOK = false;
        protected bool gtOK = false;
        private string predicate;

        public ComparisonConstraint(IComparable value, bool ltOK, bool eqOK, bool gtOK, string predicate)
        {
            this.expected = value;
            this.ltOK = ltOK;
            this.eqOK = eqOK;
            this.gtOK = gtOK;
            this.predicate = predicate;
        }

        public override bool Matches(object actual)
        {
            this.actual = actual;

            if ( actual == null || actual.GetType() != expected.GetType() )
                return false;

            int icomp = expected.CompareTo(actual); // Reverse of stated order
            return icomp < 0 && gtOK || icomp == 0 && eqOK || icomp > 0 && ltOK;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate(predicate);
            writer.WriteExpectedValue(expected);
        }
    }

    public class GreaterThanConstraint : ComparisonConstraint
    {
        public GreaterThanConstraint(IComparable expected) : base(expected, false, false, true, "greater than") { }
    }

    public class GreaterThanOrEqualConstraint : ComparisonConstraint
    {
        public GreaterThanOrEqualConstraint(IComparable expected) : base(expected, false, true, true, "greater than or equal to") { }
    }

    public class LessThanConstraint : ComparisonConstraint
    {
        public LessThanConstraint(IComparable expected) : base(expected, true, false, false, "less than") { }
    }
    public class LessThanOrEqualConstraint : ComparisonConstraint
    {
        public LessThanOrEqualConstraint(IComparable expected) : base(expected, true, true, false, "less than or equal to") { }
    }
}
