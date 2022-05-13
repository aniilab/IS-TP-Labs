#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesWebApplication;

namespace MoviesWebApplication.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly MovieDBContext _context;

        public ProductionsController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: Productions
        public async Task<IActionResult> ProductionList()
        {
            return View(await _context.Productions.ToListAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            var currentProduction = _context.Productions.FirstOrDefault(p => p.Id == id);

            ViewBag.CurrentProductionName = currentProduction.Name;
            ViewBag.Sum = from movie in _context.Movies group movie by movie.Production into prodByMovie select new { Production = prodByMovie.Key, TotalDuration = prodByMovie.Sum(movie => movie.Duration) };

            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country")] Production production)
        {
            if (ModelState.IsValid)
            {
                _context.Add(production);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProductionList));
            }
            return View(production);
        }

        // GET: Productions/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ProductionList));
            }
            return View(production);
        }

        // GET: Productions/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
    .Include(p => p.Movies)
    .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            _context.Productions.Remove(production);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProductionList));
        }

        private bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            RemoveUnmentionedProductions(workBook.Worksheets.Select(w => w.Name.ToLower()).ToList());
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                ProcessImportedProduction(worksheet);
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ProductionList));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var productions = _context.Productions.Include("Movies").ToList();

                foreach (var p in productions)
                {
                    var worksheet = workbook.Worksheets.Add(p.Name);

                    worksheet.Cell("A1").Value = "Назва";
                    worksheet.Cell("B1").Value = "Тривалість фільму";
                    worksheet.Cell("C1").Value = "Чи є оскар";
                    worksheet.Cell("D1").Value = "Жанри";
                    worksheet.Row(1).Style.Font.Bold = true;
                    var movies = p.Movies.ToList();

                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for (int i = 0; i < movies.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = movies[i].Name;
                        worksheet.Cell(i + 2, 2).Value = movies[i].Duration;
                        worksheet.Cell(i + 2, 3).Value = movies[i].HasOscar;

                        var mgg = _context.MoviesGenres.Where(mg => mg.MovieId == movies[i].Id).Include("Genre").ToList();

                        int j = 0;
                        foreach (var g in mgg)
                        {
                            worksheet.Cell(i + 2, 4 + j).Value = g.Genre.Name;
                            j++;
                        }
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        //змініть назву файла відповідно до тематики Вашого проєкту

                        FileDownloadName = $"MovieBase_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        private void RemoveUnmentionedProductions(List<string> productionNamesInLowerCase)
        {
            var productions = _context.Productions.Where(p =>
                !productionNamesInLowerCase.Contains(p.Name.ToLower())).ToList();

            foreach(var production in productions)
            {
                _context.Productions.Remove(production);
            }
        }

        private void ProcessImportedProduction(IXLWorksheet worksheet)
        {
            //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
            Production production = _context.Productions
                .FirstOrDefault(p => p.Name.ToLower() == worksheet.Name.ToLower());

            if (production == null)
            {
                production = new Production();
                production.Name = worksheet.Name;
                production.Country = "from Excel";

                //додати в контекст
                _context.Productions.Add(production);
            }

            RemoveUnmentionedMovies(
                worksheet.RowsUsed().Skip(1).Select(r => r.Cell(1).GetString().ToLower()).ToList(),
                production.Id);

            //перегляд усіх рядків                    
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                ProcessImportedMovie(row, production);
            }
        }

        private void RemoveUnmentionedMovies(List<string> movieNamesInLowerCase, int productionId)
        {
            var movies = _context.Movies.Where(m =>
                !movieNamesInLowerCase.Contains(m.Name.ToLower()) && m.ProductionId == productionId).ToList();

            foreach (var movie in movies)
            {
                _context.Movies.Remove(movie);
            }
        }

        private void ProcessImportedMovie(IXLRow row, Production production)
        {
            Movie movie =  _context.Movies
                .Include(m => m.MoviesGenres)
                .FirstOrDefault(m => m.Name.ToLower() == row.Cell(1).GetString().ToLower());

            if (movie != null)
            {
                movie.Duration = row.Cell(2).GetValue<int>();
                movie.HasOscar = row.Cell(3).GetValue<bool>();
                movie.MoviesGenres = new List<MoviesGenre>();
                _context.Movies.Update(movie);
            }
            else
            {
                movie = new Movie();
                movie.Name = row.Cell(1).GetString();
                movie.Duration = row.Cell(2).GetValue<int>();
                movie.HasOscar = row.Cell(3).GetValue<bool>();
                movie.Production = production;
                movie.MoviesGenres = new List<MoviesGenre>();
                _context.Movies.Add(movie);
            }

            //у разі наявності жанра знайти його, у разі відсутності - додати
            int i = 4;
            while (!string.IsNullOrWhiteSpace(row.Cell(i).GetString()))
            {
                ProcessImportedGenre(row.Cell(i).GetString(), movie);
                i++;
            }
        }

        private void ProcessImportedGenre(string genreName, Movie movie)
        {
            Genre genre = _context.Genres
                .FirstOrDefault(g => g.Name.ToLower() == genreName.ToLower());

            if (genre == null)
            {
                genre = new Genre();
                genre.Name = genreName;
                _context.Add(genre);
            }

            MoviesGenre movieGenre = new MoviesGenre();

            movieGenre.Movie = movie;
            movieGenre.Genre = genre;
            _context.MoviesGenres.Add(movieGenre);
        }
    }
}
