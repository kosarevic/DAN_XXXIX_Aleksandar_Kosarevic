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
        public static List<Song> songs = new List<Song>();
        public static int Id = 0;

        static void Main(string[] args)
        {
            Read();
            string option = "";
            do
            {
                Console.WriteLine("1. Read all songs.");
                Console.WriteLine("2. Add new song.");
                Console.WriteLine("3. Music player.");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.WriteLine("Chose an option:");
                option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        Service s = new Service();
                        s.ReadAllSongs();
                        Console.WriteLine();
                        break;
                    case "2":
                        s = new Service();
                        s.AddNewSong();
                        Write();
                        Console.WriteLine();
                        break;
                    case "3":
                        s = new Service();
                        break;
                    default:
                        Console.WriteLine("Incorect input, please try again.\n");
                        break;
                }

            } while (option != "4");
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
            string[] output = new string[songs.Count];
            string line = "";

            for (int i = 0; i < songs.Count; i++)
            {
                line = "[" + songs[i].Author + "]:[" + songs[i].Title + "][" + songs[i].Length + "]";
                output[i] = line;
            }
            File.WriteAllLines("..//../Files/Music.txt", output);
        }
    }
}
