// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;

namespace NUnitLite
{
    /// <summary>
    /// This class the messages used by NUnitLite as well as the
    /// prefixes used on some of the message lines.
    /// </summary>
    public class Msgs
    {
        // Prefixes used in all failure messages. All must be the same
        // length, which is held in the PrefixLength field. Should not
        // contain any tabs or newline characters.
        public static readonly string Pfx_Expected = "  Expected: ";
        public static readonly string Pfx_Actual = "  But was:  ";
        public static readonly string Pfx_Missing = "  Missing:  ";
        public static readonly string Pfx_Extra = "  Extra:    ";
        public static readonly int PrefixLength = Pfx_Expected.Length;

        // EqualConstraint failure messages
        public static readonly string StringsDiffer_1 =
            "String lengths are both {0}. Strings differ at index {1}.";
        public static readonly string StringsDiffer_2 =
            "Expected string length {0} but was {1}. Strings differ at index {2}.";
        public static readonly string CollectionType_1 =
            "Expected and actual are both <{0}>";
        public static readonly string CollectionType_2 =
            "Expected is <{0}>, actual is <{1}>";
        public static readonly string ValuesDiffer_1 =
            "Values differ at index {0}";
        public static readonly string ValuesDiffer_2 =
            "Values differ at expected index {0}, actual index {1}";

        // Substring constraint failure messages
        public static readonly string DoesNotContain =
            "String did not contain expected string.";
        public static readonly string DoesNotContain_IC =
            "String did not contain expected string, ignoring case.";

        // StartsWithConstraint failure messages
        public static readonly string DoesNotStartWith =
            "String did not start with expected string.";
        public static readonly string DoesNotStartWith_IC =
            "String did not start with expected string, ignoring case.";

        // EndsWithConstraint failure messges
        public static readonly string DoesNotEndWith =
            "String did not end with expected string.";
        public static readonly string DoesNotEndWith_IC =
            "String did not end with expected string, ignoring case.";
    }
}
