// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.DelphiNodes;
using DGrok.Framework;
using NUnitLite.Framework;

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

        public void TestAddError()
        {
            _codeBase.AddError(@"C:\Foo.pas", new Exception("Oops"));
            Assert.That(_codeBase.ErrorCount, Is.EqualTo(1));
        }
        public void TestAddFileThatIsValidUnit()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
            Assert.That(_codeBase.UnitCount, Is.EqualTo(1));
        }
        public void TestAddFileThatIsValidProject()
        {
            _codeBase.AddFile("Foo.dpr", "program Foo; end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0));
        }
        public void TestAddFileWithError()
        {
            _codeBase.AddFile("Foo.pas", "");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(0));
            Assert.That(_codeBase.ErrorCount, Is.EqualTo(1));
        }
        public void TestAddFileExpectingSuccessWithSuccess()
        {
            _codeBase.AddFileExpectingSuccess("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
        }
        [ExpectedException(typeof(ParseException))]
        public void TestAddFileExpectingSuccessWithError()
        {
            _codeBase.AddFileExpectingSuccess("Foo.pas", "");
        }
        public void TestAddParsedFile()
        {
            AstNode parseTree = new Token(TokenType.EndKeyword, null, "end", "");
            _codeBase.AddParsedFile("Foo.pas", "end", parseTree);
            Assert.That(_codeBase.ParsedFileCount, Is.EqualTo(1));
        }
        public void TestErrorByFileName()
        {
            Exception error = new Exception("Oops");
            _codeBase.AddError("Foo.pas", error);
            Assert.That(_codeBase.ErrorByFileName("Foo.pas"), Is.SameAs(error));
        }
        public void TestParsedFileByFileNameWithUnit()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.ParsedFileByFileName("Foo.pas"), Is.Not.Null);
        }
        public void TestParsedFileByFileNameWithProject()
        {
            _codeBase.AddFile("Foo.dpr", "program Foo; end.");
            Assert.That(_codeBase.ParsedFileByFileName("Foo.dpr"), Is.Not.Null);
        }
        public void TestUnitByName()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.UnitByName("Foo"), Is.Not.Null);
        }
        public void TestUnitsGoIntoUnitList()
        {
            _codeBase.AddFile("Foo.pas", "unit Foo; interface implementation end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(1), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(0), "ProjectCount");
        }
        public void TestProgramsGoIntoProjectList()
        {
            _codeBase.AddFile("Foo.pas", "program Foo; end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(1), "ProjectCount");
        }
        public void TestLibrariesGoIntoProjectList()
        {
            _codeBase.AddFile("Foo.pas", "library Foo; end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(1), "ProjectCount");
        }
        public void TestPackagesGoIntoProjectList()
        {
            _codeBase.AddFile("Foo.pas", "package Foo; end.");
            Assert.That(_codeBase.UnitCount, Is.EqualTo(0), "UnitCount");
            Assert.That(_codeBase.ProjectCount, Is.EqualTo(1), "ProjectCount");
        }
        public void TestUnitsRememberOriginalPath()
        {
            _codeBase.AddFile(@"C:\Foo.pas", "unit Foo; interface implementation end.");
            List<NamedContent<UnitNode>> units = new List<NamedContent<UnitNode>>(_codeBase.Units);
            Assert.That(units.Count, Is.EqualTo(1));
            Assert.That(units[0].Name, Is.EqualTo("Foo"));
            Assert.That(units[0].FileName, Is.EqualTo(@"C:\Foo.pas"));
        }
        public void TestProjectsRememberOriginalPath()
        {
            _codeBase.AddFile(@"C:\Foo.dpr", "program Foo; end.");
            List<NamedContent<AstNode>> projects = new List<NamedContent<AstNode>>(_codeBase.Projects);
            Assert.That(projects.Count, Is.EqualTo(1));
            Assert.That(projects[0].Name, Is.EqualTo("Foo"));
            Assert.That(projects[0].FileName, Is.EqualTo(@"C:\Foo.dpr"));
        }
        public void TestErrorOnDuplicateUnitName()
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
        public void TestErrorOnDuplicateProjectName()
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
