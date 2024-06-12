using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                _libraryDbContext.Books.Update(model);
                await _libraryDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _libraryDbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _libraryDbContext.Books.Remove(book);
            await _libraryDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
    }
}
