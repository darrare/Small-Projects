using System;

namespace NetFiveConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }

        public static string GetDbModels()
        {
            return EntityCoreNetStandardLibrary.Generator.GetDbModels();
        }
    }
}
