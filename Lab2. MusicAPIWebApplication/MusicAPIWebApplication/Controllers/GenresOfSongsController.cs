using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPIWebApplication.Models;

namespace MusicAPIWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresOfSongsController : ControllerBase
    {
        private readonly MusicAPIContext _context;

        public GenresOfSongsController(MusicAPIContext context)
        {
            _context = context;
        }

        // GET: api/GenresOfSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenresOfSong>>> GetGenresOfSongs()
        {
          if (_context.GenresOfSongs == null)
          {
              return NotFound();
          }
            return await _context.GenresOfSongs.ToListAsync();
        }

        // GET: api/GenresOfSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenresOfSong>> GetGenresOfSong(int id)
        {
          if (_context.GenresOfSongs == null)
          {
              return NotFound();
          }
            var genresOfSong = await _context.GenresOfSongs.FindAsync(id);

            if (genresOfSong == null)
            {
                return NotFound();
            }

            return genresOfSong;
        }

        // PUT: api/GenresOfSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenresOfSong(int id, GenresOfSong genresOfSong)
        {
            if (id != genresOfSong.Id)
            {
                return BadRequest();
            }

            _context.Entry(genresOfSong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresOfSongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GenresOfSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenresOfSong>> PostGenresOfSong(GenresOfSong genresOfSong)
        {
          if (_context.GenresOfSongs == null)
          {
              return Problem("Entity set 'MusicAPIContext.GenresOfSongs'  is null.");
          }
            _context.GenresOfSongs.Add(genresOfSong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenresOfSong", new { id = genresOfSong.Id }, genresOfSong);
        }

        // DELETE: api/GenresOfSongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenresOfSong(int id)
        {
            if (_context.GenresOfSongs == null)
            {
                return NotFound();
            }
            var genresOfSong = await _context.GenresOfSongs.FindAsync(id);
            if (genresOfSong == null)
            {
                return NotFound();
            }

            _context.GenresOfSongs.Remove(genresOfSong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenresOfSongExists(int id)
        {
            return (_context.GenresOfSongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
