using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    /// <summary>
    /// Program simulates music player.
    /// </summary>
    class Program
    {
        public static List<Song> songs = new List<Song>();
        public static int Id = 0;
        public static EventWaitHandle ewh = new AutoResetEvent(false);
        public static bool replay = true;
        public static List<string> commercials = new List<string>();

        static void Main(string[] args)
        {
            //Read method called to pull all data from text file (songs and commercials).
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
                        //Reading all songs avalaible start here.
                        Service s = new Service();
                        s.ReadAllSongs();
                        Console.WriteLine();
                        break;
                    case "2":
                        //Adding new song starts here.
                        s = new Service();
                        s.AddNewSong();
                        Write();
                        Console.WriteLine();
                        break;
                    case "3":
                        //Playing and replaying songs starts here.
                        //Boolean replay serves the purpose if player whants restart music player.
                        replay = true;
                        while (replay)
                        {
                            Task player = new Task(Service.MusicPlayer);
                            player.Start();
                            //EventWaitHandle stops the loop from iterrating untill user decides to play again or cancel.
                            ewh.WaitOne();
                            Console.WriteLine();
                        }
                        s = new Service();
                        //Delegate informs user that player have stopped working.
                        s.PlayerStopped();
                        break;
                    default:
                        Console.WriteLine("Incorect input, please try again.\n");
                        break;
                }

            } while (option != "4");
        }
        /// <summary>
        /// Method responsible for reading data from text files.
        /// </summary>
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
                    string[] temp = t.Split(':', '[', ']');
                    Song s = new Song();
                    s.Id = ++Id;
                    s.Author = temp[1];
                    s.Title = temp[4];
                    s.Length = new TimeSpan(int.Parse(temp[6]), int.Parse(temp[7]), int.Parse(temp[8]));
                    songs.Add(s);
                }
            }
            sr = new StreamReader("..//../Files/Commercials.txt");
            while ((line = sr.ReadLine()) != null)
            {
                commercials.Add(line);
            }
            sr.Close();
        }
        /// <summary>
        /// Method responsible for writing all data to text files.
        /// </summary>
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
