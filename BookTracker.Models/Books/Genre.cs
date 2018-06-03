using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        public Genre()
        { }

        public Genre(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Genre)
            {
                return (obj as Genre).Name == Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
