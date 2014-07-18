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
    public class DGrokException : Exception
    {
        private int _offset;

        public DGrokException(string message, int offset)
            : base(message)
        {
            _offset = offset;
        }

        public int Offset
        {
            get { return _offset; }
        }
    }
}
