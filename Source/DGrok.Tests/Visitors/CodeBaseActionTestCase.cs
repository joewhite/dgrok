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
using System.Text;
using DGrok.Framework;

namespace DGrok.Tests.Visitors
{
    public abstract class CodeBaseActionTestCase
    {
        protected virtual void AddPrefix(List<string> unit)
        {
        }
        protected virtual void AddSuffix(List<string> unit)
        {
        }
        protected abstract ICodeBaseAction CreateAction();
        protected IList<Hit> HitsFor(params string[] text)
        {
            List<string> unit = new List<string>();
            AddPrefix(unit);
            unit.AddRange(text);
            AddSuffix(unit);
            string joinedText = String.Join(Environment.NewLine, unit.ToArray());
            CodeBase codeBase = new CodeBase(CompilerDefines.CreateEmpty(), new MemoryFileLoader());
            codeBase.AddFileExpectingSuccess("Foo.pas", joinedText);
            return CreateAction().Execute(codeBase);
        }
    }
}
