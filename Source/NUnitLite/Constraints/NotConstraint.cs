// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Constraints
{
    public class NotConstraint : Constraint
    {
        Constraint baseConstraint;

        public NotConstraint(Constraint baseConstraint)
        {
            this.baseConstraint = baseConstraint;
        }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            return !baseConstraint.Matches(actual);
        }

        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "not" );
            baseConstraint.WriteDescriptionTo( writer );
        }
    }
}
