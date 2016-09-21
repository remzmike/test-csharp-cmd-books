using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cchtask
{
    class Program
    {
        static void Main(string[] args)
        {            
            Library library;

            if (args.Length==0 || !args[0].StartsWith("-") )
            {
                Help();
            }
            else
            {
                library = new Library();

                try
                {
                    switch (args[0])
                    {
                        case "-list":
                            library.List();
                            break;
                        case "-listsortby":
                            library.List(GetArgument(args));
                            break;
                        case "-checkout":
                            library.CheckOut(GetIntArgument(args));
                            break;
                        case "-checkin":
                            library.CheckIn(GetIntArgument(args));
                            break;
                        case "-?":
                            Help();
                            break;
                        default:
                            Help();
                            break;
                    }
                }
                catch (CommandLineException)
                {
                    Help();
                }
            }
        }

        static void Help()
        {
            string helpString = @"
Usage: cchtask -COMMAND [ARGUMENT]

Commands:

    -list
    -listsortby [column]
    -checkout [id]
    -checkin [id]

Examples:

    cchtask -list
    cchtask -listsortby title
    cchtask -listsortby ""title desc""
    cchtask -checkout 1
    cchtask -checkin 1
";
            Console.Write(helpString);
        }

        static string GetArgument(string[] args)
        {
            if (args.Length == 2)
            {
                return args[1];
            }
            else
            {
                throw new CommandLineException();
            }
        }

        static int GetIntArgument(string[] args)
        {
            int i;
            string s = GetArgument(args);
            if (Int32.TryParse(s, out i))
            {
                return i;
            }
            else
            {
                throw new CommandLineException();
            }
        }

    }

    class CommandLineException : Exception
    {
    }

}