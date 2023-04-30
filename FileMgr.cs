///////////////////////////////////////////////////////////////////////////////
//
// Author: Aaron Shingleton, shingletona@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 7 - Dispatch Table Calculator
// Description: Class for handling file I/O taking place in (Project)/SavedVariables
//
///////////////////////////////////////////////////////////////////////////////

namespace AS_2210_Calculator
{
    static class FileMgr
    {
        static string VARIABLES_PATH = @"..\..\..\SavedVariables";

        /// <summary>
        /// Wrapper function for first-time setup: ensures that I/O directory exists
        /// </summary>
        public static void Init()
        {
            if (!Directory.Exists(VARIABLES_PATH))
            {
                Directory.CreateDirectory(VARIABLES_PATH);
            }
        }

        /// <summary>
        /// Writes current calculator variable dictionary to a text file
        /// </summary>
        /// <param name="fileName">The name of the file to write to</param>
        /// <param name="dict">The variable dictionary to write to the file</param>
        public static void ExportVariables (string fileName, Dictionary<string, double> dict)
        {
            string exportPath = VARIABLES_PATH + @$"\{fileName}.txt";

            using (StreamWriter sw = new StreamWriter(exportPath, false))
            {
                foreach (var key in dict.Keys)
                {
                    sw.WriteLine($"{key}:{dict[key]}");
                }
            }
        }

        /// <summary>
        /// Imports a variable dictionary from a text file
        /// </summary>
        /// <param name="fileName">The name of the text file which contains the variable dictionary</param>
        /// <returns></returns>
        public static Dictionary<string, double> CalcImportVars(string fileName)
        {
            Dictionary<string, double> import = new Dictionary<string, double>();
            string importPath = VARIABLES_PATH + @"\" + fileName + ".txt";

            if (File.Exists(importPath)) 
            {
                using (StreamReader sw = new StreamReader(importPath))
                {
                    string line;
                    while ((line = sw.ReadLine()) != null)
                    {
                        string[] lineData = line.Split(":");
                        import[lineData[0]] = double.Parse(lineData[1]);
                    }
                }
                return import;
            }
            
            else
            {
                Console.WriteLine("File not found in SavedVariables folder.");
                return null;
            }
        }
    }
}
