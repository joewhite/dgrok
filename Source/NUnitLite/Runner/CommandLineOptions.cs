// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.Text;
using System.Collections;

namespace NUnitLite.Runner
{
    public class CommandLineOptions
    {
        private string optionChars;

        bool error = false;

        ArrayList invalidOptions = new ArrayList();
        ArrayList parameters = new ArrayList();

        private bool wait = false;
        public bool Wait
        {
            get { return wait; }
        }

        private bool nologo = false;
        public bool Nologo
        {
            get { return nologo; }
        }

        private bool help = false;
        public bool Help
        {
            get { return help; }
        }

        private ArrayList tests = new ArrayList();
        public string[] Tests
        {
            get { return (string[])tests.ToArray(typeof(string)); }
        }

        public int TestCount
        {
            get { return tests.Count; }
        }

        public CommandLineOptions()
        {
            this.optionChars = System.IO.Path.DirectorySeparatorChar == '/' ? "-" : "/-";
        }

        public CommandLineOptions(string optionChars)
        {
            this.optionChars = optionChars;
        }

        public void Parse(params string[] args)
        {
            foreach( string arg in args )
            {
                if (optionChars.IndexOf(arg[0]) >= 0 )
                    ProcessOption(arg);
                else
                    ProcessParameter(arg);
            }
        }

        public string[] Parameters
        {
            get { return (string[])parameters.ToArray( typeof(string) ); }
        }

        private void ProcessOption(string opt)
        {
            int pos = opt.IndexOfAny( new char[] { ':', '=' } );
            string val = string.Empty;

            if (pos >= 0)
            {
                val = opt.Substring(pos + 1);
                opt = opt.Substring(0, pos);
            }

            switch (opt.Substring(1))
            {
                case "wait":
                    wait = true;
                    break;
                case "nologo":
                    nologo = true;
                    break;
                case "help":
                    help = true;
                    break;
                case "test":
                    tests.Add(val); 
                    break;
                default:
                    error = true;
                    invalidOptions.Add(opt);
                    break;
            }
        }

        private void ProcessParameter(string param)
        {
            parameters.Add(param);
        }

        public bool HasError
        {
            get { return error; }
        }

        public string ErrorMessage
        {
            get 
            {
                StringBuilder sb = new StringBuilder();
                foreach (string opt in invalidOptions)
                    sb.Append( "Invalid option: " + opt + Environment.NewLine );
                return sb.ToString();
            }
        }

        public void DisplayHelp()
        {
            string name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            Console.WriteLine();
            Console.WriteLine("{0} [assemblies] [options]", name);
            Console.WriteLine();
            Console.WriteLine("Runs a set of NUnitLite tests from the console.");
            Console.WriteLine();
            Console.WriteLine("You may specify one or more test assemblies by name, without a path or");
            Console.WriteLine("extension. They must be in the same in the same directory as the exe");
            Console.WriteLine("or on the probing path. If no assemblies are provided, tests in the");
            Console.WriteLine("executing assembly itself are run.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -test:testname  Provides the name of a test to run. This option may be");
            Console.WriteLine("                  repeated. If no test names are given, all tests are run.");
            Console.WriteLine();
            Console.WriteLine("  -help           Displays this help");
            Console.WriteLine();
            Console.WriteLine("  -nologo         Suppresses display of the initial message");
            Console.WriteLine();
            Console.WriteLine("  -wait           Waits for a key press before exiting");
            Console.WriteLine();
            if (System.IO.Path.DirectorySeparatorChar != '/')
            {
                Console.WriteLine("On Windows, options may be prefixed by a '/' character if desired");
                Console.WriteLine();
            }
            Console.WriteLine("Options that take values may use an equal sign or a colon");
            Console.WriteLine("to separate the option from its value.");
            Console.WriteLine();
        }
    }
}
