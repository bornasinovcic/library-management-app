using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryManagementApp.Web.Controllers;

public class GenreController : Controller
{
    private readonly LibraryDbContext _libraryDbContext;

    public GenreController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(GenreFilterModel filter = null)
    {
        filter ??= new GenreFilterModel();

        var genreQuery = _libraryDbContext.Genres.AsQueryable();

        // Add filtering conditions
        if (!string.IsNullOrWhiteSpace(filter.Name))
            genreQuery = genreQuery.Where(g => g.Name.ToLower().Contains(filter.Name.ToLower()));

        var model = await genreQuery.ToListAsync();
        return PartialView("_IndexTable", model);
    }


    [HttpGet("Type/add")]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Genre model)
    {
        if (ModelState.IsValid)
        {
            _libraryDbContext.Genres.Add(model);
            await _libraryDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Genre model)
    {
        if (ModelState.IsValid)
        {
            var existingGenre = await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == model.Id);

            if (existingGenre != null)
            {
                existingGenre.Name = model.Name;
                await _libraryDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var genreToDelete = await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (genreToDelete == null)
        {
            return NotFound();
        }

        _libraryDbContext.Genres.Remove(genreToDelete);
        await _libraryDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id = null)
    {
        Genre model = null;

        if (id != null)
        {
            model = await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        return View(model);
    }
}
