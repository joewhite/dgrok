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
