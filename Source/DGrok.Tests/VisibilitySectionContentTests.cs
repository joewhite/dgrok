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
    public class VisibilitySectionContentTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.VisibilitySectionContent; }
        }

        public void TestFieldSection()
        {
            Assert.That("Foo: Integer;", ParsesAs(
                "FieldSectionNode",
                "  Class: (none)",
                "  Var: (none)",
                "  FieldList: ListNode",
                "    Items[0]: FieldDeclNode",
                "      NameList: ListNode",
                "        Items[0]: DelimitedItemNode",
                "          Item: Identifier |Foo|",
                "          Delimiter: (none)",
                "      Colon: Colon |:|",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
        public void TestMethodSection()
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
                "  DirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestConstSection()
        {
            Assert.That("const Foo = 24;", ParsesAs(
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
                "      Semicolon: Semicolon |;|"));
        }
        public void TestTypeSection()
        {
            Assert.That("type TFoo = Integer;", ParsesAs(
                "TypeSectionNode",
                "  Type: TypeKeyword |type|",
                "  TypeList: ListNode",
                "    Items[0]: TypeDeclNode",
                "      Name: Identifier |TFoo|",
                "      EqualSign: EqualSign |=|",
                "      TypeKeyword: (none)",
                "      Type: Identifier |Integer|",
                "      PortabilityDirectiveList: ListNode",
                "      Semicolon: Semicolon |;|"));
        }
    }
}
