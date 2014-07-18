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
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: (none)",
                "  ReadSpecifier: (none)",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
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
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: ReadSemikeyword |read|",
                "  ReadSpecifier: Identifier |FFoo|",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestWriteOnly()
        {
            Assert.That("property Foo: Integer write FFoo;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: (none)",
                "  ReadSpecifier: (none)",
                "  Write: WriteSemikeyword |write|",
                "  WriteSpecifier: Identifier |FFoo|",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestReadWrite()
        {
            Assert.That("property Foo: Integer read FFoo write FFoo;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: ReadSemikeyword |read|",
                "  ReadSpecifier: Identifier |FFoo|",
                "  Write: WriteSemikeyword |write|",
                "  WriteSpecifier: Identifier |FFoo|",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
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
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: ReadSemikeyword |read|",
                "  ReadSpecifier: Identifier |GetFoo|",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestIndex()
        {
            Assert.That("property Foo: Integer index 42 read GetFoo;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |Integer|",
                "  Index: IndexSemikeyword |index|",
                "  IndexValue: Number |42|",
                "  Read: ReadSemikeyword |read|",
                "  ReadSpecifier: Identifier |GetFoo|",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestStored()
        {
            Assert.That("property Foo stored False;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: (none)",
                "  Type: (none)",
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: (none)",
                "  ReadSpecifier: (none)",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: StoredSemikeyword |stored|",
                "  StoredSpecifier: Identifier |False|",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
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
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: (none)",
                "  ReadSpecifier: (none)",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: DefaultSemikeyword |default|",
                "  DefaultValue: Number |42|",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestNoDefault()
        {
            Assert.That("property Foo nodefault;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: (none)",
                "  Type: (none)",
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: (none)",
                "  ReadSpecifier: (none)",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: NoDefaultSemikeyword |nodefault|",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
        public void TestImplements()
        {
            Assert.That("property Foo: IFoo read FFoo implements IFoo;", ParsesAs(
                "PropertyNode",
                "  Class: (none)",
                "  Property: PropertyKeyword |property|",
                "  Name: Identifier |Foo|",
                "  OpenBracket: (none)",
                "  ParameterList: ListNode",
                "  CloseBracket: (none)",
                "  Colon: Colon |:|",
                "  Type: Identifier |IFoo|",
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: ReadSemikeyword |read|",
                "  ReadSpecifier: Identifier |FFoo|",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: ImplementsSemikeyword |implements|",
                "  ImplementsSpecifier: Identifier |IFoo|",
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
                "  Index: (none)",
                "  IndexValue: (none)",
                "  Read: ReadSemikeyword |read|",
                "  ReadSpecifier: Identifier |FFoo|",
                "  Write: (none)",
                "  WriteSpecifier: (none)",
                "  Stored: (none)",
                "  StoredSpecifier: (none)",
                "  Default: (none)",
                "  DefaultValue: (none)",
                "  Implements: (none)",
                "  ImplementsSpecifier: (none)",
                "  Semicolon: Semicolon |;|"));
        }
    }
}
