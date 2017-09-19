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
using System.IO;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using DGrok.Framework;
using DGrok.Converter;

namespace DGrok.Demo
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.Run(new MainForm());

            CodeBaseOptions options = new CodeBaseOptions();
            options.LoadFromRegistry();
            options.SearchPaths = "C:\\Users\\Tuyet Anh\\Desktop\\test-parser";
            CodeBase codeBase = new CodeBase(options.CreateCompilerDefines(), new FileLoader());
            List<String> filePaths = options.ListFiles();
            foreach (var filePath in filePaths)
            {
                String text = File.ReadAllText(filePath);
                codeBase.AddFile(filePath, text);
            }

            String outputFile = "C:\\Users\\Tuyet Anh\\Desktop\\test-parser\\converted.cs";
            System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile);
            file.WriteLine(codeBase.UnitByName("aidbAccInf").ToCSharpCode());
            file.Close();
        }
    }
}
