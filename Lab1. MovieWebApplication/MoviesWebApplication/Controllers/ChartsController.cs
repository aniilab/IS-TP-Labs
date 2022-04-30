using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoviesWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly MovieDBContext _context;
        public ChartsController(MovieDBContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var productions = _context.Productions.Include(p => p.Movies).ToList();
            List<object> prodMovie = new List<object>();
            prodMovie.Add(new[] { "Кінокомпанія", "Кількість фільмів" });
            foreach (var p in productions)
            {
                prodMovie.Add(new object[] { p.Name, p.Movies.Count() });
            }
            return new JsonResult(prodMovie);

        }

        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            var genres = _context.Genres.Include(g=>g.MoviesGenres).ToList();
            List<object> genMovie = new List<object>();
            genMovie.Add(new[] { "Жанр", "Кількість фільмів" });
            foreach (var g in genres)
            {
                genMovie.Add(new object[] { g.Name, g.MoviesGenres.Count() });
            }
            return new JsonResult(genMovie);

        }
    }
}
