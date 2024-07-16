using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers;

public class BookController(LibraryDbContext libraryDbContext) : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(BookFilterModel filter = null)
    {
        filter ??= new BookFilterModel();

        var bookQuery = libraryDbContext.Books.Include(b => b.BookGenre).AsQueryable();

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
    public IActionResult Create()
    {
        FillAuthorsDropdownValues();
        FillDropdownValues();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book, int[] AuthorBooks)
    {
        if (ModelState.IsValid)
        {
            // Add the new book to the database
            libraryDbContext.Books.Add(book);
            await libraryDbContext.SaveChangesAsync();

            // Add authors to the book
            foreach (var authorId in AuthorBooks)
            {
                var authorBook = new AuthorBook
                {
                    AuthorId = authorId,
                    BookId = book.Id
                };
                libraryDbContext.AuthorBooks.Add(authorBook);
            }
            await libraryDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Repopulate ViewBag if ModelState is not valid
        FillAuthorsDropdownValues();
        FillDropdownValues();

        return View(book);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await libraryDbContext.Books
            .Include(b => b.AuthorBooks)
            .ThenInclude(ab => ab.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        // Assuming you have methods to populate ViewBag ViewData, or ViewData for dropdowns
        FillAuthorsDropdownValues(); // Method to populate dropdown with authors
        FillDropdownValues(); // Method to populate dropdown with genres

        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book, int[] SelectedAuthors)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                libraryDbContext.Update(book);
                await libraryDbContext.SaveChangesAsync();

                var existingAuthorBooks = libraryDbContext.AuthorBooks.Where(ab => ab.BookId == book.Id).ToList();
                libraryDbContext.AuthorBooks.RemoveRange(existingAuthorBooks);
                await libraryDbContext.SaveChangesAsync();

                if (SelectedAuthors != null)
                {
                    foreach (var authorId in SelectedAuthors)
                    {
                        var authorBook = new AuthorBook
                        {
                            AuthorId = authorId,
                            BookId = book.Id
                        };
                        libraryDbContext.AuthorBooks.Add(authorBook);
                    }
                }

                await libraryDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
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

        // Repopulate ViewBag if ModelState is not valid
        FillAuthorsDropdownValues();
        FillDropdownValues();

        return View(book);
    }

    private bool BookExists(int id)
    {
        return libraryDbContext.Books.Any(e => e.Id == id);
    }


    [HttpGet("Pages/Details")]
    public async Task<IActionResult> Details(int id)
    {
        var book = await libraryDbContext.Books
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
        var book = await libraryDbContext.Books.FindAsync(id);
        if (book != null)
        {
            libraryDbContext.Books.Remove(book);
            await libraryDbContext.SaveChangesAsync();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = true });
            else
                return RedirectToAction("Index");
        }
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return Json(new { success = false });
        else
            return RedirectToAction("Index");
    }

    private void FillDropdownValues()
    {
        var selectItems = new List<SelectListItem>();

        var listItem = new SelectListItem();
        listItem.Text = "- choose -";
        listItem.Value = "";
        selectItems.Add(listItem);

        foreach (var genre in libraryDbContext.Genres)
        {
            listItem = new SelectListItem(genre.Name, genre.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.PossibleGenres = selectItems;
    }

    private void FillAuthorsDropdownValues()
    {
        var selectItems = new List<SelectListItem>();

        foreach (var category in libraryDbContext.Authors)
        {
            var listItem = new SelectListItem(category.FullName, category.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.AllAuthors = selectItems;
    }

}
