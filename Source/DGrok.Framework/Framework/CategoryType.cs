// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DGrok.Framework
{
    public enum CategoryType
    {
        [Description("Finds problematic code constructs.")]
        BestPracticeViolations,
        [Description("Finds code that will not compile in Delphi for .NET.")]
        DotNetCompatibility,
        [Description("Finds code that will not play well with other .NET languages.")]
        DotNetInterop,
    }
}
