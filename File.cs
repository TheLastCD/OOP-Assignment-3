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
        //Variables the contain the list of words and the map they correlate too
        //as well as the number of lines in the file.
        public List<string> Compressed = new List<string>();
        public List<int> Map = new List<int>();
        public int Lines;


        // Constructor used to Compress thee text file when said file ist instantiated as an object
        public Txt_File(List<string> f)
        {
            Compressed = Compress(f);

        }

        //Method Name: Compress
        //Return: List<(string,int)>
        //Puporse: tCompresses the file into a map and list this allows for easy and quick method tetsing if the files are the same
        private List<string> Compress(List<string> file)
        {
            List<string> compressedFile = new List<string>();

            foreach (string paragraphs in file)
            {
                string[] words = paragraphs.Split();
                
                foreach(string word in words)
                {
                    if (!(compressedFile.Contains(word)))
                        compressedFile.Add(word);
                    Map.Add(compressedFile.IndexOf(word));

                }
                if (!(compressedFile.Contains("NewLine")))
                    compressedFile.Add("NewLine");
                Lines++;
                Map.Add(compressedFile.IndexOf("NewLine"));
               
            }
            return compressedFile;
        }

        //Method Name:ReproduceSection
        //Return: List<string>
        //Purpose: to decompress a snippet of the file so that it can be displayed to the user.
        public List<string> ReproduceSection(int CentrePosition)
        {

            int cap = CentrePosition + 2;
            List<string> section = new List<string>();
            for (int count = CentrePosition - 3; count <= cap; count++)
            {
                if(Compressed[Map[count]]== "NewLine")
                {
                    if (count > CentrePosition)
                        break;
                    else
                    {
                        section.RemoveRange(0, section.IndexOf(section.Last()) - 1);
                    }
                }
                else
                    section.Add(Compressed[Map[count]]);
            }

            return section;
        }
        public void Remove(bool positive, int index)
        {

        }
        
        


    }
}
