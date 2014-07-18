// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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
