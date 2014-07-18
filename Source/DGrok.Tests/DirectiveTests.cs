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
using System.Text;
using DGrok.Framework;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

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

        [Test]
        public void Abstract()
        {
            Assert.That("abstract", ParsesAsSimpleDirective("AbstractSemikeyword |abstract|"));
        }
        [Test]
        public void Assembler()
        {
            Assert.That("assembler", ParsesAsSimpleDirective("AssemblerSemikeyword |assembler|"));
        }
        [Test]
        public void Cdecl()
        {
            Assert.That("cdecl", ParsesAsSimpleDirective("CdeclSemikeyword |cdecl|"));
        }
        [Test]
        public void DispIdAloneDoesNotParse()
        {
            AssertDoesNotParse("dispid");
        }
        [Test]
        public void DispIdWithValue()
        {
            Assert.That("dispid 42", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: DispIdSemikeyword |dispid|",
                "  ValueNode: Number |42|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Dynamic()
        {
            Assert.That("dynamic", ParsesAsSimpleDirective("DynamicSemikeyword |dynamic|"));
        }
        [Test]
        public void Export()
        {
            Assert.That("export", ParsesAsSimpleDirective("ExportSemikeyword |export|"));
        }
        [Test]
        public void External()
        {
            Assert.That("external", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void ExternalDll()
        {
            Assert.That("external 'Foo.dll'", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: StringLiteral |'Foo.dll'|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void ExternalIndexAndName()
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
        [Test]
        public void Far()
        {
            Assert.That("far", ParsesAsSimpleDirective("FarSemikeyword |far|"));
        }
        [Test]
        public void Final()
        {
            Assert.That("final", ParsesAsSimpleDirective("FinalSemikeyword |final|"));
        }
        [Test]
        public void Forward()
        {
            Assert.That("forward", ParsesAsSimpleDirective("ForwardSemikeyword |forward|"));
        }
        [Test]
        public void Inline()
        {
            Assert.That("inline", ParsesAsSimpleDirective("InlineKeyword |inline|"));
        }
        [Test]
        public void Local()
        {
            Assert.That("local", ParsesAsSimpleDirective("LocalSemikeyword |local|"));
        }
        [Test]
        public void MessageAloneDoesNotParse()
        {
            AssertDoesNotParse("message");
        }
        [Test]
        public void MessageWithValue()
        {
            Assert.That("message WM_ULTIMATEANSWER", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: (none)",
                "  KeywordNode: MessageSemikeyword |message|",
                "  ValueNode: Identifier |WM_ULTIMATEANSWER|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void Near()
        {
            Assert.That("near", ParsesAsSimpleDirective("NearSemikeyword |near|"));
        }
        [Test]
        public void Overload()
        {
            Assert.That("overload", ParsesAsSimpleDirective("OverloadSemikeyword |overload|"));
        }
        [Test]
        public void Override()
        {
            Assert.That("override", ParsesAsSimpleDirective("OverrideSemikeyword |override|"));
        }
        [Test]
        public void Pascal()
        {
            Assert.That("pascal", ParsesAsSimpleDirective("PascalSemikeyword |pascal|"));
        }
        [Test]
        public void Register()
        {
            Assert.That("register", ParsesAsSimpleDirective("RegisterSemikeyword |register|"));
        }
        [Test]
        public void Reintroduce()
        {
            Assert.That("reintroduce", ParsesAsSimpleDirective("ReintroduceSemikeyword |reintroduce|"));
        }
        [Test]
        public void Safecall()
        {
            Assert.That("safecall", ParsesAsSimpleDirective("SafecallSemikeyword |safecall|"));
        }
        [Test]
        public void Static()
        {
            Assert.That("static", ParsesAsSimpleDirective("StaticSemikeyword |static|"));
        }
        [Test]
        public void Stdcall()
        {
            Assert.That("stdcall", ParsesAsSimpleDirective("StdcallSemikeyword |stdcall|"));
        }
        [Test]
        public void VarArgs()
        {
            Assert.That("varargs", ParsesAsSimpleDirective("VarArgsSemikeyword |varargs|"));
        }
        [Test]
        public void Virtual()
        {
            Assert.That("virtual", ParsesAsSimpleDirective("VirtualSemikeyword |virtual|"));
        }
        [Test]
        public void Platform()
        {
            Assert.That("platform", ParsesAsSimpleDirective("PlatformSemikeyword |platform|"));
        }
        [Test]
        public void Deprecated()
        {
            Assert.That("deprecated", ParsesAsSimpleDirective("DeprecatedSemikeyword |deprecated|"));
        }
        [Test]
        public void Library()
        {
            Assert.That("library", ParsesAsSimpleDirective("LibraryKeyword |library|"));
        }
        [Test]
        public void SingleWordWithLeadingSemicolon()
        {
            Assert.That("; abstract", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: AbstractSemikeyword |abstract|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void MessageWithLeadingSemicolon()
        {
            Assert.That("; message WM_ULTIMATEANSWER", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: MessageSemikeyword |message|",
                "  ValueNode: Identifier |WM_ULTIMATEANSWER|",
                "  DataNode: ListNode"));
        }
        [Test]
        public void ExternalWithLeadingSemicolon()
        {
            Assert.That("; external", ParsesAs(
                "DirectiveNode",
                "  SemicolonNode: Semicolon |;|",
                "  KeywordNode: ExternalSemikeyword |external|",
                "  ValueNode: (none)",
                "  DataNode: ListNode"));
        }
        [Test]
        public void LookaheadRejectsLoneSemicolon()
        {
            Parser parser = CreateParser(";");
            Assert.That(parser.CanParseRule(RuleType), Is.False);
        }
    }
}
