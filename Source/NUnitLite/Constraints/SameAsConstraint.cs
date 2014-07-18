// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Constraints
{
    public class SameAsConstraint : Constraint
    {
        protected object expected;

        public SameAsConstraint(object expected)
        {
            this.expected = expected;
        }

        public override bool Matches(object actual)
        {
            this.actual = actual;

            return Object.ReferenceEquals(expected,actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("same as");
            writer.WriteExpectedValue(expected);
        }
    }
}
