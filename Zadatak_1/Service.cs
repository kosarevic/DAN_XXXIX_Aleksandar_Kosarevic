using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void MusicPlayer()
        {

        }

    }
}
