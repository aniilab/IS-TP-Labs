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
    public class ArtistsJobsInMoviesController : Controller
    {
        private readonly MovieDBContext _context;

        public ArtistsJobsInMoviesController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: ArtistsJobsInMovies
        public async Task<IActionResult> ArtistMoviesList(int artistId)
        {
            var currentArtist = _context.Artists.FirstOrDefault(a => a.Id == artistId);
            ViewBag.CurrentArtistName = currentArtist.Name;

            var movieDBContext = await _context.ArtistsJobsInMovies
                .Where(am => am.ArtistId == artistId)
                .Include(am => am.Movie).ToListAsync();

            return View(movieDBContext);
        }

        // GET: ArtistsJobsInMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistsJobsInMovie = await _context.ArtistsJobsInMovies
                .Include(a => a.Artist)
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistsJobsInMovie == null)
            {
                return NotFound();
            }

            return View(artistsJobsInMovie);
        }

        // GET: ArtistsJobsInMovies/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name");
            return View();
        }

        // POST: ArtistsJobsInMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,ArtistId,Job")] ArtistsJobsInMovie artistsJobsInMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artistsJobsInMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistsJobsInMovie.ArtistId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", artistsJobsInMovie.MovieId);
            return View(artistsJobsInMovie);
        }

        // GET: ArtistsJobsInMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistsJobsInMovie = await _context.ArtistsJobsInMovies.FindAsync(id);
            if (artistsJobsInMovie == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistsJobsInMovie.ArtistId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", artistsJobsInMovie.MovieId);
            return View(artistsJobsInMovie);
        }

        // POST: ArtistsJobsInMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,ArtistId,Job")] ArtistsJobsInMovie artistsJobsInMovie)
        {
            if (id != artistsJobsInMovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistsJobsInMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistsJobsInMovieExists(artistsJobsInMovie.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistsJobsInMovie.ArtistId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", artistsJobsInMovie.MovieId);
            return View(artistsJobsInMovie);
        }

        // GET: ArtistsJobsInMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistsJobsInMovie = await _context.ArtistsJobsInMovies
                .Include(a => a.Artist)
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistsJobsInMovie == null)
            {
                return NotFound();
            }

            return View(artistsJobsInMovie);
        }

        // POST: ArtistsJobsInMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistsJobsInMovie = await _context.ArtistsJobsInMovies.FindAsync(id);
            _context.ArtistsJobsInMovies.Remove(artistsJobsInMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistsJobsInMovieExists(int id)
        {
            return _context.ArtistsJobsInMovies.Any(e => e.Id == id);
        }
    }
}
