using bestmovies_academy.web.Models;
using Microsoft.EntityFrameworkCore;

namespace bestmovies_academy.web.Data
{
    public class MoviesContext: DbContext
    {
        public DbSet<MovieModel> Movies{ get; set; }
        public MoviesContext(DbContextOptions options): base(options){}
    }
}