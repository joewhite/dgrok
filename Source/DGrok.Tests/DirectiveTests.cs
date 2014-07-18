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
using NUnitLite.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class DirectiveTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Directive; }
        }

        public void TestAbstract()
        {
            Assert.That("abstract", ParsesAs("AbstractSemikeyword |abstract|"));
        }
        public void TestAssembler()
        {
            Assert.That("assembler", ParsesAs("AssemblerSemikeyword |assembler|"));
        }
        public void TestCdecl()
        {
            Assert.That("cdecl", ParsesAs("CdeclSemikeyword |cdecl|"));
        }
        public void TestDispIdAloneDoesNotParse()
        {
            AssertDoesNotParse("dispid");
        }
        public void TestDispIdWithValue()
        {
            Assert.That("dispid 42", ParsesAs(
                "ParameterizedDirectiveNode",
                "  Keyword: DispIdSemikeyword |dispid|",
                "  Value: Number |42|"));
        }
        public void TestDynamic()
        {
            Assert.That("dynamic", ParsesAs("DynamicSemikeyword |dynamic|"));
        }
        public void TestExport()
        {
            Assert.That("export", ParsesAs("ExportSemikeyword |export|"));
        }
        public void TestExternal()
        {
            Assert.That("external", ParsesAs(
                "ExternalNode",
                "  External: ExternalSemikeyword |external|",
                "  DllName: (none)",
                "  SpecifierList: ListNode"));
        }
        public void TestExternalDll()
        {
            Assert.That("external 'Foo.dll'", ParsesAs(
                "ExternalNode",
                "  External: ExternalSemikeyword |external|",
                "  DllName: StringLiteral |'Foo.dll'|",
                "  SpecifierList: ListNode"));
        }
        public void TestExternalIndexAndName()
        {
            Assert.That("external 'Foo.dll' index 42 name 'Bar'", ParsesAs(
                "ExternalNode",
                "  External: ExternalSemikeyword |external|",
                "  DllName: StringLiteral |'Foo.dll'|",
                "  SpecifierList: ListNode",
                "    Items[0]: ParameterizedDirectiveNode",
                "      Keyword: IndexSemikeyword |index|",
                "      Value: Number |42|",
                "    Items[1]: ParameterizedDirectiveNode",
                "      Keyword: NameSemikeyword |name|",
                "      Value: StringLiteral |'Bar'|"));
        }
        public void TestFar()
        {
            Assert.That("far", ParsesAs("FarSemikeyword |far|"));
        }
        public void TestFinal()
        {
            Assert.That("final", ParsesAs("FinalSemikeyword |final|"));
        }
        public void TestForward()
        {
            Assert.That("forward", ParsesAs("ForwardSemikeyword |forward|"));
        }
        public void TestInline()
        {
            Assert.That("inline", ParsesAs("InlineKeyword |inline|"));
        }
        public void TestLocal()
        {
            Assert.That("local", ParsesAs("LocalSemikeyword |local|"));
        }
        public void TestMessageAloneDoesNotParse()
        {
            AssertDoesNotParse("message");
        }
        public void TestMessageWithValue()
        {
            Assert.That("message WM_ULTIMATEANSWER", ParsesAs(
                "ParameterizedDirectiveNode",
                "  Keyword: MessageSemikeyword |message|",
                "  Value: Identifier |WM_ULTIMATEANSWER|"));
        }
        public void TestNear()
        {
            Assert.That("near", ParsesAs("NearSemikeyword |near|"));
        }
        public void TestOverload()
        {
            Assert.That("overload", ParsesAs("OverloadSemikeyword |overload|"));
        }
        public void TestOverride()
        {
            Assert.That("override", ParsesAs("OverrideSemikeyword |override|"));
        }
        public void TestPascal()
        {
            Assert.That("pascal", ParsesAs("PascalSemikeyword |pascal|"));
        }
        public void TestRegister()
        {
            Assert.That("register", ParsesAs("RegisterSemikeyword |register|"));
        }
        public void TestReintroduce()
        {
            Assert.That("reintroduce", ParsesAs("ReintroduceSemikeyword |reintroduce|"));
        }
        public void TestSafecall()
        {
            Assert.That("safecall", ParsesAs("SafecallSemikeyword |safecall|"));
        }
        public void TestStatic()
        {
            Assert.That("static", ParsesAs("StaticSemikeyword |static|"));
        }
        public void TestStdcall()
        {
            Assert.That("stdcall", ParsesAs("StdcallSemikeyword |stdcall|"));
        }
        public void TestVarArgs()
        {
            Assert.That("varargs", ParsesAs("VarArgsSemikeyword |varargs|"));
        }
        public void TestVirtual()
        {
            Assert.That("virtual", ParsesAs("VirtualSemikeyword |virtual|"));
        }
        public void TestPlatform()
        {
            Assert.That("platform", ParsesAs("PlatformSemikeyword |platform|"));
        }
        public void TestDeprecated()
        {
            Assert.That("deprecated", ParsesAs("DeprecatedSemikeyword |deprecated|"));
        }
        public void TestLibrary()
        {
            Assert.That("library", ParsesAs("LibraryKeyword |library|"));
        }
    }
}
