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
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: (none)",
                "  Type: (none)",
                "  DirectiveList: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestReadOnly()
        {
            Assert.That("property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: (none)",
                "      Directive: ReadSemikeyword |read|",
                "      Value: Identifier |FFoo|",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestIndexedProperty()
        {
            Assert.That("property Foo[Index: Integer]: Integer read GetFoo;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: OpenBracket |[|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ParameterNode",
                "        Modifier: (none)",
                "        Names: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: Identifier |Index|",
                "            Delimiter: (none)",
                "        Colon: Colon |:|",
                "        Type: Identifier |Integer|",
                "        EqualSign: (none)",
                "        DefaultValue: (none)",
                "      Delimiter: (none)",
                "  CloseBracket: CloseBracket |]|",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: (none)",
                "      Directive: ReadSemikeyword |read|",
                "      Value: Identifier |GetFoo|",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestDefaultIndexedProperty()
        {
            Assert.That("property Foo[Index: Integer]: Integer read GetFoo; default;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: OpenBracket |[|",
                "  ParameterList: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      Item: ParameterNode",
                "        Modifier: (none)",
                "        Names: ListNode",
                "          Items[0]: DelimitedItemNode",
                "            Item: Identifier |Index|",
                "            Delimiter: (none)",
                "        Colon: Colon |:|",
                "        Type: Identifier |Integer|",
                "        EqualSign: (none)",
                "        DefaultValue: (none)",
                "      Delimiter: (none)",
                "  CloseBracket: CloseBracket |]|",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: (none)",
                "      Directive: ReadSemikeyword |read|",
                "      Value: Identifier |GetFoo|",
                "      Data: ListNode",
                "    Items[1]: DirectiveNode",
                "      Semicolon: Semicolon |;|",
                "      Directive: DefaultSemikeyword |default|",
                "      Value: (none)",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestDefault()
        {
            Assert.That("property Foo default 42;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: (none)",
                "  Type: (none)",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: (none)",
                "      Directive: DefaultSemikeyword |default|",
                "      Value: Number |42|",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestClassProperty()
        {
            Assert.That("class property Foo: Integer read FFoo;", ParsesAs(
                "PropertyNode",
                "  Class: ClassKeyword |class|",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  DirectiveList: ListNode",
                "    Items[0]: DirectiveNode",
                "      Semicolon: (none)",
                "      Directive: ReadSemikeyword |read|",
                "      Value: Identifier |FFoo|",
                "      Data: ListNode",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
