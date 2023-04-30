///////////////////////////////////////////////////////////////////////////////
//
// Author: Aaron Shingleton, shingletona@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 7 - Dispatch Table Calculator
// Description: Uses a dispatch table to add commands to a Calculator class
//
///////////////////////////////////////////////////////////////////////////////

namespace AS_2210_Calculator
{
    
    internal class Program
    {
        static Calculator calculator = new Calculator();
        static CommandLineManager CommandLineManager;
        static void Main(string[] args)
        {
            // Spaghetti
            DispatchMgr.s_Calculator = calculator;
            CommandLineManager = new CommandLineManager(DispatchMgr.s_Calculator, DispatchMgr.dispatch);
            DispatchMgr.s_CLI = CommandLineManager;
            DispatchMgr.Init();

            // Boil the spaghetti a lil bit
            FileMgr.Init();

            // Enjoy the spaghetti
            CommandLineManager.PromptLoop();
        }
        
    }
}