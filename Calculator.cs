///////////////////////////////////////////////////////////////////////////////
//
// Author: Aaron Shingleton, shingletona@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 7 - Dispatch Table Calculator
// Description: Class for performing calculator functions
//
///////////////////////////////////////////////////////////////////////////////

namespace AS_2210_Calculator
{
    internal class Calculator
    {
        Dictionary<string, double> variables = new();
        Stack<double> calcStack = new Stack<double>();
        

        public Calculator()
        {
            variables["currentValue"] = 0;
        }

        /// <summary>
        /// If it exists, gets the value of currentValue variable.
        /// Otherwise, gets default value of 0.
        /// </summary>
        /// <returns>The value of currentValue, or the default value</returns>
        public double GetCurrentValue()
        {
            return variables.ContainsKey("currentValue") ? variables["currentValue"] : 0;
        }

        /// <summary>
        /// Creates a new variable entry identified by destination, mapped to parsed source value.
        /// </summary>
        /// <param name="destination">The new variable's name</param>
        /// <param name="source">The stored value of the new variable.</param>
        public void Store(string source, string destination)
        {
            double x = 0;

            if (!double.TryParse(source, out x))
            {
                Console.WriteLine($"Value \"{source}\" not valid.");
                return;
            }
            
            else if (variables.Keys.Contains(destination))
            {
                Console.WriteLine($"Variable {destination} value overwritten from {variables[destination]} to {source}.");
            }

            else if (!variables.Keys.Contains(destination))
            {
                Console.WriteLine($"Variable {destination} created.");
            }

            variables[destination] = x;
        }

        /// <summary>
        /// Sets a variable to another variable's value.
        /// Typical implementation is to set currentValue to source.
        /// May also be used to copy source value to other destination variables.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public void Set(string destination, string source)
        {
            // 3-input tier in Program.cs : Sets source variable into destination variable
            if (variables.Keys.Contains(source) && variables.Keys.Contains(destination))
            {
                variables[destination] = variables[source];
                Console.WriteLine($"Variable {destination} set to {variables[source]}.");
            }

            else 
            {
                double x = Parse(source);
                PushState();
                variables["currentValue"] = x;
            }
        
        }

        /// <summary>
        /// Tries to fetch the stored value of the input variable name.
        /// If not a variable name, assumed to be an input value to be operated on.
        /// </summary>
        /// <param name="input">The variable to be fetched, or the value to be operated on.</param>
        /// <returns></returns>
        public double Parse(string input)
        {
            double x;
            if (variables.ContainsKey(input))
            {
                x = variables[input];
            }
            else
            {
                if (!double.TryParse(input, out x))
                {
                    Console.WriteLine($"Value \"{input}\" not valid.");
                    x = 0;
                }
            }
            return x;
        }

        /// <summary>
        /// Adds two given values together and sets it as the current state value.
        /// </summary>
        /// <param name="a">The first value to be added.</param>
        /// <param name="b">The second value to be added.</param>
        public void Add(string a, string b)
        {
            double x = Parse(a);
            double y = Parse(b);

            PushState();
            variables["currentValue"] = x + y;
        }

        /// <summary>
        /// Subtracts b from a and sets currentValue to difference.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void Sub(string a, string b)
        {
            double x = Parse(a);
            double y = Parse(b);

            PushState();
            variables["currentValue"] = x - y;
        }

        /// <summary>
        /// Multiplies a * b and sets currentValue to product.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void Multiply(string a, string b)
        {
            double x = Parse(a);
            double y = Parse(b);

            PushState();
            variables["currentValue"] = x * y;
        }

        /// <summary>
        /// Performs a % b and sets currentValue to remainder.
        /// </summary>
        /// <param name="a"></param>
        public void Mod(string a, string b)
        {
            double x = Parse(a);
            double y = Parse(b);

            PushState();
            variables["currentValue"] = x % y;
        }

        /// <summary>
        /// Squares parsed value of a and sets currentValue to result.
        /// </summary>
        /// <param name="a"></param>
        public void Square(string a)
        {
            double x = Parse(a);

            PushState();
            variables["currentValue"] = Math.Pow(x, 2);
        }

        /// <summary>
        /// Takes sqrt of a and sets currentValue to result.
        /// </summary>
        /// <param name="a"></param>
        public void Sqrt(string a)
        {
            double x = Parse(a);
            PushState();
            variables["currentValue"] = Math.Sqrt(x);
        }

        /// <summary>
        /// Raises x parsed value to y parsed value power and sets currentValue to result.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void Exponentiate(string a, string b)
        {
            double x = Parse(a);
            double y = Parse(b);
            PushState();
            variables["currentValue"] = Math.Pow(x, y);
        }

        /// <summary>
        /// Performs (parsed) a! and stores result in currentValue.
        /// </summary>
        /// <param name="a"></param>
        public void Factorial(string a)
        {
            int iteration = (int)Parse(a);
            double value = 1;

            for (int i = iteration; i > 1; i--)
            {
                value *= iteration;
            }

            PushState();
            variables["currentValue"] = value;
        }

        public void Clear(string key)
        {
            if (variables.ContainsKey(key))
            {
                variables[key] = 0;
                Console.WriteLine($"Variable {key} set to {variables[key]}.");
            }
            else if (!variables.ContainsKey(key))
            {
                PushState();
                variables["currentValue"] = 0;
            }
        }

        /// <summary>
        /// Restores currentValue to next previous state.
        /// </summary>
        public void Undo()
        {
            if (calcStack.TryPop(out double result))
            {
                variables["currentValue"] = result;
            }
            else
            {
                Console.WriteLine("No operations to undo.");
            }
        }

        /// <summary>
        /// Shorthand method for pushing currentValue to calcStack.
        /// </summary>
        void PushState()
        {
            calcStack.Push(variables["currentValue"]);
        }

        /// <summary>
        /// Exports the current variable dictionary to (fileName).txt
        /// </summary>
        /// <param name="fileName"></param>
        public void ExportVars(string fileName)
        {
            FileMgr.ExportVariables(fileName, variables);
        }

        /// <summary>
        /// Imports a variable dictionary from (fileName).txt
        /// </summary>
        /// <param name="fileName"></param>
        public void ImportVars(string fileName)
        {
            variables = FileMgr.CalcImportVars(fileName);
            WriteVars();
        }

        /// <summary>
        /// Displays a small text block that lists the current variable dictionary
        /// </summary>
        public void WriteVars()
        {
            Console.WriteLine("\n===============\nCURRENT VARIABLES\n---------------");
            foreach (KeyValuePair<string, double> kvp in variables)
            {
                Console.WriteLine(kvp.Key + "=" + kvp.Value);
            }
            Console.WriteLine("===============\n");
        }
    }
}
