using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Service
    {

        public void ReadAllSongs()
        {
            foreach (Song s in Program.songs)
            {
                Console.WriteLine("{0} {1} {2}", s.Author, s.Title, s.Length);
            }
        }

        public void AddNewSong()
        {
            string input;
            Song s = new Song();
            s.Id = ++Program.Id;
            Console.WriteLine("Please insert song author.");
            input = Console.ReadLine();
            s.Author = input;
            Console.WriteLine("Please insert song title.");
            input = Console.ReadLine();
            s.Title = input;
            Console.WriteLine("Please insert duration of the song. (hh)");
            int hours = int.Parse(Console.ReadLine());
            Console.WriteLine("Please insert duration of the song. (mm)");
            int minutes = int.Parse(Console.ReadLine());
            Console.WriteLine("Please insert duration of the song. (ss)");
            int seconds = int.Parse(Console.ReadLine());
            s.Length = new TimeSpan(hours, minutes, seconds);
            Program.songs.Add(s);
        }

        public static void MusicPlayer()
        {
            string input;
            foreach (Song s in Program.songs)
            {
                Console.WriteLine(s.Id + ". " + s.Author + " " + s.Title + " " + s.Length);
            }
            Console.WriteLine();
            Console.WriteLine("Select song you wish to play.");
            input = Console.ReadLine();
            Song chosen = new Song();
            foreach (Song song in Program.songs)
            {
                if(song.Id == int.Parse(input))
                {
                    chosen = song;
                }
            }
            Console.WriteLine();
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + chosen.Title + "\n");

            Thread t1 = new Thread(() => PlaySong(chosen));
            t1.Start();
            t1.Join();

        }

        public static void PlaySong(Song song)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < song.Length)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Song is still playing...");
            }
            s.Stop();
            Console.WriteLine();
            Console.WriteLine("Song has finished playing.");
            string input;
            Console.WriteLine();
            Console.WriteLine("Play another song or exit player? (y/n)");
            input = Console.ReadLine();
            if(input == "y")
            {
                Program.ewh.Set();
            }
            else
            {
                Program.replay = false;
                Program.ewh.Set();
            }
        }

    }
}
