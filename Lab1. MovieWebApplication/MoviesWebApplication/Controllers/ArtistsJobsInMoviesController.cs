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
            ViewBag.CurrentArtist=currentArtist;

            var movieDBContext = await _context.ArtistsJobsInMovies
                .Where(am => am.ArtistId == artistId)
                .Include(am => am.Movie).ToListAsync();

            return View(movieDBContext);
        }

        // GET: ArtistsJobsInMovies/Create

        [Authorize(Roles = "admin")]
        public IActionResult Create(int currentArtistId)
        {
            var currentArtist = _context.Artists.FirstOrDefault(a => a.Id == currentArtistId);
            var currentArtistJobsInMoviesIds = _context.ArtistsJobsInMovies.Where(am => am.ArtistId == currentArtistId).Select(am => am.MovieId);
            ViewBag.CurrentArtist = currentArtist;
            ViewBag.CurrentArtistName = currentArtist.Name;
            ViewBag.MoviesList = new SelectList(_context.Movies.Where(m => !currentArtistJobsInMoviesIds.Contains(m.Id)), "Id", "Name");
            return View();
        }

        // POST: ArtistsJobsInMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRes([Bind("Id,MovieId,ArtistId,Job")] ArtistsJobsInMovie artistsJobsInMovie)
        {
            Artist artist = _context.Artists.FirstOrDefault(x => x.Id == artistsJobsInMovie.ArtistId);
            if (ModelState.IsValid)
            {
                Movie movie = _context.Movies.FirstOrDefault(x => x.Id == artistsJobsInMovie.MovieId);
                artistsJobsInMovie.Movie = movie;
                artistsJobsInMovie.Artist = artist;
                _context.Add(artistsJobsInMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ArtistMoviesList), new { artistId = artist.Id });
            }
            var currentArtistJobsInMoviesIds = _context.ArtistsJobsInMovies.Where(am => am.ArtistId == artist.Id).Select(am => am.MovieId);
            ViewBag.ArtistId = artist.Id;
            ViewBag.ArtistName = artist.Name;
            ViewBag.MoviesList = new SelectList(_context.Movies.Where(m => !currentArtistJobsInMoviesIds.Contains(m.Id)), "Id", "Name");
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

            ViewBag.CurrentArtist = artistsJobsInMovie.Artist;

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
            return RedirectToAction(nameof(ArtistMoviesList), new { artistId = artistsJobsInMovie.ArtistId });
        }

        private bool ArtistsJobsInMovieExists(int id)
        {
            return _context.ArtistsJobsInMovies.Any(e => e.Id == id);
        }
    }
}
