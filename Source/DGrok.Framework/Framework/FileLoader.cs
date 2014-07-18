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

namespace DGrok.Framework
{
    public class FileLoader : IFileLoader
    {
        public string ExpandFileName(string currentDirectory, string fileName)
        {
            return Path.Combine(currentDirectory, fileName);
        }
        public string Load(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}
