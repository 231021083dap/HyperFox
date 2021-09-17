using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;

namespace WebApi.Repositories
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetAll();
    }

    public class FilmRepository : IFilmRepository
    {
        private readonly WebApiContext _context;

        public FilmRepository(WebApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Film> GetAll()
        {
            return _context.Film;
        }
    }
}
