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
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace DGrok.Tests
{
    [TestFixture]
    public class LexerTests
    {
        private Constraint LexesAs(params string[] expected)
        {
            return new LexesAsConstraint(expected, delegate(IEnumerable<Token> tokens)
            {
                return tokens;
            });
        }

        // Baseline tests
        [Test]
        public void BlankSource()
        {
            Assert.That("", LexesAs());
        }
        [Test]
        public void OnlyWhitespace()
        {
            Assert.That("  \r\n  ", LexesAs());
        }
        [Test]
        public void TwoTimesSigns()
        {
            Assert.That("**", LexesAs("TimesSign |*|", "TimesSign |*|"));
        }
        [Test]
        public void LeadingWhitespaceIsIgnored()
        {
            Assert.That("  \r\n  *", LexesAs("TimesSign |*|"));
        }

        // Comment tests
        [Test]
        public void SingleLineCommentAtEof()
        {
            Assert.That("// Foo", LexesAs("SingleLineComment |// Foo|"));
        }
        [Test]
        public void SingleLineCommentFollowedByCrlf()
        {
            Assert.That("// Foo\r\n", LexesAs("SingleLineComment |// Foo|"));
        }
        [Test]
        public void SingleLineCommentFollowedByLf()
        {
            Assert.That("// Foo\n", LexesAs("SingleLineComment |// Foo|"));
        }
        [Test]
        public void TwoSingleLineComments()
        {
            Assert.That("// Foo\r\n// Bar", LexesAs(
                "SingleLineComment |// Foo|",
                "SingleLineComment |// Bar|"));
        }
        [Test]
        public void CurlyBraceComment()
        {
            Assert.That("{ Foo }", LexesAs("CurlyBraceComment |{ Foo }|"));
        }
        [Test]
        public void CurlyBraceCommentWithEmbeddedNewline()
        {
            Assert.That("{ Foo\r\n  Bar }", LexesAs("CurlyBraceComment |{ Foo\r\n  Bar }|"));
        }
        [Test]
        public void TwoCurlyBraceComments()
        {
            Assert.That("{Foo}{Bar}", LexesAs(
                "CurlyBraceComment |{Foo}|",
                "CurlyBraceComment |{Bar}|"));
        }
        [Test]
        public void ParenStarComment()
        {
            Assert.That("(* Foo *)", LexesAs("ParenStarComment |(* Foo *)|"));
        }
        [Test]
        public void ParenStarCommentWithEmbeddedNewline()
        {
            Assert.That("(* Foo\r\n   Bar *)", LexesAs("ParenStarComment |(* Foo\r\n   Bar *)|"));
        }
        [Test]
        public void TwoParenStarComments()
        {
            Assert.That("(*Foo*)(*Bar*)", LexesAs(
                "ParenStarComment |(*Foo*)|",
                "ParenStarComment |(*Bar*)|"));
        }
        // Compiler-directive tests
        [Test]
        public void CurlyBraceCompilerDirective()
        {
            Assert.That("{$DEFINE FOO}", LexesAs(
                "CompilerDirective |{$DEFINE FOO}|, parsed=|DEFINE FOO|"));
        }
        [Test]
        public void CurlyBraceCompilerDirectiveTrimsTrailing()
        {
            Assert.That("{$DEFINE FOO }", LexesAs(
                "CompilerDirective |{$DEFINE FOO }|, parsed=|DEFINE FOO|"));
        }
        [Test]
        public void ParenStarCompilerDirective()
        {
            Assert.That("(*$DEFINE FOO*)", LexesAs(
                "CompilerDirective |(*$DEFINE FOO*)|, parsed=|DEFINE FOO|"));
        }
        [Test]
        public void ParenStarCompilerDirectiveTrimsTrailing()
        {
            Assert.That("(*$DEFINE FOO *)", LexesAs(
                "CompilerDirective |(*$DEFINE FOO *)|, parsed=|DEFINE FOO|"));
        }

        // Number tests
        [Test]
        public void Digit()
        {
            Assert.That("0", LexesAs("Number |0|"));
        }
        [Test]
        public void Integer()
        {
            Assert.That("42", LexesAs("Number |42|"));
        }
        [Test]
        public void Float()
        {
            Assert.That("42.42", LexesAs("Number |42.42|"));
        }
        [Test]
        public void FloatWithNoDigitsAfterDecimalPoint()
        {
            Assert.That("42.", LexesAs("Number |42.|"));
        }
        [Test]
        public void ScientificNotation()
        {
            Assert.That("42e42", LexesAs("Number |42e42|"));
        }
        [Test]
        public void ScientificNotationWithCapitalLetter()
        {
            Assert.That("42E42", LexesAs("Number |42E42|"));
        }
        [Test]
        public void NegativeExponent()
        {
            Assert.That("42e-42", LexesAs("Number |42e-42|"));
        }
        [Test]
        public void ExplicitlyPositiveExponent()
        {
            Assert.That("42e+42", LexesAs("Number |42e+42|"));
        }
        [Test]
        public void ExplicitlyPositiveNumberLexesAsUnaryOperator()
        {
            Assert.That("+42", LexesAs("PlusSign |+|", "Number |42|"));
        }
        [Test]
        public void NegativeNumberLexesAsUnaryOperator()
        {
            Assert.That("-42", LexesAs("MinusSign |-|", "Number |42|"));
        }
        [Test]
        public void Hex()
        {
            Assert.That("$2A", LexesAs("Number |$2A|"));
        }

        // String literal tests
        [Test]
        public void EmptyString()
        {
            Assert.That("''", LexesAs("StringLiteral |''|"));
        }
        [Test]
        public void SimpleString()
        {
            Assert.That("'abc'", LexesAs("StringLiteral |'abc'|"));
        }
        [Test]
        public void StringWithEmbeddedApostrophe()
        {
            Assert.That("'Bob''s'", LexesAs("StringLiteral |'Bob''s'|"));
        }
        [Test]
        public void Character()
        {
            Assert.That("#32", LexesAs("StringLiteral |#32|"));
        }
        [Test]
        public void HexCharacter()
        {
            Assert.That("#$1A", LexesAs("StringLiteral |#$1A|"));
        }
        [Test]
        public void Mixed()
        {
            Assert.That("'Foo'#13#10'Bar'", LexesAs("StringLiteral |'Foo'#13#10'Bar'|"));
        }
        [Test]
        public void DoubleQuotedCharacter()
        {
            // This is valid only in asm blocks, but valid nonetheless.
            Assert.That("\"'\"", LexesAs("StringLiteral |\"'\"|"));
        }

        // Identifier tests
        [Test]
        public void Identifier()
        {
            Assert.That("Foo", LexesAs("Identifier |Foo|"));
        }
        [Test]
        public void LeadingUnderscore()
        {
            Assert.That("_Foo", LexesAs("Identifier |_Foo|"));
        }
        [Test]
        public void EmbeddedUnderscore()
        {
            Assert.That("Foo_Bar", LexesAs("Identifier |Foo_Bar|"));
        }
        [Test]
        public void EmbeddedDigits()
        {
            Assert.That("Foo42", LexesAs("Identifier |Foo42|"));
        }
        [Test]
        public void AmpersandIdentifier()
        {
            Assert.That("&Foo", LexesAs("Identifier |&Foo|"));
        }
        [Test]
        public void AmpersandSemikeyword()
        {
            Assert.That("&Absolute", LexesAs("Identifier |&Absolute|"));
        }
        [Test]
        public void AmpersandKeyword()
        {
            Assert.That("&And", LexesAs("Identifier |&And|"));
        }

        // Keyword tests
        #region Semikeyword tests
        [Test]
        public void SemikeywordsAreCaseInsensitive()
        {
            Assert.That("Absolute", LexesAs("AbsoluteSemikeyword |Absolute|"));
        }
        [Test]
        public void AbsoluteSemikeyword()
        {
            Assert.That("absolute", LexesAs("AbsoluteSemikeyword |absolute|"));
        }
        [Test]
        public void AbstractSemikeyword()
        {
            Assert.That("abstract", LexesAs("AbstractSemikeyword |abstract|"));
        }
        [Test]
        public void AssemblerSemikeyword()
        {
            Assert.That("assembler", LexesAs("AssemblerSemikeyword |assembler|"));
        }
        [Test]
        public void AssemblySemikeyword()
        {
            Assert.That("assembly", LexesAs("AssemblySemikeyword |assembly|"));
        }
        [Test]
        public void AtSemikeyword()
        {
            Assert.That("at", LexesAs("AtSemikeyword |at|"));
        }
        [Test]
        public void AutomatedSemikeyword()
        {
            Assert.That("automated", LexesAs("AutomatedSemikeyword |automated|"));
        }
        [Test]
        public void CdeclSemikeyword()
        {
            Assert.That("cdecl", LexesAs("CdeclSemikeyword |cdecl|"));
        }
        [Test]
        public void ContainsSemikeyword()
        {
            Assert.That("contains", LexesAs("ContainsSemikeyword |contains|"));
        }
        [Test]
        public void DefaultSemikeyword()
        {
            Assert.That("default", LexesAs("DefaultSemikeyword |default|"));
        }
        [Test]
        public void DeprecatedSemikeyword()
        {
            Assert.That("deprecated", LexesAs("DeprecatedSemikeyword |deprecated|"));
        }
        [Test]
        public void DispIdSemikeyword()
        {
            Assert.That("dispid", LexesAs("DispIdSemikeyword |dispid|"));
        }
        [Test]
        public void DynamicSemikeyword()
        {
            Assert.That("dynamic", LexesAs("DynamicSemikeyword |dynamic|"));
        }
        [Test]
        public void ExportSemikeyword()
        {
            Assert.That("export", LexesAs("ExportSemikeyword |export|"));
        }
        [Test]
        public void ExternalSemikeyword()
        {
            Assert.That("external", LexesAs("ExternalSemikeyword |external|"));
        }
        [Test]
        public void FarSemikeyword()
        {
            Assert.That("far", LexesAs("FarSemikeyword |far|"));
        }
        [Test]
        public void FinalSemikeyword()
        {
            Assert.That("final", LexesAs("FinalSemikeyword |final|"));
        }
        [Test]
        public void ForwardSemikeyword()
        {
            Assert.That("forward", LexesAs("ForwardSemikeyword |forward|"));
        }
        [Test]
        public void HelperSemikeyword()
        {
            Assert.That("helper", LexesAs("HelperSemikeyword |helper|"));
        }
        [Test]
        public void ImplementsSemikeyword()
        {
            Assert.That("implements", LexesAs("ImplementsSemikeyword |implements|"));
        }
        [Test]
        public void IndexSemikeyword()
        {
            Assert.That("index", LexesAs("IndexSemikeyword |index|"));
        }
        [Test]
        public void LocalSemikeyword()
        {
            Assert.That("local", LexesAs("LocalSemikeyword |local|"));
        }
        [Test]
        public void MessageSemikeyword()
        {
            Assert.That("message", LexesAs("MessageSemikeyword |message|"));
        }
        [Test]
        public void NameSemikeyword()
        {
            Assert.That("name", LexesAs("NameSemikeyword |name|"));
        }
        [Test]
        public void NearSemikeyword()
        {
            Assert.That("near", LexesAs("NearSemikeyword |near|"));
        }
        [Test]
        public void NoDefaultSemikeyword()
        {
            Assert.That("nodefault", LexesAs("NoDefaultSemikeyword |nodefault|"));
        }
        [Test]
        public void OnSemikeyword()
        {
            Assert.That("on", LexesAs("OnSemikeyword |on|"));
        }
        [Test]
        public void OperatorSemikeyword()
        {
            Assert.That("operator", LexesAs("OperatorSemikeyword |operator|"));
        }
        [Test]
        public void OutSemikeyword()
        {
            Assert.That("out", LexesAs("OutSemikeyword |out|"));
        }
        [Test]
        public void OverloadSemikeyword()
        {
            Assert.That("overload", LexesAs("OverloadSemikeyword |overload|"));
        }
        [Test]
        public void OverrideSemikeyword()
        {
            Assert.That("override", LexesAs("OverrideSemikeyword |override|"));
        }
        [Test]
        public void PackageSemikeyword()
        {
            Assert.That("package", LexesAs("PackageSemikeyword |package|"));
        }
        [Test]
        public void PascalSemikeyword()
        {
            Assert.That("pascal", LexesAs("PascalSemikeyword |pascal|"));
        }
        [Test]
        public void PlatformSemikeyword()
        {
            Assert.That("platform", LexesAs("PlatformSemikeyword |platform|"));
        }
        [Test]
        public void PrivateSemikeyword()
        {
            Assert.That("private", LexesAs("PrivateSemikeyword |private|"));
        }
        [Test]
        public void ProtectedSemikeyword()
        {
            Assert.That("protected", LexesAs("ProtectedSemikeyword |protected|"));
        }
        [Test]
        public void PublicSemikeyword()
        {
            Assert.That("public", LexesAs("PublicSemikeyword |public|"));
        }
        [Test]
        public void PublishedSemikeyword()
        {
            Assert.That("published", LexesAs("PublishedSemikeyword |published|"));
        }
        [Test]
        public void ReadSemikeyword()
        {
            Assert.That("read", LexesAs("ReadSemikeyword |read|"));
        }
        [Test]
        public void ReadOnlySemikeyword()
        {
            Assert.That("readonly", LexesAs("ReadOnlySemikeyword |readonly|"));
        }
        [Test]
        public void RegisterSemikeyword()
        {
            Assert.That("register", LexesAs("RegisterSemikeyword |register|"));
        }
        [Test]
        public void ReintroduceSemikeyword()
        {
            Assert.That("reintroduce", LexesAs("ReintroduceSemikeyword |reintroduce|"));
        }
        [Test]
        public void RequiresSemikeyword()
        {
            Assert.That("requires", LexesAs("RequiresSemikeyword |requires|"));
        }
        [Test]
        public void ResidentSemikeyword()
        {
            Assert.That("resident", LexesAs("ResidentSemikeyword |resident|"));
        }
        [Test]
        public void SafecallSemikeyword()
        {
            Assert.That("safecall", LexesAs("SafecallSemikeyword |safecall|"));
        }
        [Test]
        public void SealedSemikeyword()
        {
            Assert.That("sealed", LexesAs("SealedSemikeyword |sealed|"));
        }
        [Test]
        public void StdcallSemikeyword()
        {
            Assert.That("stdcall", LexesAs("StdcallSemikeyword |stdcall|"));
        }
        [Test]
        public void StoredSemikeyword()
        {
            Assert.That("stored", LexesAs("StoredSemikeyword |stored|"));
        }
        [Test]
        public void StrictSemikeyword()
        {
            Assert.That("strict", LexesAs("StrictSemikeyword |strict|"));
        }
        [Test]
        public void VarArgsSemikeyword()
        {
            Assert.That("varargs", LexesAs("VarArgsSemikeyword |varargs|"));
        }
        [Test]
        public void VirtualSemikeyword()
        {
            Assert.That("virtual", LexesAs("VirtualSemikeyword |virtual|"));
        }
        [Test]
        public void WriteSemikeyword()
        {
            Assert.That("write", LexesAs("WriteSemikeyword |write|"));
        }
        [Test]
        public void WriteOnlySemikeyword()
        {
            Assert.That("writeonly", LexesAs("WriteOnlySemikeyword |writeonly|"));
        }
        #endregion
        #region Keyword tests
        [Test]
        public void KeywordsAreCaseInsensitive()
        {
            Assert.That("And", LexesAs("AndKeyword |And|"));
        }
        [Test]
        public void AndKeyword()
        {
            Assert.That("and", LexesAs("AndKeyword |and|"));
        }
        [Test]
        public void ArrayKeyword()
        {
            Assert.That("array", LexesAs("ArrayKeyword |array|"));
        }
        [Test]
        public void AsKeyword()
        {
            Assert.That("as", LexesAs("AsKeyword |as|"));
        }
        [Test]
        public void AsmKeyword()
        {
            Assert.That("asm", LexesAs("AsmKeyword |asm|"));
        }
        [Test]
        public void BeginKeyword()
        {
            Assert.That("begin", LexesAs("BeginKeyword |begin|"));
        }
        [Test]
        public void CaseKeyword()
        {
            Assert.That("case", LexesAs("CaseKeyword |case|"));
        }
        [Test]
        public void ClassKeyword()
        {
            Assert.That("class", LexesAs("ClassKeyword |class|"));
        }
        [Test]
        public void ConstKeyword()
        {
            Assert.That("const", LexesAs("ConstKeyword |const|"));
        }
        [Test]
        public void ConstructorKeyword()
        {
            Assert.That("constructor", LexesAs("ConstructorKeyword |constructor|"));
        }
        [Test]
        public void DestructorKeyword()
        {
            Assert.That("destructor", LexesAs("DestructorKeyword |destructor|"));
        }
        [Test]
        public void DispInterfaceKeyword()
        {
            Assert.That("dispinterface", LexesAs("DispInterfaceKeyword |dispinterface|"));
        }
        [Test]
        public void DivKeyword()
        {
            Assert.That("div", LexesAs("DivKeyword |div|"));
        }
        [Test]
        public void DoKeyword()
        {
            Assert.That("do", LexesAs("DoKeyword |do|"));
        }
        [Test]
        public void DownToKeyword()
        {
            Assert.That("downto", LexesAs("DownToKeyword |downto|"));
        }
        [Test]
        public void ElseKeyword()
        {
            Assert.That("else", LexesAs("ElseKeyword |else|"));
        }
        [Test]
        public void EndKeyword()
        {
            Assert.That("end", LexesAs("EndKeyword |end|"));
        }
        [Test]
        public void ExceptKeyword()
        {
            Assert.That("except", LexesAs("ExceptKeyword |except|"));
        }
        [Test]
        public void ExportsKeyword()
        {
            Assert.That("exports", LexesAs("ExportsKeyword |exports|"));
        }
        [Test]
        public void FileKeyword()
        {
            Assert.That("file", LexesAs("FileKeyword |file|"));
        }
        [Test]
        public void FinalizationKeyword()
        {
            Assert.That("finalization", LexesAs("FinalizationKeyword |finalization|"));
        }
        [Test]
        public void FinallyKeyword()
        {
            Assert.That("finally", LexesAs("FinallyKeyword |finally|"));
        }
        [Test]
        public void ForKeyword()
        {
            Assert.That("for", LexesAs("ForKeyword |for|"));
        }
        [Test]
        public void FunctionKeyword()
        {
            Assert.That("function", LexesAs("FunctionKeyword |function|"));
        }
        [Test]
        public void GotoKeyword()
        {
            Assert.That("goto", LexesAs("GotoKeyword |goto|"));
        }
        [Test]
        public void IfKeyword()
        {
            Assert.That("if", LexesAs("IfKeyword |if|"));
        }
        [Test]
        public void ImplementationKeyword()
        {
            Assert.That("implementation", LexesAs("ImplementationKeyword |implementation|"));
        }
        [Test]
        public void InKeyword()
        {
            Assert.That("in", LexesAs("InKeyword |in|"));
        }
        [Test]
        public void InheritedKeyword()
        {
            Assert.That("inherited", LexesAs("InheritedKeyword |inherited|"));
        }
        [Test]
        public void InitializationKeyword()
        {
            Assert.That("initialization", LexesAs("InitializationKeyword |initialization|"));
        }
        [Test]
        public void InlineKeyword()
        {
            Assert.That("inline", LexesAs("InlineKeyword |inline|"));
        }
        [Test]
        public void InterfaceKeyword()
        {
            Assert.That("interface", LexesAs("InterfaceKeyword |interface|"));
        }
        [Test]
        public void IsKeyword()
        {
            Assert.That("is", LexesAs("IsKeyword |is|"));
        }
        [Test]
        public void LabelKeyword()
        {
            Assert.That("label", LexesAs("LabelKeyword |label|"));
        }
        [Test]
        public void LibraryKeyword()
        {
            Assert.That("library", LexesAs("LibraryKeyword |library|"));
        }
        [Test]
        public void ModKeyword()
        {
            Assert.That("mod", LexesAs("ModKeyword |mod|"));
        }
        [Test]
        public void NilKeyword()
        {
            Assert.That("nil", LexesAs("NilKeyword |nil|"));
        }
        [Test]
        public void NotKeyword()
        {
            Assert.That("not", LexesAs("NotKeyword |not|"));
        }
        [Test]
        public void ObjectKeyword()
        {
            Assert.That("object", LexesAs("ObjectKeyword |object|"));
        }
        [Test]
        public void OfKeyword()
        {
            Assert.That("of", LexesAs("OfKeyword |of|"));
        }
        [Test]
        public void OrKeyword()
        {
            Assert.That("or", LexesAs("OrKeyword |or|"));
        }
        [Test]
        public void PackedKeyword()
        {
            Assert.That("packed", LexesAs("PackedKeyword |packed|"));
        }
        [Test]
        public void ProcedureKeyword()
        {
            Assert.That("procedure", LexesAs("ProcedureKeyword |procedure|"));
        }
        [Test]
        public void ProgramKeyword()
        {
            Assert.That("program", LexesAs("ProgramKeyword |program|"));
        }
        [Test]
        public void PropertyKeyword()
        {
            Assert.That("property", LexesAs("PropertyKeyword |property|"));
        }
        [Test]
        public void RaiseKeyword()
        {
            Assert.That("raise", LexesAs("RaiseKeyword |raise|"));
        }
        [Test]
        public void RecordKeyword()
        {
            Assert.That("record", LexesAs("RecordKeyword |record|"));
        }
        [Test]
        public void RepeatKeyword()
        {
            Assert.That("repeat", LexesAs("RepeatKeyword |repeat|"));
        }
        [Test]
        public void ResourceStringKeyword()
        {
            Assert.That("resourcestring", LexesAs("ResourceStringKeyword |resourcestring|"));
        }
        [Test]
        public void SetKeyword()
        {
            Assert.That("set", LexesAs("SetKeyword |set|"));
        }
        [Test]
        public void ShlKeyword()
        {
            Assert.That("shl", LexesAs("ShlKeyword |shl|"));
        }
        [Test]
        public void ShrKeyword()
        {
            Assert.That("shr", LexesAs("ShrKeyword |shr|"));
        }
        [Test]
        public void StringKeyword()
        {
            Assert.That("string", LexesAs("StringKeyword |string|"));
        }
        [Test]
        public void ThenKeyword()
        {
            Assert.That("then", LexesAs("ThenKeyword |then|"));
        }
        [Test]
        public void ThreadVarKeyword()
        {
            Assert.That("threadvar", LexesAs("ThreadVarKeyword |threadvar|"));
        }
        [Test]
        public void ToKeyword()
        {
            Assert.That("to", LexesAs("ToKeyword |to|"));
        }
        [Test]
        public void TryKeyword()
        {
            Assert.That("try", LexesAs("TryKeyword |try|"));
        }
        [Test]
        public void TypeKeyword()
        {
            Assert.That("type", LexesAs("TypeKeyword |type|"));
        }
        [Test]
        public void UnitKeyword()
        {
            Assert.That("unit", LexesAs("UnitKeyword |unit|"));
        }
        [Test]
        public void UntilKeyword()
        {
            Assert.That("until", LexesAs("UntilKeyword |until|"));
        }
        [Test]
        public void UsesKeyword()
        {
            Assert.That("uses", LexesAs("UsesKeyword |uses|"));
        }
        [Test]
        public void VarKeyword()
        {
            Assert.That("var", LexesAs("VarKeyword |var|"));
        }
        [Test]
        public void WhileKeyword()
        {
            Assert.That("while", LexesAs("WhileKeyword |while|"));
        }
        [Test]
        public void WithKeyword()
        {
            Assert.That("with", LexesAs("WithKeyword |with|"));
        }
        [Test]
        public void XorKeyword()
        {
            Assert.That("xor", LexesAs("XorKeyword |xor|"));
        }
        #endregion

        // Equality / inequality / assignment tests
        [Test]
        public void ColonEquals()
        {
            Assert.That(":=", LexesAs("ColonEquals |:=|"));
        }
        [Test]
        public void EqualSign()
        {
            Assert.That("=", LexesAs("EqualSign |=|"));
        }
        [Test]
        public void GreaterThan()
        {
            Assert.That(">", LexesAs("GreaterThan |>|"));
        }
        [Test]
        public void LessThan()
        {
            Assert.That("<", LexesAs("LessThan |<|"));
        }
        [Test]
        public void LessOrEqual()
        {
            Assert.That("<=", LexesAs("LessOrEqual |<=|"));
        }
        [Test]
        public void GreaterOrEqual()
        {
            Assert.That(">=", LexesAs("GreaterOrEqual |>=|"));
        }
        [Test]
        public void NotEqual()
        {
            Assert.That("<>", LexesAs("NotEqual |<>|"));
        }

        // Punctuation tests
        [Test]
        public void AtSign()
        {
            Assert.That("@", LexesAs("AtSign |@|"));
        }
        [Test]
        public void Caret()
        {
            Assert.That("^", LexesAs("Caret |^|"));
        }
        [Test]
        public void CloseBracket()
        {
            Assert.That("]", LexesAs("CloseBracket |]|"));
        }
        [Test]
        public void CloseParenthesis()
        {
            Assert.That(")", LexesAs("CloseParenthesis |)|"));
        }
        [Test]
        public void Colon()
        {
            Assert.That(":", LexesAs("Colon |:|"));
        }
        [Test]
        public void Comma()
        {
            Assert.That(",", LexesAs("Comma |,|"));
        }
        [Test]
        public void DivideBySign()
        {
            Assert.That("/", LexesAs("DivideBySign |/|"));
        }
        [Test]
        public void Dot()
        {
            Assert.That(".", LexesAs("Dot |.|"));
        }
        [Test]
        public void DotDot()
        {
            Assert.That("..", LexesAs("DotDot |..|"));
        }
        [Test]
        public void MinusSign()
        {
            Assert.That("-", LexesAs("MinusSign |-|"));
        }
        [Test]
        public void OpenBracket()
        {
            Assert.That("[", LexesAs("OpenBracket |[|"));
        }
        [Test]
        public void OpenParenthesis()
        {
            Assert.That("(", LexesAs("OpenParenthesis |(|"));
        }
        [Test]
        public void PlusSign()
        {
            Assert.That("+", LexesAs("PlusSign |+|"));
        }
        [Test]
        public void Semicolon()
        {
            Assert.That(";", LexesAs("Semicolon |;|"));
        }
        [Test]
        public void TimesSign()
        {
            Assert.That("*", LexesAs("TimesSign |*|"));
        }
    }
}
