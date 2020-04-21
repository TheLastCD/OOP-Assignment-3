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
            ///diff GitRepositories_2a.txt GitRepositories_2b.txt
            //Declaration of method variables
            bool leave = false, failed = false;
            List<string> FirstFile = new List<string>(), SecondFile = new List<string>();
            string[] files = new string[0];
            int Diff_Loc = 0;
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

            // Code that verifies that the two files are the samw
            Console.Write($">: [Output] ");
            try
            {
                for (int i = 0; i < Second.Map.Count()-1; i++)
                {
                    if (First.Map[i] == First.Compressed.IndexOf("NewLine"))
                    {
                        Diff_Loc++;
                    }
                    if(First.Compressed[First.Map[i]] != Second.Compressed[Second.Map[i]])
                    {
                        FindThing(First.ReproduceSection(i), Second.ReproduceSection(i), Diff_Loc);

                        failed = true;

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

        public static void FindThing(List<string> ReferenceFile, List<string> SecondFile,int line)
        {
            //difference held at value 2
            //if second file is missing something that the first file has
            //adding if the the first file is missing something in the second 
            int marker = 0;
            bool addition = true;
            Console.Write($"Line: {line} \n");
            for(int x =0; x <= ReferenceFile.Count-1; x++)
            {
                if(ReferenceFile[x]!= SecondFile[x])
                {
                    if(ReferenceFile[x] == SecondFile[x +1])
                        Console.Write("+ ");
                    else
                    {
                        Console.Write("- ");
                        addition = false;
                    }
                    marker = x;

                    break;
                }

            }

            for(int i = 0; i <= ReferenceFile.Count-1; i++)
            {
                if(i == marker)
                {
                    if (addition)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{SecondFile[i]} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{ReferenceFile[i]} ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write($"{ReferenceFile[i]} ");
                }
            }
        }
        public void SaveLogFile(string change)
        {
            File.WriteAllText(@"Log.txt", change);
        }

    }

}
