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

namespace DGrok.Framework
{
    public class Hit
    {
        private string _description;
        private Location _location;

        public Hit(Location location, string description)
        {
            _location = location;
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }
        public Location Location
        {
            get { return _location; }
        }
    }
}
