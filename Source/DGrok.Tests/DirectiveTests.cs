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
using NUnitLite.Constraints;
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

        private Constraint ParsesAsSimpleDirective(string expectedDirective)
        {
            return ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: " + expectedDirective,
                "  ValueNode: (none)",
                "  DataNode: ListNode");
        }

        public void TestAbstract()
        {
            Assert.That("abstract", ParsesAsSimpleDirective("AbstractSemikeyword |abstract|"));
        }
        public void TestAssembler()
        {
            Assert.That("assembler", ParsesAsSimpleDirective("AssemblerSemikeyword |assembler|"));
        }
        public void TestCdecl()
        {
            Assert.That("cdecl", ParsesAsSimpleDirective("CdeclSemikeyword |cdecl|"));
        }
        public void TestDispIdAloneDoesNotParse()
        {
            AssertDoesNotParse("dispid");
        }
        public void TestDispIdWithValue()
        {
            Assert.That("dispid 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: DispIdSemikeyword |dispid|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        public void TestDynamic()
        {
            Assert.That("dynamic", ParsesAsSimpleDirective("DynamicSemikeyword |dynamic|"));
        }
        public void TestExport()
        {
            Assert.That("export", ParsesAsSimpleDirective("ExportSemikeyword |export|"));
        }
        public void TestExternal()
        {
            Assert.That("external", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestExternalDll()
        {
            Assert.That("external 'Foo.dll'", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: StringLiteral |'Foo.dll'|",
                "  DataNode: ListNode"));
        }
        public void TestExternalIndexAndName()
        {
            Assert.That("external 'Foo.dll' index 42 name 'Bar'", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: StringLiteral |'Foo.dll'|",
                "  DataNode: ListNode",
                "    Items[0]: ExportsSpecifierNode",
                "      KeywordNode: IndexSemikeyword |index|",
                "      ValueNode: Number |42|",
                "    Items[1]: ExportsSpecifierNode",
                "      KeywordNode: NameSemikeyword |name|",
                "      ValueNode: StringLiteral |'Bar'|"));
        }
        public void TestFar()
        {
            Assert.That("far", ParsesAsSimpleDirective("FarSemikeyword |far|"));
        }
        public void TestFinal()
        {
            Assert.That("final", ParsesAsSimpleDirective("FinalSemikeyword |final|"));
        }
        public void TestForward()
        {
            Assert.That("forward", ParsesAsSimpleDirective("ForwardSemikeyword |forward|"));
        }
        public void TestInline()
        {
            Assert.That("inline", ParsesAsSimpleDirective("InlineKeyword |inline|"));
        }
        public void TestLocal()
        {
            Assert.That("local", ParsesAsSimpleDirective("LocalSemikeyword |local|"));
        }
        public void TestMessageAloneDoesNotParse()
        {
            AssertDoesNotParse("message");
        }
        public void TestMessageWithValue()
        {
            Assert.That("message WM_ULTIMATEANSWER", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: MessageSemikeyword |message|",
                "  ValueNode: Identifier |WM_ULTIMATEANSWER|",
                "  DataNode: ListNode"));
        }
        public void TestNear()
        {
            Assert.That("near", ParsesAsSimpleDirective("NearSemikeyword |near|"));
        }
        public void TestOverload()
        {
            Assert.That("overload", ParsesAsSimpleDirective("OverloadSemikeyword |overload|"));
        }
        public void TestOverride()
        {
            Assert.That("override", ParsesAsSimpleDirective("OverrideSemikeyword |override|"));
        }
        public void TestPascal()
        {
            Assert.That("pascal", ParsesAsSimpleDirective("PascalSemikeyword |pascal|"));
        }
        public void TestRegister()
        {
            Assert.That("register", ParsesAsSimpleDirective("RegisterSemikeyword |register|"));
        }
        public void TestReintroduce()
        {
            Assert.That("reintroduce", ParsesAsSimpleDirective("ReintroduceSemikeyword |reintroduce|"));
        }
        public void TestSafecall()
        {
            Assert.That("safecall", ParsesAsSimpleDirective("SafecallSemikeyword |safecall|"));
        }
        public void TestStatic()
        {
            Assert.That("static", ParsesAsSimpleDirective("StaticSemikeyword |static|"));
        }
        public void TestStdcall()
        {
            Assert.That("stdcall", ParsesAsSimpleDirective("StdcallSemikeyword |stdcall|"));
        }
        public void TestVarArgs()
        {
            Assert.That("varargs", ParsesAsSimpleDirective("VarArgsSemikeyword |varargs|"));
        }
        public void TestVirtual()
        {
            Assert.That("virtual", ParsesAsSimpleDirective("VirtualSemikeyword |virtual|"));
        }
        public void TestPlatform()
        {
            Assert.That("platform", ParsesAsSimpleDirective("PlatformSemikeyword |platform|"));
        }
        public void TestDeprecated()
        {
            Assert.That("deprecated", ParsesAsSimpleDirective("DeprecatedSemikeyword |deprecated|"));
        }
        public void TestLibrary()
        {
            Assert.That("library", ParsesAsSimpleDirective("LibraryKeyword |library|"));
        }
        public void TestSingleWordWithLeadingSemicolon()
        {
            Assert.That("; abstract", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: AbstractSemikeyword |abstract|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestMessageWithLeadingSemicolon()
        {
            Assert.That("; message WM_ULTIMATEANSWER", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: MessageSemikeyword |message|",
                "  ValueNode: Identifier |WM_ULTIMATEANSWER|",
                "  DataNode: ListNode"));
        }
        public void TestExternalWithLeadingSemicolon()
        {
            Assert.That("; external", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        public void TestLookaheadRejectsLoneSemicolon()
        {
            Parser parser = CreateParser(";");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
