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
    public class ProcedureTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ProcedureType; }
        }

        public void TestProcedure()
        {
            Assert.That("procedure", ParsesAs(
                "ProcedureTypeNode",
                "  MethodType: ProcedureKeyword |procedure|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Of: (none)",
                "  Object: (none)"));
        }
        public void TestFunction()
        {
            Assert.That("function: Integer", ParsesAs(
                "ProcedureTypeNode",
                "  MethodType: FunctionKeyword |function|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: Colon |:|",
                "  ReturnType: Identifier |Integer|",
                "  Of: (none)",
                "  Object: (none)"));
        }
        public void TestEmptyParameterList()
        {
            Assert.That("procedure()", ParsesAs(
                "ProcedureTypeNode",
                "  MethodType: ProcedureKeyword |procedure|",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  ParameterList: ListNode",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Of: (none)",
                "  Object: (none)"));
        }
        public void TestParameters()
        {
            Assert.That("procedure(Sender: TObject; var CanClose: Boolean)", ParsesAs(
                "ProcedureTypeNode",
                "  MethodType: ProcedureKeyword |procedure|",
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
                "  Of: (none)",
                "  Object: (none)"));
        }
        public void TestOfObject()
        {
            Assert.That("procedure of object", ParsesAs(
                "ProcedureTypeNode",
                "  MethodType: ProcedureKeyword |procedure|",
                "  OpenParenthesis: (none)",
                "  ParameterList: ListNode",
                "  CloseParenthesis: (none)",
                "  Colon: (none)",
                "  ReturnType: (none)",
                "  Of: OfKeyword |of|",
                "  Object: ObjectKeyword |object|"));
        }
    }
}
