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
    public class FileTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FileType; }
        }

        public void TestUntypedFile()
        {
            Assert.That("file", ParsesAs(
                "FileTypeNode",
                "  File: FileKeyword |file|",
                "  Of: (none)",
                "  Type: (none)"));
        }
        public void TestIdentifier()
        {
            Assert.That("file of Integer", ParsesAs(
                "FileTypeNode",
                "  File: FileKeyword |file|",
                "  Of: OfKeyword |of|",
                "  Type: Identifier |Integer|"));
        }
        public void TestQualified()
        {
            Assert.That("file of Foo.TBar", ParsesAs(
                "FileTypeNode",
                "  File: FileKeyword |file|",
                "  Of: OfKeyword |of|",
                "  Type: BinaryOperationNode",
                "    Left: Identifier |Foo|",
                "    Operator: Dot |.|",
                "    Right: Identifier |TBar|"));
        }
    }
}
