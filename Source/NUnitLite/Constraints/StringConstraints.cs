// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite.Constraints
{
    public abstract class StringConstraint : Constraint
    {
        protected string expected;
        protected bool ignoreCase = false;

        protected abstract void WriteFailureMessageTo(MessageWriter writer);
        protected abstract bool IsMatch(string expected, string actual );

        public StringConstraint(string expected)
        {
            this.expected = expected;
        }

        #region Modifier Properties
        public StringConstraint IgnoreCase
        {
            get
            {
                ignoreCase = true;
                return this;
            }
        }
        #endregion

        public override bool Matches(object actual)
        {
            this.actual = actual;

            if ( !(actual is string) )
                return false;

            if (ignoreCase)
                return IsMatch(expected.ToLower(), ((string)actual).ToLower());
            else
                return IsMatch(expected, (string)actual );
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            WriteFailureMessageTo(writer);
            writer.DisplayStringDifferences((string)expected, (string)actual, -1);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            //WritePredicateTo(writer);
            writer.WriteExpectedValue(expected);
            //if (ignoreCase)
            //    writer.Write(" ignoring case");
        }
    }

    public class SubstringConstraint : StringConstraint
    {
        public SubstringConstraint(string expected) : base(expected) { }

        protected override bool IsMatch(string expected, string actual)
        {
            return actual.IndexOf(expected) >= 0;
        }

        protected override void WriteFailureMessageTo(MessageWriter writer)
        {
            if (ignoreCase)
                writer.WriteMessageLine(Msgs.DoesNotContain_IC);
            else
                writer.WriteMessageLine(Msgs.DoesNotContain);
        }
    }

    public class StartsWithConstraint : StringConstraint
    {
        public StartsWithConstraint(string expected) : base(expected) { }

        protected override bool IsMatch(string expected, string actual)
        {
            return actual.StartsWith( expected );
        }

        protected override void WriteFailureMessageTo(MessageWriter writer)
        {
            if (ignoreCase)
                writer.WriteMessageLine(Msgs.DoesNotStartWith_IC);
            else
                writer.WriteMessageLine(Msgs.DoesNotStartWith);
        }
    }

    public class EndsWithConstraint : StringConstraint
    {
        public EndsWithConstraint(string expected) : base(expected) { }

        protected override bool IsMatch(string expected, string actual)
        {
            return actual.EndsWith(expected);
        }

        protected override void WriteFailureMessageTo(MessageWriter writer)
        {
            if (ignoreCase)
                writer.WriteMessageLine(Msgs.DoesNotEndWith_IC);
            else
                writer.WriteMessageLine(Msgs.DoesNotEndWith);
        }
    }
}
