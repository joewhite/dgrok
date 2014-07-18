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
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestContents()
        {
            Assert.That("class procedure Foo; end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "    Items[0]: VisibilitySectionNode",
                "      VisibilityNode: (none)",
                "      ContentListNode: ListNode",
                "        Items[0]: MethodHeadingNode",
                "          ClassKeywordNode: (none)",
                "          MethodTypeNode: ProcedureKeyword |procedure|",
                "          NameNode: Identifier |Foo|",
                "          OpenParenthesisNode: (none)",
                "          ParameterListNode: ListNode",
                "          CloseParenthesisNode: (none)",
                "          ColonNode: (none)",
                "          ReturnTypeNode: (none)",
                "          DirectiveListNode: ListNode",
                "          SemicolonNode: Semicolon |;|",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestAbstractClass()
        {
            Assert.That("class abstract end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: AbstractSemikeyword |abstract|",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestSealedClass()
        {
            Assert.That("class sealed end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: SealedSemikeyword |sealed|",
                "  OpenParenthesisNode: (none)",
                "  InheritanceListNode: ListNode",
                "  CloseParenthesisNode: (none)",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestBaseClass()
        {
            Assert.That("class(TComponent) end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  InheritanceListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |TComponent|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestInterface()
        {
            Assert.That("class(TInterfacedObject, IInterface) end", ParsesAs(
                "ClassTypeNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  DispositionNode: (none)",
                "  OpenParenthesisNode: OpenParenthesis |(|",
                "  InheritanceListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: Identifier |TInterfacedObject|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |IInterface|",
                "      DelimiterNode: (none)",
                "  CloseParenthesisNode: CloseParenthesis |)|",
                "  ContentListNode: ListNode",
                "  EndKeywordNode: EndKeyword |end|"));
        }
        public void TestNoBody()
        {
            Parser parser = CreateParser("class(Exception);");
            AstNode node = parser.ParseRule(RuleType.ClassType);
            parser.ParseToken(new SingleTokenTokenSet(TokenType.Semicolon));
            Assert.That(parser.AtEof, Is.True);
            Assert.That(node.Inspect(), Is.EqualTo(
                "ClassTypeNode" + Environment.NewLine +
                "  ClassKeywordNode: ClassKeyword |class|" + Environment.NewLine +
                "  DispositionNode: (none)" + Environment.NewLine +
                "  OpenParenthesisNode: OpenParenthesis |(|" + Environment.NewLine +
                "  InheritanceListNode: ListNode" + Environment.NewLine +
                "    Items[0]: DelimitedItemNode" + Environment.NewLine +
                "      ItemNode: Identifier |Exception|" + Environment.NewLine +
                "      DelimiterNode: (none)" + Environment.NewLine +
                "  CloseParenthesisNode: CloseParenthesis |)|" + Environment.NewLine +
                "  ContentListNode: ListNode" + Environment.NewLine +
                "  EndKeywordNode: (none)"));
        }
    }
}
