using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Song
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime Length { get; set; }

        public Song(int id, string author, string title, DateTime length)
        {
            Id = id;
            Author = author;
            Title = title;
            Length = length;
        }

        public Song()
        {
        }
    }
}
