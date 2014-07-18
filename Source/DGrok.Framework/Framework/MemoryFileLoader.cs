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
    public class MemoryFileLoader : IFileLoader
    {
        private IDictionary<string, string> _files =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public IDictionary<string, string> Files
        {
            get { return _files; }
        }

        public string ExpandFileName(string currentDirectory, string fileName)
        {
            return fileName;
        }
        public string Load(string fileName)
        {
            if (Files.ContainsKey(fileName))
                return Files[fileName];
            throw new IOException("File not found: " + fileName);
        }
    }
}
