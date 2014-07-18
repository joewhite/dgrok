// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DGrok.Framework
{
    public class Location
    {
        private string _fileName;
        private string _fileSource;
        private int _offset;

        public Location(string fileName, string fileSource, int offset)
        {
            _fileName = fileName;
            _fileSource = fileSource;
            _offset = offset;
        }

        public string Directory
        {
            get
            {
                if (String.IsNullOrEmpty(_fileName))
                    return "";
                return Path.GetDirectoryName(_fileName);
            }
        }
        public string FileName
        {
            get { return _fileName; }
        }
        public string FileSource
        {
            get { return _fileSource; }
        }
        public int Offset
        {
            get { return _offset; }
        }
    }
}
