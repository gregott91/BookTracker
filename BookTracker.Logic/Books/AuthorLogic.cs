using BookTracker.DAL.Books;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public class AuthorLogic : IAuthorLogic
    {
        private IAuthorRepository authorRepo;

        public AuthorLogic(IAuthorRepository authorRepo)
        {
            this.authorRepo = authorRepo;
        }

        public async Task<IEnumerable<Author>> SaveAuthorsAsync(IEnumerable<Author> authors)
        {
            var savedAuthors = new HashSet<Author>(await this.authorRepo.GetAuthorsByNameAsync(authors.Select(x => x.Name)));
            var unsavedAuthors = new List<Author>();

            foreach (var author in authors)
            {
                if (!savedAuthors.Contains(author))
                {
                    unsavedAuthors.Add(author);
                }
            }

            var newSavedAuthors = (await this.authorRepo.SaveAuthorsAsync(unsavedAuthors)).ToList();

            newSavedAuthors.AddRange(savedAuthors);

            return newSavedAuthors;
        }
    }
}
