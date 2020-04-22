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
            while (true)
            {
                ///diff GitRepositories_2a.txt GitRepositories_2b.txt
                ///diff GitRepositories_3a.txt GitRepositories_3b.txt
                //Declaration of method variables
                bool leave = false, failed = false;
                List<string> FirstFile = new List<string>(), SecondFile = new List<string>();
                string[] files = new string[0];
                int Diff_Loc = 0;
                string log = "";
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
                    catch (Exception e)
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
                    for (int i = 0; i < Second.Map.Count() - 1; i++)
                    {
                        if (First.Map[i] == First.Compressed.IndexOf("NewLine"))// determines what line the program is 
                        {
                            Diff_Loc++;
                        }

                        if (First.Compressed[First.Map[i]] != Second.Compressed[Second.Map[i]]) // if the two words dont match
                        {
                            Console.WriteLine($"\n whats being compared {First.Compressed[First.Map[i]]}, {Second.Compressed[Second.Map[i]]}");// this statement shows you whats being compared
                            (string, bool) result = Highlighter(First.ReproduceSection(i), Second.ReproduceSection(i), Diff_Loc, i);
                            log += result.Item1;
                            failed = true;

                        }

                    }
                    if (!failed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{files[1]} and {files[2]} are not different\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        SaveLogFile(log);
                    Console.WriteLine("\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n {e.Message}");
                }
            }
            
        }

        public static (string,bool) Highlighter(List<string> ReferenceFile, List<string> SecondFile,int line,int index)
        {

            int marker = 0;
            bool addition = true;
            string concat = "";

            Console.Write($"Line: {line} \n");// print line
            for(int x =0; x <= ReferenceFile.Count-1; x++) // look throuhg the list for a difference
            {
                if(ReferenceFile[x]!= SecondFile[x])
                {
                    concat = "+";
                    if(ReferenceFile[x] == SecondFile[x +1])
                        Console.Write("+ ");
                    else
                    {
                        Console.Write("- ");
                        addition = false;
                        concat = "-";
                    }
                    marker = x;// save location of in difference
                    break;
                }

            }

            for(int i = 0; i <= ReferenceFile.Count-1; i++)
            {
                if(i == marker)// once marker has been met
                {
                    if (addition)// decide twhat color to highlight the word
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{SecondFile[i]} ");
                        concat += SecondFile[i];
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{ReferenceFile[i]} ");
                        concat += ReferenceFile[i];
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else//else just print in white 
                {
                    Console.Write($"{ReferenceFile[i]} ");
                    concat += ReferenceFile[i];
                }

            }

            return (concat,addition);
            
        }
        public static void SaveLogFile(string change)
        {
            File.WriteAllText(@"Log.txt", change);
        }

    }

}
