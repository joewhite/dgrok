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
    public class PropertyTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.Property; }
        }

        public void TestRedeclaration()
        {
            Assert.That("property Foo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: (none)",
                "  TypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestReadOnly()
        {
            Assert.That("property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |FFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestIndexedProperty()
        {
            Assert.That("property Foo[Index: Integer]: Integer read GetFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: OpenBracket |[|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ParameterNode",
                "        ModifierNode: (none)",
                "        NameListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Identifier |Index|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        TypeNode: Identifier |Integer|",
                "        EqualSignNode: (none)",
                "        DefaultValueNode: (none)",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |GetFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestDefaultIndexedProperty()
        {
            Assert.That("property Foo[Index: Integer]: Integer read GetFoo; default;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: OpenBracket |[|",
                "  ParameterListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: ParameterNode",
                "        ModifierNode: (none)",
                "        NameListNode: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            ItemNode: Identifier |Index|",
                "            DelimiterNode: (none)",
                "        ColonNode: Colon |:|",
                "        TypeNode: Identifier |Integer|",
                "        EqualSignNode: (none)",
                "        DefaultValueNode: (none)",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |GetFoo|",
                "      DataNode: ListNode",
                "    Items[1]: DirectiveNode",
                "      SemicolonNode: Semicolon |;|",
                "      KeywordNode: DefaultSemikeyword |default|",
                "      ValueNode: (none)",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestDefault()
        {
            Assert.That("property Foo default 42;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: (none)",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: (none)",
                "  TypeNode: (none)",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: DefaultSemikeyword |default|",
                "      ValueNode: Number |42|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
        public void TestClassProperty()
        {
            Assert.That("class property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  ClassKeywordNode: ClassKeyword |class|",
                "  PropertyKeywordNode: PropertyKeyword |property|",
                "  NameNode: Identifier |Foo|",
                "  OpenBracketNode: (none)",
                "  ParameterListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  ColonNode: Colon |:|",
                "  TypeNode: Identifier |Integer|",
                "  DirectiveListNode: ListNode",
                "    Items[0]: DirectiveNode",
                "      SemicolonNode: (none)",
                "      KeywordNode: ReadSemikeyword |read|",
                "      ValueNode: Identifier |FFoo|",
                "      DataNode: ListNode",
                "  SemicolonNode: Semicolon |;|"));
        }
    }
}
