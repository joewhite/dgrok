// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Collections;

namespace NUnitLite.Constraints
{
    public abstract class CollectionConstraint : Constraint
    {
        protected bool IsItemInCollection(object expected, ICollection collection)
        {
            if (collection is IList)
                return ((IList)collection).Contains(expected);

            foreach (object obj in collection)
                if (obj == expected)
                    return true;

            return false;
        }

        protected bool IsSubsetOf(ICollection expected, ICollection actual)
        {
            foreach (object obj in expected)
                if (!IsItemInCollection(obj, actual))
                    return false;

            return true;
        }
    }

    public class AllItemsConstraint : Constraint
    {
        private Constraint itemConstraint;

        public AllItemsConstraint(Constraint itemConstraint)
        {
            this.itemConstraint = itemConstraint;
        }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            if ( actual == null || !(actual is ICollection) )
                return false;

            foreach(object item in (ICollection)actual)
                if (!itemConstraint.Matches(item))
                    return false;

            return true;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("all items");
            itemConstraint.WriteDescriptionTo(writer);
        }
    }

    public class CollectionContainsConstraint : CollectionConstraint
    {
        private object expected;

        public CollectionContainsConstraint(object expected)
        {
            this.expected = expected;
        }

        public override bool Matches(object actual)
        {
            return IsItemInCollection(expected, (ICollection)actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate( "collection containing " );
            writer.WriteExpectedValue(expected);
        }
    }

    public class CollectionEquivalentConstraint : CollectionConstraint
    {
        private ICollection expected;

        public CollectionEquivalentConstraint(ICollection expected)
        {
            this.expected = expected;
        }

        public override bool Matches(object actual)
        {
            return actual is ICollection &&
                IsSubsetOf((ICollection)actual, expected) &&
                IsSubsetOf(expected, (ICollection)actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("equivalent to");
            writer.WriteExpectedValue(expected);
        }
    }

    public class CollectionSubsetConstraint : CollectionConstraint
    {
        private ICollection expected;

        public CollectionSubsetConstraint(ICollection expected)
        {
            this.expected = expected;
        }

        public override bool Matches(object actual)
        {
            return IsSubsetOf( (ICollection)actual, expected );
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate( "subset of" );
            writer.WriteExpectedValue(expected);
        }
    }
}
