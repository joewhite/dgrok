// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public interface IFrame
    {
        string DisplayName { get; }
        bool IsEof { get; }
        Location Location { get; }
        IFrame Next { get; set; }
        TokenType TokenType { get; }

        bool CanParseToken(TokenSet tokenSet);
        Token ParseToken(TokenSet tokenSet);
    }
}
