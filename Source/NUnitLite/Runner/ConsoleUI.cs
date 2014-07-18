// *****************************************************
// Copyright 2006, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;
using System.Collections;
using System.Reflection;
using NUnitLite.Framework;

namespace NUnitLite.Runner
{
    public class ConsoleUI : TestRunner
    {
        private TextWriter writer;
        private CommandLineOptions options;
        private int reportCount = 0;

        private ArrayList assemblies = new ArrayList();

        #region Main
        public static void Main( string[] args )
        {
            TestLoader.DefaultAssembly = Assembly.GetCallingAssembly();

            CommandLineOptions options = new CommandLineOptions();
            options.Parse( args );

            if ( !options.Nologo )
                WriteCopyright();

            if (options.Help)
                options.DisplayHelp();
            else if (options.HasError)
                Console.WriteLine(options.ErrorMessage);
            else
            {
                ConsoleUI runner = new ConsoleUI( options, Console.Out );

                try
                {
                    runner.Run();
                }
                catch (TestRunnerException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            if (options.Wait)
            {
                Console.WriteLine("Press Enter key to continue . . .");
                Console.ReadLine();
            }
        }
        #endregion

        #region Constructor
        public ConsoleUI(CommandLineOptions options, TextWriter writer)
        {
            this.options = options;
            this.writer = writer;
        }
        #endregion

        #region Public Methods
        public void Run()
        {
            if (options.Parameters.Length > 0)
                foreach (string name in options.Parameters)
                    assemblies.Add(Assembly.Load(name));

            if (options.TestCount > 0)
                Run(options.Tests);
            else if (assemblies.Count > 0)
                Run((Assembly[])assemblies.ToArray(typeof(Assembly)));
            else
                Run( TestLoader.DefaultAssembly );
        }

        public override TestResult Run(NUnitLite.Framework.ITest test)
        {
            TestResult result = base.Run(test);
            ResultSummary summary = new ResultSummary(result);

            writer.WriteLine("{0} Tests : {1} Errors, {2} Failures, {3} Not Run",
                summary.TestCount, summary.ErrorCount, summary.FailureCount, summary.NotRunCount);

            if (summary.ErrorCount + summary.FailureCount > 0)
                PrintErrorReport(result);

            if (summary.NotRunCount > 0)
                PrintNotRunReport(result);

            return result;
        }
        #endregion

        #region Helper Methods
        private static void WriteCopyright()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            System.Version version = executingAssembly.GetName().Version;

            object[] objectAttrs = executingAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            AssemblyProductAttribute productAttr = (AssemblyProductAttribute)objectAttrs[0];

            objectAttrs = executingAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            AssemblyCopyrightAttribute copyrightAttr = (AssemblyCopyrightAttribute)objectAttrs[0];

            Console.WriteLine(String.Format("{0} version {1}", productAttr.Product, version.ToString(3)));
            Console.WriteLine(copyrightAttr.Copyright);
            Console.WriteLine();

            string clrPlatform = Type.GetType("Mono.Runtime", false) == null ? ".NET" : "Mono";
            Console.WriteLine("Runtime Environment -");
            Console.WriteLine("    OS Version: {0}", Environment.OSVersion);
            Console.WriteLine("  {0} Version: {1}", clrPlatform, Environment.Version);
            Console.WriteLine();
        }
        
        private void PrintErrorReport(TestResult result)
        {
            reportCount = 0;
            writer.WriteLine();
            writer.WriteLine("Errors and Failures:");
            PrintErrorResults(result);
        }

        private void PrintErrorResults(TestResult result)
        {
            if (result.Results != null)
                foreach (TestResult r in result.Results)
                    PrintErrorResults(r);
            else if (result.IsError || result.IsFailure)
            {
                writer.WriteLine();
                writer.WriteLine("{0}) {1} ({2})", ++reportCount, result.Test.Name, result.Test.FullName);
                writer.WriteLine(result.Message);
                writer.WriteLine(result.StackTrace);
            }
        }

        private void PrintNotRunReport(TestResult result)
        {
            reportCount = 0;
            writer.WriteLine();
            writer.WriteLine("Errors and Failures:");
            PrintNotRunResults(result);
        }

        private void PrintNotRunResults(TestResult result)
        {
            if (result.Results != null)
                foreach (TestResult r in result.Results)
                    PrintNotRunResults(r);
            else if (result.ResultState == ResultState.NotRun)
            {
                writer.WriteLine();
                writer.WriteLine("{0}) {1} ({2}) : {3}", ++reportCount, result.Test.Name, result.Test.FullName, result.Message);
            }
        }
        #endregion
    }
}
