using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public Author()
        { }

        public Author(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Author)
            {
                return (obj as Author).Name == Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
