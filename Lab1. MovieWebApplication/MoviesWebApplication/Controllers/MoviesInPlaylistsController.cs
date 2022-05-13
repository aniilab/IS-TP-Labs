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
    public class MoviesInPlaylistsController : Controller
    {
        private readonly MovieDBContext _context;

        public MoviesInPlaylistsController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: MoviesInPlaylists
        public async Task<IActionResult> PlaylistMovies(int playlistId)
        {
            var currentPlaylist = _context.Playlists.FirstOrDefault(p=>p.Id==playlistId);
            if (currentPlaylist == null)
            {
                return NotFound();
            }
            ViewBag.CurrentPlaylist = currentPlaylist;
            ViewBag.PlaylistUserName = _context.Playlists.Where(p => p.Id == playlistId).Select(p => p.UserName).Single();

            var movieDBContext = await _context.MoviesInPlaylists.Where(mp=>mp.PlaylistId==playlistId)
                .Include(m => m.Movie).ToListAsync();

            return View(movieDBContext);
        }

        // GET: MoviesInPlaylists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviesInPlaylist = await _context.MoviesInPlaylists
                .Include(m => m.Movie)
                .Include(m => m.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesInPlaylist == null)
            {
                return NotFound();
            }

            return View(moviesInPlaylist);
        }

        // GET: MoviesInPlaylists/Create
        public IActionResult Create(int currentPlaylistId)
        {
            var currentPlaylist = _context.Playlists.FirstOrDefault(p => p.Id == currentPlaylistId);
            var currentPlaylistMovieIds = _context.MoviesInPlaylists.Where(mp => mp.PlaylistId == currentPlaylistId).Select(mp => mp.MovieId);
            ViewBag.CurrentPlaylist = currentPlaylist;
            ViewBag.MoviesList = new SelectList(_context.Movies.Where(m => !currentPlaylistMovieIds.Contains(m.Id)), "Id", "Name");
            return View();
        }

        // POST: MoviesInPlaylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRes([Bind("Id,MovieId,PlaylistId")] MoviesInPlaylist moviesInPlaylist)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(x => x.Id == moviesInPlaylist.PlaylistId);
            if (ModelState.IsValid)
            {
                Movie movie = _context.Movies.FirstOrDefault(x => x.Id == moviesInPlaylist.MovieId);
                moviesInPlaylist.Movie = movie;
                moviesInPlaylist.Playlist = playlist;
                _context.Add(moviesInPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PlaylistMovies), new { playlistId = playlist.Id });
            }
            var currentPlaylistMovieIds = _context.MoviesInPlaylists.Where(mp => mp.PlaylistId == playlist.Id).Select(mp => mp.MovieId);
            ViewBag.CurrentPlaylist = playlist;
            ViewBag.MoviesList = new SelectList(_context.Movies.Where(m => !currentPlaylistMovieIds.Contains(m.Id)), "Id", "Name", moviesInPlaylist.MovieId);
            return View(moviesInPlaylist);
        }

        // GET: MoviesInPlaylists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviesInPlaylist = await _context.MoviesInPlaylists
                .Include(m => m.Movie)
                .Include(m => m.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesInPlaylist == null)
            {
                return NotFound();
            }
            ViewBag.CurrentPlaylist = _context.MoviesInPlaylists.Where(mp => mp.Id == id).Select(mp => mp.Playlist).Single();
            ViewBag.CurrentMovie = _context.MoviesInPlaylists.Where(mp => mp.Id == id).Select(mp => mp.Movie).Single();

            return View(moviesInPlaylist);
        }

        // POST: MoviesInPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviesInPlaylist = await _context.MoviesInPlaylists.FindAsync(id);
            _context.MoviesInPlaylists.Remove(moviesInPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PlaylistMovies), new { playlistId = moviesInPlaylist.PlaylistId });
        }

        private bool MoviesInPlaylistExists(int id)
        {
            return _context.MoviesInPlaylists.Any(e => e.Id == id);
        }
    }
}
