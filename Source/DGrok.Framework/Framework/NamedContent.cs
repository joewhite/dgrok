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
    public class NamedContent<T>
    {
        private T _content;
        private string _fileName;

        public NamedContent(string fileName, T content)
        {
            _fileName = fileName;
            _content = content;
        }

        public T Content
        {
            get { return _content; }
        }
        public string FileName
        {
            get { return _fileName; }
        }
        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(_fileName); }
        }
    }
}
