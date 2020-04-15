using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;

namespace OOP_Assignment_2_Code_Review
{
    class Txt_File
    {
        public List<string> file;
        public List<(string, int)> Compressed;
        List<int> Map = new List<int>();



        public Txt_File(List<string> f)
        {
            file = f;
            Compressed = Compress();
        }

        //Method Name: Get_List
        //Return: List<(string,int)>
        //Puporse: to compress the file using a lossless form of text compression
        private List<(string, int)> Compress()
        {
            Dictionary<string, int> compressedFile = new Dictionary<string, int>();

            foreach (string paragraphs in file)
            {
                string[] words = paragraphs.Split();
                
                foreach(string word in words)
                {
                    if (compressedFile.ContainsKey(word))
                        compressedFile[word] += 1;

                    else
                        compressedFile.Add(word, 1);

                    Map.Add(compressedFile.Keys.ToList().IndexOf(word));

                }
               
            }
            List<(string, int)> CompressedList = compressedFile.Select(x => (x.Key,x.Value)).ToList();

            return CompressedList;
        }
        public void Reproduce()
        {
            bool finished = true;
            int count = 0;
            while (finished)
            {
                Console.Write($" {Compressed[Map[count]].Item1}");
                count++;

            }
        }


    }
}
