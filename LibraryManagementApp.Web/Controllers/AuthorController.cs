using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers;

public class AuthorController(LibraryDbContext libraryDbContext) : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(AuthorFilterModel filter = null)
    {
        filter ??= new AuthorFilterModel();

        var authorQuery = libraryDbContext.Authors.Include(p => p.PlaceOfBirth).AsQueryable();

        // Add filtering conditions
        if (!string.IsNullOrWhiteSpace(filter.FullName))
            authorQuery = authorQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            authorQuery = authorQuery.Where(a => a.Email.ToLower().Contains(filter.Email.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.City))
            authorQuery = authorQuery.Where(p => p.PlaceOfBirthId != null && p.PlaceOfBirth.Name.ToLower().Contains(filter.City.ToLower()));

        var model = await authorQuery.ToListAsync();
        return PartialView("_IndexTable", model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        FillBooksDropdownValues();
        FillDropdownValues();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Author author, int[] AuthorBooks)
    {
        if (ModelState.IsValid)
        {
            // Create new author and save to the database
            libraryDbContext.Authors.Add(author);
            await libraryDbContext.SaveChangesAsync();

            // Add selected books to the author
            foreach (var bookId in AuthorBooks)
            {
                var authorBook = new AuthorBook
                {
                    AuthorId = author.Id,
                    BookId = bookId
                };
                libraryDbContext.AuthorBooks.Add(authorBook);
            }
            await libraryDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Repopulate ViewBag if ModelState is not valid
        FillBooksDropdownValues();
        FillDropdownValues();

        return View(author);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var author = await libraryDbContext.Authors
            .Include(a => a.AuthorBooks)
            .ThenInclude(ab => ab.Book)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound();
        }
        FillBooksDropdownValues();
        FillDropdownValues();
        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Author author, int[] SelectedBooks)
    {
        if (id != author.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                libraryDbContext.Update(author);
                await libraryDbContext.SaveChangesAsync();

                var existingAuthorBooks = libraryDbContext.AuthorBooks.Where(ab => ab.AuthorId == author.Id).ToList();
                libraryDbContext.AuthorBooks.RemoveRange(existingAuthorBooks);
                await libraryDbContext.SaveChangesAsync();

                foreach (var bookId in SelectedBooks)
                {
                    var authorBook = new AuthorBook
                    {
                        AuthorId = author.Id,
                        BookId = bookId
                    };
                    libraryDbContext.AuthorBooks.Add(authorBook);
                }
                await libraryDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(author.Id))
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
        FillBooksDropdownValues();
        FillDropdownValues();


        return View(author);
    }

    private bool AuthorExists(int id)
    {
        return libraryDbContext.Authors.Any(e => e.Id == id);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var author = await libraryDbContext.Authors
            .Include(a => a.PlaceOfBirth)
            .Include(a => a.AuthorBooks)
            .ThenInclude(ab => ab.Book)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound();
        }
        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await libraryDbContext.Authors.FindAsync(id);
        if (author != null)
        {
            libraryDbContext.Authors.Remove(author);
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

        foreach (var category in libraryDbContext.Cities)
        {
            listItem = new SelectListItem(category.Name, category.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.PossibleCities = selectItems;
    }

    private void FillBooksDropdownValues()
    {
        var selectItems = new List<SelectListItem>();


        foreach (var category in libraryDbContext.Books)
        {
            var listItem = new SelectListItem(category.Title, category.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.AllBooks = selectItems;
    }
}
