using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static List<Song> songs = new List<Song>();
        static int Id = 0;

        static void Main(string[] args)
        {
            Read();
            string option = "";
            do
            {
                Console.WriteLine("1. Read all songs.");
                Console.WriteLine("2. Add new song.");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.WriteLine("Chose an option:");
                option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        foreach (Song s in songs)
                        {
                            Console.WriteLine("{0} {1} {2}",s.Author, s.Title, s.Length);
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Insert author of the song.");
                        string 
                        break;
                    default:
                        Console.WriteLine("Incorect input, please try again.\n");
                        break;
                }

            } while (option != "3");
        }

        static void Read()
        {
            List<string> text = new List<string>();

            StreamReader sr = new StreamReader("..//../Files/Music.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                text.Add(line);
            }

            sr.Close();

            if (text.Any())
            {

                foreach (string t in text)
                {
                    string[] temp = t.Split(':','[',']');
                    Song s = new Song();
                    s.Id = ++Id;
                    s.Author = temp[1];
                    s.Title = temp[4];
                    s.Length = new TimeSpan(int.Parse(temp[6]), int.Parse(temp[7]), int.Parse(temp[8]));
                    songs.Add(s);
                }
            }
        }

        static void Write()
        {

        }
    }
}
