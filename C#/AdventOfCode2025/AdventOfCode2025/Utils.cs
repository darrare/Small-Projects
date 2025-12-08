using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal static class Utils
    {
        /// <summary>
        /// Returns the lines from the input file.
        /// </summary>
        /// <param name="fileName">Name of the file within the InputData folder</param>
        /// <returns>Lines from the file</returns>
        public static List<string> ReadLinesFromInputFile(string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "InputData", fileName);
            var lines = File.ReadAllLines(path).ToList();
            return lines;
        }

        /// <summary>
        /// Returns the values of a comma delimited file with a single line.
        /// </summary>
        /// <param name="fileName">Name of the file within the InputData folder</param>
        /// <returns>Data values from the file</returns>
        public static List<string> ReadDataFromCommaDelimitedSingleLine(string fileName)
        {

            var path = Path.Combine(AppContext.BaseDirectory, "InputData", fileName);
            var lines = File.ReadAllLines(path).ToList();

            return lines[0].Split(',').ToList();
        }
    }
}
