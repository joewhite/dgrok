// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;

namespace NUnitLite.Framework
{
    public class StackFilter
    {
        public static string Filter(string rawTrace)
        {
            if (rawTrace == null) return null;

            StringReader sr = new StringReader(rawTrace);
            StringWriter sw = new StringWriter();

            // TODO: Need try here?
            string line;
            while ( (line = sr.ReadLine()) != null )
                if ( !FilterLine( line ) )
                    sw.WriteLine(line);

            return sw.ToString();;
        }

        static bool FilterLine(string line)
        {
            string[] patterns = new string[]
			{
				"NUnitLite.Framework.TestCase",
				"NUnitLite.Framework.TestResult",
				"NUnitLite.Framework.TestSuite",
				"NUnitLite.Framework.Assert", 
			};

            for (int i = 0; i < patterns.Length; i++)
            {
                if (line.IndexOf(patterns[i]) > 0)
                    return true;
            }

            return false;
        }
    }
}
