// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;
using NUnit.Framework;

namespace DGrok.Tests
{
    [TestFixture]
    public class ArrayTypeTests : ParserTestCase
    {
        protected override RuleType RuleType
        {
            get { return RuleType.ArrayType; }
        }

        [Test]
        public void DynamicArray()
        {
            Assert.That("array of Integer", ParsesAs(
                "ArrayTypeNode",
                "  ArrayKeywordNode: ArrayKeyword |array|",
                "  OpenBracketNode: (none)",
                "  IndexListNode: ListNode",
                "  CloseBracketNode: (none)",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |Integer|"));
        }
        [Test]
        public void TwoDimensionalArray()
        {
            Assert.That("array [24..42, Byte] of Integer", ParsesAs(
                "ArrayTypeNode",
                "  ArrayKeywordNode: ArrayKeyword |array|",
                "  OpenBracketNode: OpenBracket |[|",
                "  IndexListNode: ListNode",
                "    Items[0]: DelimitedItemNode",
                "      ItemNode: BinaryOperationNode",
                "        LeftNode: Number |24|",
                "        OperatorNode: DotDot |..|",
                "        RightNode: Number |42|",
                "      DelimiterNode: Comma |,|",
                "    Items[1]: DelimitedItemNode",
                "      ItemNode: Identifier |Byte|",
                "      DelimiterNode: (none)",
                "  CloseBracketNode: CloseBracket |]|",
                "  OfKeywordNode: OfKeyword |of|",
                "  TypeNode: Identifier |Integer|"));
        }
    }
}
