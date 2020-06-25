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
            while (true)
            {
                Console.WriteLine("Please insert song author.");
                input = Console.ReadLine();
                s.Author = input;
                if(s.Author == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }
                else
                {
                    break;
                }
            }
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
            bool start = true;
            Thread t = new Thread(Commercials);
            while (s.Elapsed < song.Length)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Song is still playing...");
                Console.ResetColor();
                if (start)
                {
                    t.Start();
                    start = false;
                }
                Thread.Sleep(1000);
            }
            t.Abort();
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

        public delegate void Notification();

        public event Notification OnNotification;

        public void PlayerStopped()
        {
            OnNotification += () =>
            {
                Console.WriteLine("Music player has stopped.\n");
            };
            OnNotification.Invoke();
        }

        public static void Commercials()
        {
            Random r = new Random();
            while (true)
            {
                Thread.Sleep(200);
                Console.WriteLine(Program.commercials[r.Next(0,Program.commercials.Count)]);
            }
        }

    }
}
