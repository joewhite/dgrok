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

namespace DGrok.Framework
{
    public class CompilerOptions
    {
        private string _optionsSetOff = "";
        private string _optionsSetOn = "";

        public string OptionsSetOff
        {
            get { return _optionsSetOff; }
            set { _optionsSetOff = value.ToUpperInvariant(); }
        }
        public string OptionsSetOn
        {
            get { return _optionsSetOn; }
            set { _optionsSetOn = value.ToUpperInvariant(); }
        }

        private bool? GetOptionDefault(char option)
        {
            switch (option)
            {
                case 'B': return false;
                case 'C': return true;
                case 'D': return true;
                case 'E': return false;
                case 'F': return false;
                case 'G': return true;
                case 'H': return true;
                case 'I': return true;
                case 'J': return false;
                case 'K': return false;
                case 'L': return true;
                case 'M': return false;
                case 'N': return true;
                case 'O': return true;
                case 'P': return true;
                case 'Q': return false;
                case 'R': return false;
                case 'S': return false;
                case 'T': return false;
                case 'U': return false;
                case 'V': return true;
                case 'W': return false;
                case 'X': return true;
                case 'Y': return true;
                case 'Z': return false;
                default: return null;
            }
        }
        private bool? GetOptionSetting(char option)
        {
            if (_optionsSetOff.IndexOf(option) >= 0)
                return false;
            if (_optionsSetOn.IndexOf(option) >= 0)
                return true;
            return null;
        }
        public bool IsOptionOff(char option)
        {
            ValidateOption(option);
            return !GetOptionSetting(option) ?? !GetOptionDefault(option) ?? false;
        }
        public bool IsOptionOn(char option)
        {
            ValidateOption(option);
            return GetOptionSetting(option) ?? GetOptionDefault(option) ?? false;
        }
        private void ValidateOption(char option)
        {
            if (!char.IsUpper(option))
                throw new ArgumentOutOfRangeException("Option must be specified as an uppercase letter");
        }
    }
}
