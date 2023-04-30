///////////////////////////////////////////////////////////////////////////////
//
// Author: Aaron Shingleton, shingletona@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 7 - Dispatch Table Calculator
// Description: Container class for Calculator function dispatch table
//
///////////////////////////////////////////////////////////////////////////////

namespace AS_2210_Calculator
{
    static class DispatchMgr
    {
        public static Calculator s_Calculator;
        public static CommandLineManager s_CLI;
        public static Dictionary<string, dynamic> dispatch = new();

        /// <summary>
        /// Wrapper function for dispatch table setup once references to other objects are valid
        /// </summary>
        public static void Init()
        {
            Setup();
        }

        /// <summary>
        /// Uses dispatch table to map text input from CommandLineManager to function calls throughout program classes
        /// </summary>
        static void Setup()
        {
            dispatch["add"] = new Action<string, string>((a, b) => s_Calculator.Add(a, b));
            dispatch["sub"] = new Action<string, string>((a, b) => s_Calculator.Sub(a, b));
            dispatch["multiply"] = new Action<string, string>((a, b) => s_Calculator.Multiply(a, b));
            dispatch["mod"] = new Action<string, string>((a, b) => s_Calculator.Mod(a, b));

            dispatch["square"] = new Action<string>((a) => s_Calculator.Square(a));
            dispatch["sqrt"] = new Action<string>((a) => s_Calculator.Sqrt(a));
            dispatch["exp"] = new Action<string, string>((a, b) => s_Calculator.Exponentiate(a, b));
            dispatch["factorial"] = new Action<string>((a) => s_Calculator.Factorial(a));

            dispatch["clr"] = new Action<string>((a) => s_Calculator.Clear(a));
            dispatch["store"] = new Action<string, string>((a, b) => s_Calculator.Store(a, b));
            dispatch["set"] = new Action<string, string>((a, b) => s_Calculator.Set(a, b));
            dispatch["undo"] = new Action(() => s_Calculator.Undo());
            dispatch["exportvars"] = new Action<string>((a) => s_Calculator.ExportVars(a));
            dispatch["importvars"] = new Action<string>((a) => s_Calculator.ImportVars(a));
            dispatch["vars"] = new Action(() => s_Calculator.WriteVars());

            dispatch["help"] = new Action(() => s_CLI.Help());
        }
    }
}
