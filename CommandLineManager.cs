///////////////////////////////////////////////////////////////////////////////
//
// Author: Aaron Shingleton, shingletona@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 7 - Dispatch Table Calculator
// Description: Class for handling console I/O for user
//
///////////////////////////////////////////////////////////////////////////////

namespace AS_2210_Calculator
{
    internal class CommandLineManager
    {
        Calculator calcRef;
        Dictionary<string, dynamic> functionsDispatch;
        public CommandLineManager(Calculator calculator, Dictionary<string, dynamic> functionsList)
        {
            calcRef = calculator;
            functionsDispatch = functionsList;
        }

        /// <summary>
        /// Starts a while loop that processes user input into the Calculator static instance.
        /// </summary>
        public void PromptLoop()
        {
            Help();
            while (true)
            {
                Console.WriteLine(calcRef.GetCurrentValue());
                string[] input = Console.ReadLine().Split(" ");
                input[0] = input[0].ToLower();

                if (!functionsDispatch.ContainsKey(input[0]))
                {
                    Console.WriteLine($"Input not valid. Please revise command and try again." +
                        $"\nCommand \"{input[0]}\" does not exist.");
                }

                else if (input.Length > 3)
                {
                    Console.WriteLine("Input not valid. Please revise command and try again." +
                        "\nThis s_Calculator does not support any commands with more than two input parameters.");
                }

                else if (input.Length == 3)
                {
                    string[] overrides = { "store", "set" };

                    if (overrides.Contains(input[0]))
                    {
                        functionsDispatch[input[0]](input[1], input[2]);
                    }

                    else
                    {
                        functionsDispatch[input[0]](input[1], input[2]);
                    }

                }

                else if (input.Length == 2)
                {
                    string currentValue = calcRef.GetCurrentValue().ToString();
                    // Philip Edwards proposed using an overrides filter string array: thought it was a really good idea.
                    string[] overrides = { "clr", "square", "sqrt", "factorial", "exportvars", "importvars" };

                    if (overrides.Contains(input[0]))
                    {
                        functionsDispatch[input[0]](input[1]);
                    }

                    else
                    {
                        functionsDispatch[input[0]](currentValue, input[1]);
                    }

                }

                else if (input.Length == 1)
                {
                    string currentValue = calcRef.GetCurrentValue().ToString();

                    // Philip Edwards proposed using a filter string array: thought it was a really good idea.
                    string[] overrides_void = { "undo", "vars", "help" };
                    string[] overrides_cVal = { "clr" };
                    string[] error = { "add", "sub", "multiply", "mod", "" };

                    if (error.Contains(input[0]))
                    {
                        Console.WriteLine("Input not valid. Please revise command and try again." +
                            "\n The command given requires at least one parameter.");
                    }

                    else if (overrides_void.Contains(input[0]))
                    {
                        functionsDispatch[input[0]]();
                    }

                    else if (overrides_cVal.Contains(input[0]))
                    {
                        functionsDispatch[input[0]]("currentValue");
                    }

                    else
                    {
                        functionsDispatch[input[0]](currentValue);
                    }

                }
            }
        }

        /// <summary>
        /// Displays page explaining all of the commands for the program
        /// </summary>
        public void Help()
        {
            string helpFilePath = @"..\..\..\help.txt";
            using (StreamReader sw = new StreamReader(helpFilePath))
            {
                Console.Write(sw.ReadToEnd() + "\n");
            }
        }
    }
}
