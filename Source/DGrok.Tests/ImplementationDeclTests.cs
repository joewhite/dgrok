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
    public class ImplementationDeclTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ImplementationDecl; }
        }

        public void TestLabelSection()
        {
            Assert.That("label 42;", ParsesAs(
                "LabelDeclSectionNode",
                "  Label: LabelKeyword |label|",
                "  LabelList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Number |42|",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestConstSection()
        {
            Assert.That("const Foo = 24; Bar = 42;", ParsesAs(
                "ConstSectionNode",
                "  Const: ConstKeyword |const|",
                "  ConstList: ListNode",
                "    Items[0]: ConstantDeclNode",
                "      Name: Identifier |Foo|",
                "      Colon: (none)",
                "      Type: (none)",
                "      EqualSign: EqualSign |=|",
                "      Value: Number |24|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "    Items[1]: ConstantDeclNode",
                "      Name: Identifier |Bar|",
                "      Colon: (none)",
                "      Type: (none)",
                "      EqualSign: EqualSign |=|",
                "      Value: Number |42|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestTypeSection()
        {
            Assert.That("type TFoo = Integer; TBar = Byte;", ParsesAs(
                "TypeSectionNode",
                "  Type: TypeKeyword |type|",
                "  TypeList: ListNode",
                "    Items[0]: TypeDeclNode",
                "      Name: Identifier |TFoo|",
                "      EqualSign: EqualSign |=|",
                "      TypeKeyword: (none)",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|",
                "    Items[1]: TypeDeclNode",
                "      Name: Identifier |TBar|",
                "      EqualSign: EqualSign |=|",
                "      TypeKeyword: (none)",
                "      Type: Identifier |Byte|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestVarSection()
        {
            Assert.That("var Foo: Integer;", ParsesAs(
                "VarSectionNode",
                "  Var: VarKeyword |var|",
                "  VarList: ListNode",
                "    Items[0]: VarDeclNode",
                "      Names: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      Absolute: (none)",
                "      AbsoluteAddress: (none)",
                "      EqualSign: (none)",
                "      Value: (none)",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestMethodImplementation()
        {
            Assert.That("procedure Foo; begin end;", ParsesAs(
                "MethodImplementationNode",
                "  MethodHeading: MethodHeadingNode",
                "    Class: (none)",
                "    MethodType: ProcedureKeyword |procedure|",
                "    Name: Identifier |Foo|",
                "    OpenParenthesis: (none)",
                "    ParameterList: ListNode",
                "    CloseParenthesis: (none)",
                "    Colon: (none)",
                "    ReturnType: (none)",
                "    DirectiveList: ListNode",
                "    Semicolon: Semicolon |;|",
                "  FancyBlock: FancyBlockNode",
                "    DeclList: ListNode",
                "    Block: BlockNode",
                "      Begin: BeginKeyword |begin|",
                "      StatementList: ListNode",
                "      End: EndKeyword |end|",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestExportsStatement()
        {
            Assert.That("exports Foo;", ParsesAs(
                "ExportsStatementNode",
                "  Exports: ExportsKeyword |exports|",
                "  ItemList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ExportsItemNode",
                "        Name: Identifier |Foo|",
                "        SpecifierList: ListNode",
                "      Delimiter: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
