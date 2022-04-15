#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            ViewBag.CurrentGenreName = currentGenre.Name;

            var movieDBContext = await _context.MoviesGenres
                .Where(mg => mg.GenreId == genreId)
                .Include(mg => mg.Movie).ToListAsync();

            return View(movieDBContext);
        } 

        // GET: MoviesGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviesGenre = await _context.MoviesGenres
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesGenre == null)
            {
                return NotFound();
            }

            return View(moviesGenre);
        }

        // GET: MoviesGenres/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name");
            return View();
        }

        // POST: MoviesGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,GenreId")] MoviesGenre moviesGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moviesGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", moviesGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", moviesGenre.MovieId);
            return View(moviesGenre);
        }

        // GET: MoviesGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviesGenre = await _context.MoviesGenres.FindAsync(id);
            if (moviesGenre == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", moviesGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", moviesGenre.MovieId);
            return View(moviesGenre);
        }

        // POST: MoviesGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,GenreId")] MoviesGenre moviesGenre)
        {
            if (id != moviesGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moviesGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesGenreExists(moviesGenre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", moviesGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", moviesGenre.MovieId);
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
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesGenre == null)
            {
                return NotFound();
            }

            return View(moviesGenre);
        }

        // POST: MoviesGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviesGenre = await _context.MoviesGenres.FindAsync(id);
            _context.MoviesGenres.Remove(moviesGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesGenreExists(int id)
        {
            return _context.MoviesGenres.Any(e => e.Id == id);
        }
    }
}
