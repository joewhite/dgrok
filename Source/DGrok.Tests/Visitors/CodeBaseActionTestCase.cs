// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
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
