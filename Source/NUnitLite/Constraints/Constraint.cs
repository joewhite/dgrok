// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;

namespace NUnitLite.Constraints
{
    public abstract class Constraint
    {
        #region UnsetObject Class
        /// <summary>
        /// Class used to detect any derived constraints
        /// that fail to set the actual value in their
        /// Matches override.
        /// </summary>
        private class UnsetObject
        {
            public override string ToString()
            {
                return "UNSET";
            }
        }
        #endregion

        protected static object UNSET = new UnsetObject();
        protected object actual = UNSET;

        #region Public Methods
        /// <summary>
        /// Write the failure message to the MessageWriter provided
        /// as an argument. The default implementation simply passes
        /// the constraint and the actual value to the writer, which
        /// then displays the constraint description and the value.
        /// 
        /// Constraints that need to provide additional details,
        /// such as where the error occured can override this.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display the message</param>
        public virtual void WriteMessageTo(MessageWriter writer)
        {
            writer.DisplayDifferences(this, actual);
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public abstract bool Matches(object actual);

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public abstract void WriteDescriptionTo(MessageWriter writer);
        #endregion

        #region Operator Overloads
        /// <summary>
        /// This operator creates a constraint that is satisfied only if both 
        /// argument constraints are satisfied.
        /// </summary>
        public static Constraint operator &(Constraint left, Constraint right)
        {
            return new AndConstraint(left, right);
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied if either 
        /// of the argument constraints is satisfied.
        /// </summary>
        public static Constraint operator |(Constraint left, Constraint right)
        {
            return new OrConstraint(left, right);
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied if the 
        /// argument constraint is not satisfied.
        /// </summary>
        public static Constraint operator !(Constraint m)
        {
            return new NotConstraint(m == null ? new EqualConstraint(null) : m);
        }
        #endregion
    }
}
