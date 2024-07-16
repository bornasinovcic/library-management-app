using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryManagementApp.Web.Controllers;

public class GenreController(LibraryDbContext libraryDbContext) : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(GenreFilterModel filter = null)
    {
        filter ??= new GenreFilterModel();

        var genreQuery = libraryDbContext.Genres.AsQueryable();

        // Add filtering conditions
        if (!string.IsNullOrWhiteSpace(filter.Name))
            genreQuery = genreQuery.Where(g => g.Name.ToLower().Contains(filter.Name.ToLower()));

        var model = await genreQuery.ToListAsync();
        return PartialView("_IndexTable", model);
    }


    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Genre model)
    {
        if (ModelState.IsValid)
        {
            libraryDbContext.Genres.Add(model);
            await libraryDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);

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
            var existingGenre = await libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == model.Id);

            if (existingGenre != null)
            {
                existingGenre.Name = model.Name;
                await libraryDbContext.SaveChangesAsync();
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
        try
        {
            var genre = await libraryDbContext.Genres.FindAsync(id);
            if (genre != null)
            {
                libraryDbContext.Genres.Remove(genre);
                await libraryDbContext.SaveChangesAsync();

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = true });
                else
                    return RedirectToAction("Index");
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = false, message = "Genre not found." });
            else
                TempData["ErrorMessage"] = "Genre not found.";
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
        {
            // Foreign key constraint violation
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = false, message = "Unable to delete genre. It is being used somewhere in the application." });
            else
                TempData["ErrorMessage"] = "Unable to delete genre. It is being used somewhere in the application.";
        }
        catch (Exception ex)
        {
            // Log the exception (ex)
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = false, message = "An unexpected error occurred. Please try again later." });
            else
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
        }

        return RedirectToAction("Index");
    }


    [HttpGet("Type/Details")]
    public async Task<IActionResult> Details(int? id = null)
    {
        Genre model = null;

        if (id != null)
        {
            model = await libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        return View(model);
    }
}
