using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers;

public class BookController : Controller
{
    private readonly LibraryDbContext _libraryDbContext;

    public BookController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }


    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(BookFilterModel filter = null)
    {
        filter ??= new BookFilterModel();

        var bookQuery = _libraryDbContext.Books.Include(b => b.BookGenre).AsQueryable();

        // Add filtering conditions
        if (!string.IsNullOrWhiteSpace(filter.Title))
            bookQuery = bookQuery.Where(b => b.Title.ToLower().Contains(filter.Title.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.Description))
            bookQuery = bookQuery.Where(b => b.Description.ToLower().Contains(filter.Description.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.Genre))
            bookQuery = bookQuery.Where(b => b.BookGenre != null && b.BookGenre.Name.ToLower().Contains(filter.Genre.ToLower()));

        var model = await bookQuery.ToListAsync();
        return PartialView("_IndexTable", model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _libraryDbContext
            .Books
            .Include(b => b.BookGenre)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }
        FillDropdownValues();
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book book)
    {
        if (ModelState.IsValid)
        {
            _libraryDbContext.Update(book);
            await _libraryDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        FillDropdownValues();
        return View(book);
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var book = await _libraryDbContext.Books
            .Include(b => b.BookGenre)
            .Include(b => b.AuthorBooks)
            .ThenInclude(ab => ab.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _libraryDbContext.Books.FindAsync(id);
        if (book != null)
        {
            _libraryDbContext.Books.Remove(book);
            await _libraryDbContext.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void FillDropdownValues()
    {
        var selectItems = new List<SelectListItem>();

        var listItem = new SelectListItem();
        listItem.Text = "- odaberite -";
        listItem.Value = "";
        selectItems.Add(listItem);

        foreach (var genre in _libraryDbContext.Genres)
        {
            listItem = new SelectListItem(genre.Name, genre.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.PossibleGenres = selectItems;
    }

}
