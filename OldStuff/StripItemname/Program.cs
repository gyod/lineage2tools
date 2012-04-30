using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StripItemname
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader input = new StreamReader("itemname-e.tsv");
            StreamWriter output = new StreamWriter("itemnames.tsv");

            do
            {
                string[] info = input.ReadLine().Split('\t');
                output.WriteLine(info[0] + "\t" + info[1]);

            } while (!input.EndOfStream);

            input.Close();
            output.Close();
        }
    }
}
