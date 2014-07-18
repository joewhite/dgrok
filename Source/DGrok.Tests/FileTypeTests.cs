// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class FileTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.FileType; }
        }

        [Test]
        public void UntypedFile()
        {
            Assert.That("file", ParsesAs(
                "FileTypeNode",
                "  FileKeywordNode: FileKeyword |file|",
                "  OfKeywordNode: (none)",
                "  TypeNode: (none)"));
        }
        [Test]
        public void Identifier()
        {
            Assert.That("file of Integer", ParsesAs(
                "FileTypeNode",
                "  FileKeywordNode: FileKeyword |file|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |Integer|"));
        }
        [Test]
        public void Qualified()
        {
            Assert.That("file of Foo.TBar", ParsesAs(
                "FileTypeNode",
                "  FileKeywordNode: FileKeyword |file|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: BinaryOperationNode",
                "    LeftNode: Identifier |Foo|",
                "    OperatorNode: Dot |.|",
                "    RightNode: Identifier |TBar|"));
        }
    }
}
