using BookTracker.DAL.Books;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public class GenreLogic : IGenreLogic
    {
        private IGenreRepository genreRepo;

        public GenreLogic(IGenreRepository genreRepo)
        {
            this.genreRepo = genreRepo;
        }

        public async Task<IEnumerable<Genre>> SaveGenresAsync(IEnumerable<Genre> genres)
        {
            var savedGenres = new HashSet<Genre>(await this.genreRepo.GetGenresByNameAsync(genres.Select(x => x.Name)));
            var unsavedGenres = new List<Genre>();

            foreach(var genre in genres)
            {
                if (!savedGenres.Contains(genre))
                {
                    unsavedGenres.Add(genre);
                }
            }

            var newSavedGenres = (await this.genreRepo.SaveGenresAsync(unsavedGenres)).ToList();

            newSavedGenres.AddRange(savedGenres);

            return newSavedGenres;
        }
    }
}
