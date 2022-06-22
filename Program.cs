using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace Semester_Project
{
    class Program
    {
        static void Main(string[] args)
        {
        step1:
            //Console asks for the name of text file and locates it on desktop

            Console.WriteLine("\nOh hi there! \n" +
                "This program does some simple text analysis for a .txt file. \n" +
                "Please, move the file that you want to analyze on your desktop and tell me its name (without extension).\n");
            string FileName = Console.ReadLine();
            string FullFileName = FileName + ".txt";
            string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string FilePath = System.IO.Path.Combine(DesktopPath, FullFileName);

            //Initial values set to 0
            int CharacterCount = 0;
            int WordCount = 0;
            int SentenceCount = 0;
            
            //Dictionaries in order to later count frequencies of characters and words
            IDictionary<string, int> dictword = new Dictionary<string, int>();
            IDictionary<char, int> dictchar = new Dictionary<char, int>();

            //I make use of exceptions in order to not crash the program and notify user about what's wrong
            try
            {
                //Copies the content of a text file into string 
                string text = File.ReadAllText(FilePath);

                //Breaking down text into sentences, sentences into words
                string[] sentences = text.Split(new char[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var sentence in sentences)
                {
                    string[] words = sentence.Split(new char[] { ' ', ',', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    int WordsInSentence = words.Length;
                    WordCount = (WordCount + WordsInSentence);

                    //Count the number of times each word is occured in the text
                    foreach (string word in words)
                    {
                        string stringlower = word.ToLower();
                        if (!dictword.ContainsKey(stringlower))
                        {
                            dictword.Add(stringlower, 0);
                        }
                        dictword[stringlower]++;
                    }


                }

                int TotalSentences = sentences.Length;
                SentenceCount = TotalSentences;


            }
            catch (Exception error)
            {
                //Says the error and goes back in the beginning
                Console.WriteLine(error.Message + "\n");
                goto step1;
            }

            string textforcharacters = File.ReadAllText(FilePath);
            char[] characters = textforcharacters.ToCharArray();
            CharacterCount = characters.Length;

            //Count the number of times each character is occured in the text
            foreach (char character in characters)
            {
                char charlower = Char.ToLower(character);
                if (!dictchar.ContainsKey(charlower))
                {
                    dictchar.Add(charlower, 0);
                }
                dictchar[charlower]++;
            }


            Console.WriteLine("\nCharacter count: " + CharacterCount);
            Console.WriteLine("\nWord count: " + (WordCount));
            Console.WriteLine("\nSentence count: " + (SentenceCount));

            Console.WriteLine("\nHere is the frequency of each letter used:");

            //Print the histogram
            foreach (KeyValuePair<char, int> kvp in dictchar)
            {
                double percent = Math.Ceiling((double)kvp.Value / characters.Length * 100);

                Console.Write(kvp.Key + " ");
                for (int i = 0; i < percent; i++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nHere is the frequency of each word used:");

            //Print the histogram
            foreach (KeyValuePair<string, int> kvp in dictword)
            {
                double percent = Math.Ceiling((double)kvp.Value / WordCount * 100);

                Console.Write(kvp.Key + " ");
                for (int i = 0; i < percent; i++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

        step2:

            Console.Beep();
            Console.WriteLine("\nDo you wish to analyze another document? \nWrite yes or no");
            string ans = Console.ReadLine();
            if (ans == "yes" || ans == "Yes")
                {
                goto step1;
                }
            
            else if (ans == "no" || ans == "No")
            {
                goto final;
            }    

            else
            {
                goto step2;
            }

        final:
            Console.WriteLine("\nPress any key to quit");
            Console.ReadKey();

            }
    }
}
