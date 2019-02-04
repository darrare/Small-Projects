using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BuildGitPullBatchFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] directories = Directory.GetDirectories(@"D:\Repositories\3020 Class Repositories");
            using (StreamWriter sr = new StreamWriter(@"D:\Repositories\3020 Class Repositories\file.txt"))
            {
                for(int i = 0; i < directories.Length; i++)
                {
                    sr.WriteLine(@"cd " + directories[i] + " 2>> ..\\CDError.txt");
                    //sr.WriteLine("git clean -f");
                    sr.WriteLine("git fetch origin 2>> ..\\FetchError.txt");
                    sr.WriteLine("git reset --hard origin/master 2>> ..\\ResetError.txt");
                }
            }
        }
    }
}
