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
    public class ClassTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ClassType; }
        }

        public void TestEmptyClass()
        {
            Assert.That("class end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: (none)",
                "  OpenParenthesis: (none)",
                "  InheritanceList: ListNode",
                "  CloseParenthesis: (none)",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestContents()
        {
            Assert.That("class procedure Foo; end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: (none)",
                "  OpenParenthesis: (none)",
                "  InheritanceList: ListNode",
                "  CloseParenthesis: (none)",
                "  Contents: ListNode",
                "    Items[0]: VisibilitySectionNode",
                "      Visibility: (none)",
                "      Contents: ListNode",
                "        Items[0]: MethodHeadingNode",
                "          Class: (none)",
                "          MethodType: ProcedureKeyword |procedure|",
                "          Name: Identifier |Foo|",
                "          OpenParenthesis: (none)",
                "          ParameterList: ListNode",
                "          CloseParenthesis: (none)",
                "          Colon: (none)",
                "          ReturnType: (none)",
                "          DirectiveList: ListNode",
                "          Semicolon: Semicolon |;|",
                "  End: EndKeyword |end|"));
        }
        public void TestAbstractClass()
        {
            Assert.That("class abstract end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: AbstractSemikeyword |abstract|",
                "  OpenParenthesis: (none)",
                "  InheritanceList: ListNode",
                "  CloseParenthesis: (none)",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestSealedClass()
        {
            Assert.That("class sealed end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: SealedSemikeyword |sealed|",
                "  OpenParenthesis: (none)",
                "  InheritanceList: ListNode",
                "  CloseParenthesis: (none)",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestBaseClass()
        {
            Assert.That("class(TComponent) end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: (none)",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  InheritanceList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |TComponent|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestInterface()
        {
            Assert.That("class(TInterfacedObject, IInterface) end", ParsesAs(
                "ClassTypeNode",
                "  Class: ClassKeyword |class|",
                "  Disposition: (none)",
                "  OpenParenthesis: OpenParenthesis |(|",
                "  InheritanceList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: Identifier |TInterfacedObject|",
                "      Delimiter: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      Item: Identifier |IInterface|",
                "      Delimiter: (none)",
                "  CloseParenthesis: CloseParenthesis |)|",
                "  Contents: ListNode",
                "  End: EndKeyword |end|"));
        }
        public void TestNoBody()
        {
            Parser parser = CreateParser("class(Exception);");
            AstNode node = parser.ParseRule(RuleType.ClassType);
            parser.ParseToken(new TokenSet(TokenType.Semicolon));
            Assert.That(parser.AtEof, Is.True);
            Assert.That(node.Inspect(), Is.EqualTo(
                "ClassTypeNode" + Environment.NewLine +
                "  Class: ClassKeyword |class|" + Environment.NewLine +
                "  Disposition: (none)" + Environment.NewLine +
                "  OpenParenthesis: OpenParenthesis |(|" + Environment.NewLine +
                "  InheritanceList: ListNode" + Environment.NewLine +
                "    Items[0]: DelimitedItemNode" + Environment.NewLine +
                "      Item: Identifier |Exception|" + Environment.NewLine +
                "      Delimiter: (none)" + Environment.NewLine +
                "  CloseParenthesis: CloseParenthesis |)|" + Environment.NewLine +
                "  Contents: ListNode" + Environment.NewLine +
                "  End: (none)"));
        }
    }
}
