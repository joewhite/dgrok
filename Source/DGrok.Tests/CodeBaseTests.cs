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
using DGrok.DelphiNodes;
using DGrok.Framework;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace DGrok.Tests
{
    [TestFixture]
    public class CodeBaseTests
    {
        private CodeBase _codeBase;

        [SetUp]
        public void SetUp()
        {
            _codeBase = new CodeBase(CompilerDefines.CreateEmpty(), new MemoryFileLoader());
        }

        [Test]
        public void AddError()
        {
            _codeBase.AddError(@"C:\Foo.pas", new Exception("Oops"));
            Assert.That(_codeBase.ErrorCount, Is.EqualTo(1));
        }
        [Test]
        public void AddFileThatIsValidUnit()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
            Assert.That(_codeBase.UnitCount, Is.EqualTo(1));
        }
        [Test]
        public void AddFileThatIsValidProject()
        {
            _codeBase.AddFile("Foo.dpr", "program Foo; end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0));
        }
        [Test]
        public void AddFileWithError()
        {
            _codeBase.AddFile("Foo.pas", "");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(0));
            Assert.That(_codeBase.ErrorCount, Is.EqualTo(1));
        }
        [Test]
        public void AddFileExpectingSuccessWithSuccess()
        {
            _codeBase.AddFileExpectingSuccess("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
        }
        [Test, ExpectedException(typeof(ParseException))]
        public void AddFileExpectingSuccessWithError()
        {
            _codeBase.AddFileExpectingSuccess("Foo.pas", "");
        }
        [Test]
        public void AddParsedFile()
        {
            AstNode parseTree = new Token(TokenType.EndKeyword, null, "end", "");
            _codeBase.AddParsedFile("Foo.pas", "end", parseTree);
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
        }
        [Test]
        public void ErrorByFileName()
        {
            Exception error = new Exception("Oops");
            _codeBase.AddError("Foo.pas", error);
            Assert.That(_codeBase.ErrorByFileName("Foo.pas"), Is.SameAs(error));
        }
        [Test]
        public void ParsedFileByFileNameWithUnit()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileByFileName("Foo.pas"), Is.Not.Null);
        }
        [Test]
        public void ParsedFileByFileNameWithProject()
        {
            _codeBase.AddFile("Foo.dpr", "program Foo; end.");
            Assert.That(_codeBase.ParsedFileByFileName("Foo.dpr"), Is.Not.Null);
        }
        [Test]
        public void UnitByName()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.UnitByName("Foo"), Is.Not.Null);
        }
        [Test]
        public void UnitsGoIntoUnitList()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(1), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(0), "ProjectCount");
        }
        [Test]
        public void ProgramsGoIntoProjectList()
        {
            _codeBase.AddFile("Foo.pas", "program Foo; end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(1), "ProjectCount");
        }
        [Test]
        public void LibrariesGoIntoProjectList()
        {
            _codeBase.AddFile("Foo.pas", "library Foo; end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(1), "ProjectCount");
        }
        [Test]
        public void PackagesGoIntoProjectList()
        {
            _codeBase.AddFile("Foo.pas", "package Foo; end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(1), "ProjectCount");
        }
        [Test]
        public void UnitsRememberOriginalPath()
        {
            _codeBase.AddFile(@"C:\Foo.pas", "unit Foo; interface implementation end.");
            List<NamedContent<UnitNode>> units = new List<NamedContent<UnitNode>>(_codeBase.Units);
            Assert.That(units.Count, Is.EqualTo(1));
            Assert.That(units[0].Name, Is.EqualTo("Foo"));
            Assert.That(units[0].FileName, Is.EqualTo(@"C:\Foo.pas"));
        }
        [Test]
        public void ProjectsRememberOriginalPath()
        {
            _codeBase.AddFile(@"C:\Foo.dpr", "program Foo; end.");
            List<NamedContent<AstNode>> projects = new List<NamedContent<AstNode>>(_codeBase.Projects);
            Assert.That(projects.Count, Is.EqualTo(1));
            Assert.That(projects[0].Name, Is.EqualTo("Foo"));
            Assert.That(projects[0].FileName, Is.EqualTo(@"C:\Foo.dpr"));
        }
        [Test]
        public void ErrorOnDuplicateUnitName()
        {
            _codeBase.AddFile(@"C:\Dir1\Foo.pas", "unit Foo; interface implementation end.");
            _codeBase.AddFile(@"C:\Dir2\foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1), "ParsedFileCount");
            List<NamedContent<Exception>> errors = new List<NamedContent<Exception>>(_codeBase.Errors);
            Assert.That(errors.Count, Is.EqualTo(1), "Error count");
            Assert.That(errors[0].Content, Is.InstanceOfType(typeof(DuplicateFileNameException)));
            Assert.That(errors[0].Content.Message, Is.EqualTo(
                @"File 'C:\Dir2\foo.pas' has the same name as 'C:\Dir1\Foo.pas'"));
        }
        [Test]
        public void ErrorOnDuplicateProjectName()
        {
            _codeBase.AddFile(@"C:\Dir1\Foo.dpr", "program Foo; end.");
            _codeBase.AddFile(@"C:\Dir2\foo.dpr", "library Foo; end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1), "ParsedFileCount");
            List<NamedContent<Exception>> errors = new List<NamedContent<Exception>>(_codeBase.Errors);
            Assert.That(errors.Count, Is.EqualTo(1), "Error count");
            Assert.That(errors[0].Content, Is.InstanceOfType(typeof(DuplicateFileNameException)));
            Assert.That(errors[0].Content.Message, Is.EqualTo(
                @"File 'C:\Dir2\foo.dpr' has the same name as 'C:\Dir1\Foo.dpr'"));
        }
    }
}
