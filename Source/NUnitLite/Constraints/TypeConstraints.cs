// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Constraints
{
    public abstract class TypeConstraint : Constraint
    {
        protected Type expectedType;

        public TypeConstraint(Type type)
        {
            this.expectedType = type;
        }
    }

    public class ExactTypeConstraint : TypeConstraint
    {
        public ExactTypeConstraint(Type type) : base( type ) { }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            return actual != null && actual.GetType() == this.expectedType;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteExpectedValue(expectedType);
        }
    }

    public class InstanceOfTypeConstraint : TypeConstraint
    {
        public InstanceOfTypeConstraint(Type type) : base(type) { }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            return actual != null && expectedType.IsInstanceOfType(actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("instance of");
            writer.WriteExpectedValue(expectedType);
        }
    }
}
