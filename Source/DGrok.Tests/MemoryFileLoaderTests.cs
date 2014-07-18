// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DGrok.Framework;
using NUnitLite.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class MemoryFileLoaderTests
    {
        [ExpectedException(typeof(IOException))]
        public void TestIOExceptionOnMissingFile()
        {
            IFileLoader loader = new MemoryFileLoader();
            loader.Load("Foo.pas");
        }
    }
}
