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
        /// <summary>
        /// Method reads all songs and displays them on console.
        /// </summary>
        public void ReadAllSongs()
        {
            foreach (Song s in Program.songs)
            {
                Console.WriteLine("{0} {1} {2}", s.Author, s.Title, s.Length);
            }
        }
        /// <summary>
        /// Method adds new song to application.
        /// </summary>
        public void AddNewSong()
        {
            string input;
            Song s = new Song();
            s.Id = ++Program.Id;
            //Author name is assigned.
            while (true)
            {
                Console.WriteLine("Please insert song author.");
                input = Console.ReadLine();
                s.Author = input;
                if (s.Author == "")
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }
                else
                {
                    break;
                }
            }
            //Song title is assigned.
            while (true)
            {
                Console.WriteLine("Please insert song title.");
                input = Console.ReadLine();
                s.Title = input;
                if (s.Title == "")
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }
                else
                {
                    break;
                }
            }
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            //Song duration is assigned bellow.
            while (true)
            {
                Console.WriteLine("Please insert duration of the song. (hh)");
                bool success = int.TryParse(Console.ReadLine(), out hours);
                if (!success || hours < 0)
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }
                else break;
            }
            while (true)
            {
                Console.WriteLine("Please insert duration of the song. (mm)");
                bool success = int.TryParse(Console.ReadLine(), out minutes);
                if (!success || minutes < 0 || minutes > 59)
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }
                else break;
            }
            while (true)
            {
                Console.WriteLine("Please insert duration of the song. (ss)");
                bool success = int.TryParse(Console.ReadLine(), out seconds);
                if (!success || seconds < 0 || seconds > 59)
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }
                else break;
            }
            //Length is made in form of timespan.
            s.Length = new TimeSpan(hours, minutes, seconds);
            Program.songs.Add(s);
            Console.WriteLine();
            Console.WriteLine("Song successfully created.");
        }
        /// <summary>
        /// Method simulates music player, mixed with commercials.
        /// </summary>
        public static void MusicPlayer()
        {
            string input;
            Song chosen = null;
            while (true)
            {
                //First all songs present in player are being displayed.
                foreach (Song s in Program.songs)
                {
                    Console.WriteLine(s.Id + ". " + s.Author + " " + s.Title + " " + s.Length);
                }
                //User choses one from the list.
                Console.WriteLine();
                Console.WriteLine("Select song you wish to play. (input number from the list)");
                bool success = int.TryParse(Console.ReadLine(), out int id);
                //Program checks if chosen song exists in the list.
                foreach (Song song in Program.songs)
                {
                    if (song.Id == id)
                    {
                        chosen = song;
                    }
                }

                if (chosen == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input, please try again.\n");
                }
                else break;
            }
            Console.WriteLine();
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + chosen.Title + "\n");
            //Song playing starts here.
            Thread t1 = new Thread(() => PlaySong(chosen));
            t1.Start();
            t1.Join();

        }
        /// <summary>
        /// Method made exclusively to be invoked by thread while music is playing.
        /// </summary>
        /// <param name="song"></param>
        public static void PlaySong(Song song)
        {
            //Stopwatch detects when song duration expires.
            Stopwatch s = new Stopwatch();
            s.Start();
            //Boolean made to initiate commercials thread only once.
            bool start = true;
            Thread t = new Thread(Commercials);
            Console.WriteLine("Press ESCAPE key to stop playing.\n");
            //Loop iterates as long as elapsed time is less than duration of the song.
            while (s.Elapsed < song.Length && !Console.KeyAvailable)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Song is still playing...");
                Console.ResetColor();
                if (start)
                {
                    //Commercials thread is initiated only once when song starts.
                    t.Start();
                    start = false;
                }
                Thread.Sleep(1000);
            }
            //Commercials thread is aborted when song finishes playing.
            t.Abort();
            s.Stop();
            Console.WriteLine();
            Console.WriteLine("Song has finished playing.");
            string input;
            Console.WriteLine();
            Console.WriteLine("Play another song or exit player? (y/n)");
            input = Console.ReadLine();
            //If user inserts "y", player will run again.
            if (input == "y")
            {
                Program.ewh.Set();
            }
            //Otherwise player will exit.
            else
            {
                Program.replay = false;
                Program.ewh.Set();
            }
        }
        /// <summary>
        /// Delegate made for notifying user that player has stopped playing.
        /// </summary>
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
        /// <summary>
        /// Method made to iterate commercials while music is playing.
        /// </summary>
        public static void Commercials()
        {
            Random r = new Random();
            //Random commercials run continously every 200ms.
            while (true)
            {
                Thread.Sleep(200);
                Console.WriteLine(Program.commercials[r.Next(0, Program.commercials.Count)]);
            }
        }

    }
}
