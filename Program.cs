using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;

namespace OOP_Assignment_2_Code_Review
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaration of method variables
            bool leave = false, failed = false;
            List<string> FirstFile = new List<string>(), SecondFile = new List<string>();
            string[] files = new string[0];
            // Responsible for error checking the users input, only allowing an input that both exist and is correctly spelt
            while (!leave)
            {
                try
                {
                    Console.Write(">: [Input] ");
                    files = Console.ReadLine().Split();
                    if (files[0].ToLower() == "diff" && files.Length == 3)
                    {
                        FirstFile = new List<string>(File.ReadAllLines(files[1].ToString()));
                        SecondFile = new List<string>(File.ReadAllLines(files[2].ToString()));
                        leave = true;
                    }
                    else
                    {
                        if (files.Length != 3)
                            Console.WriteLine("Please Only Enter 2 Files");
                        else
                            Console.WriteLine("You Have Not Used The Diff Command");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //instantiations of Txt_File class
            Txt_File First = new Txt_File(FirstFile);
            Txt_File Second = new Txt_File(SecondFile);

            // Code that verifies that the two files are the same
            Console.Write($">: [Output] ");
            try
            {
                for (int i = 0; i < Second.Compressed.Count()-1; i++)
                {

                    if (Second.Compressed[i] != First.Compressed[i])
                    {
                        failed = true;
                        Console.Write($"{files[1]} and {files[2]} are different");
                        FindThing(First.Compressed[i],Second.Compressed[i], i);
                    }
                    else
                    {
                        Second.Compressed[i] = (Second.Compressed[i].Item1, Second.Compressed[i].Item2 - 1);
                        First.Compressed[i] = (First.Compressed[i].Item1, First.Compressed[i].Item2 - 1);
                    }
                }
                if (!failed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{files[1]} and {files[2]} are not different");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n {e.Message}");
            }
        }

        public static void FindThing((string,int) First, (string,int) Second, int indexOfValues)
        {
            
        }

    }

}
