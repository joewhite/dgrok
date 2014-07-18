// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Constraints
{
    public abstract class BinaryOperation : Constraint
    {
        protected Constraint left;
        protected Constraint right;

        public BinaryOperation(Constraint left, Constraint right)
        {
            this.left = left;
            this.right = right;
        }
    }

    public class AndConstraint : BinaryOperation
    {
        public AndConstraint(Constraint left, Constraint right) : base(left, right) { }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            return left.Matches(actual) && right.Matches(actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            left.WriteDescriptionTo(writer);
            writer.WriteConnector("and");
            right.WriteDescriptionTo(writer);
        }
    }

    public class OrConstraint : BinaryOperation
    {
        public OrConstraint(Constraint left, Constraint right) : base(left, right) { }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            return left.Matches(actual) || right.Matches(actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            left.WriteDescriptionTo(writer);
            writer.WriteConnector("or");
            right.WriteDescriptionTo(writer);
        }
    }
}
