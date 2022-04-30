#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesWebApplication;

namespace MoviesWebApplication.Controllers
{
    public class MoviesGenresController : Controller 
    {
        private readonly MovieDBContext _context;

        public MoviesGenresController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: MoviesGenres
        public async Task<IActionResult> GenreMoviesList(int genreId)
        {
            var currentGenre = _context.Genres.FirstOrDefault(g => g.Id == genreId);

            if(currentGenre == null)
            {
                return NotFound();
            }

            ViewBag.CurrentGenre = currentGenre;

            var movieDBContext = await _context.MoviesGenres
                .Where(mg => mg.GenreId == genreId)
                .Include(mg => mg.Movie).ToListAsync();

            return View(movieDBContext);
        } 

        // GET: MoviesGenres/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create(int currentGenreId)
        {
            var currentGenre = _context.Genres.FirstOrDefault(g => g.Id == currentGenreId);
            var currentGenreMovieIds = _context.MoviesGenres.Where(mg => mg.GenreId == currentGenreId).Select(mg => mg.MovieId);
            ViewBag.CurrentGenre = currentGenre;
            ViewBag.MoviesList = new SelectList(_context.Movies.Where(m => !currentGenreMovieIds.Contains(m.Id)), "Id", "Name");
            return View();
        }

        // POST: MoviesGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRes([Bind("Id,MovieId,GenreId")] MoviesGenre moviesGenre)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == moviesGenre.GenreId);
            if (ModelState.IsValid)
            {
                Movie movie = _context.Movies.FirstOrDefault(x => x.Id == moviesGenre.MovieId);
                moviesGenre.Movie = movie;
                moviesGenre.Genre = genre;
                _context.Add(moviesGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GenreMoviesList), new { genreId = genre.Id });
            }
            var currentGenreMovieIds = _context.MoviesGenres.Where(mg => mg.GenreId == genre.Id).Select(mg => mg.MovieId);
            ViewBag.GenreId = genre.Id;
            ViewBag.GenreName = genre.Name;
            ViewBag.MoviesList = new SelectList(_context.Movies.Where(m => !currentGenreMovieIds.Contains(m.Id)), "Id", "Name", moviesGenre.MovieId);
            return View(moviesGenre);
        }

        // GET: MoviesGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviesGenre = await _context.MoviesGenres
                .Include(mg => mg.Genre)
                .Include(mg => mg.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesGenre == null)
            {
                return NotFound();
            }

            ViewBag.CurrentGenre = moviesGenre.Genre;

            return View(moviesGenre);
        }

        // POST: MoviesGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviesGenre = await _context.MoviesGenres
                .FindAsync(id);
            _context.MoviesGenres.Remove(moviesGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GenreMoviesList), new { genreId = moviesGenre.GenreId });
        }
    }
}
