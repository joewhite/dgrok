// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Reflection;
using NUnitLite.Framework;
using NUnitLite.Runner;

namespace DGrok.Tests
{
    public class Program
    {
        public static int Main()
        {
            CommandLineOptions options = new CommandLineOptions();
            ConsoleUI runner = new ConsoleUI(options, Console.Out);
            try
            {
                TestResult result = runner.Run(Assembly.GetExecutingAssembly());
                if (result.IsFailure || result.IsError)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return 1;
            }
        }
    }
}
