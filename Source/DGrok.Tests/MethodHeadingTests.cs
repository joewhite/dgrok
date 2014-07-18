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
    public class MethodHeadingTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.MethodHeading; }
        }

        public void TestProcedure()
        {
            Assert.That("procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestFunctionWithoutReturnType()
        {
            Assert.That("function Foo;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: FunctionKeyword |function|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestConstructor()
        {
            Assert.That("constructor Foo;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ConstructorKeyword |constructor|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestDestructor()
        {
            Assert.That("destructor Foo; override;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: DestructorKeyword |destructor|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: OverrideSemikeyword |override|",
                "      Delimiter: Semicolon |;|"));
        }
        public void TestMultipleDirectives()
        {
            Assert.That("procedure Foo; virtual; abstract; deprecated;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: VirtualSemikeyword |virtual|",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: AbstractSemikeyword |abstract|",
                "      Delimiter: Semicolon |;|",
                "    Items[2]: DelimitedItemNode",
                "      Item: DeprecatedSemikeyword |deprecated|",
                "      Delimiter: Semicolon |;|"));
        }
        public void TestQualifiedName()
        {
            Assert.That("procedure TFoo.Bar;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: BinaryOperationNode",
                "    Left: Identifier |TFoo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |Bar|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestEmptyParameterList()
        {
            Assert.That("procedure Foo();", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestParameters()
        {
            Assert.That("procedure Foo(Sender: TObject; var CanClose: Boolean);", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ParameterNode",
                "        Modifier: (none)",
                "        Names: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: Identifier |Sender|",
                "            Delimiter: (none)",
                "        Colon: Colon |:|",
                "        Type: Identifier |TObject|",
                "        EqualSign: (none)",
                "        DefaultValue: (none)",
                "      Delimiter: Semicolon |;|",
                "    Items[1]: DelimitedItemNode",
                "      Item: ParameterNode",
                "        Modifier: VarKeyword |var|",
                "        Names: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: Identifier |CanClose|",
                "            Delimiter: (none)",
                "        Colon: Colon |:|",
                "        Type: Identifier |Boolean|",
                "        EqualSign: (none)",
                "        DefaultValue: (none)",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestReturnType()
        {
            Assert.That("function Foo: Boolean;", ParsesAs(
                "MethodHeadingNode",
                "  Class: (none)",
                "  MethodType: FunctionKeyword |function|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: Colon |:|",
                "  ReturnType: Identifier |Boolean|",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestClassProcedure()
        {
            Assert.That("class procedure Foo;", ParsesAs(
                "MethodHeadingNode",
                "  Class: ClassKeyword |class|",
                "  MethodType: ProcedureKeyword |procedure|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
        public void TestClassFunction()
        {
            Assert.That("class function Foo: Integer;", ParsesAs(
                "MethodHeadingNode",
                "  Class: ClassKeyword |class|",
                "  MethodType: FunctionKeyword |function|",
                "  Name: Identifier |Foo|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: Colon |:|",
                "  ReturnType: Identifier |Integer|",
                "  Semicolon: Semicolon |;|",
                "  DirectiveList: ListNode"));
        }
    }
}
