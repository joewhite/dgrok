// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Collections;

namespace NUnitLite.Constraints
{
    public class EmptyConstraint : Constraint
    {
        public override bool Matches(object actual)
        {
            this.actual = actual;

            return actual is string && (string)actual == string.Empty
                || actual is ICollection && ((ICollection)actual).Count == 0;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            //if (actual is string)
            //    writer.WriteExpected(string.Empty);
            //else
                writer.Write("<empty>");
        }
    }
}
