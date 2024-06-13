using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers;
[Route("api/author")]
[ApiController]
public class AuthorApiController : ControllerBase
{
    private readonly LibraryDbContext _libraryDbContext;
    public AuthorApiController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    // GET: api/author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> Get()
    {
        var authors = await _libraryDbContext.Authors
            .Include(a => a.PlaceOfBirth)
            .ToListAsync();

        return Ok(authors);
    }

    // GET: api/author/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await _libraryDbContext.Authors
            .Include(a => a.PlaceOfBirth)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    // POST: api/author
    [HttpPost]
    public async Task<ActionResult<Author>> Post([FromBody] Author model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _libraryDbContext.Authors.Add(model);
        await _libraryDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { id = model.Id }, model);
    }

    // PUT: api/author/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Author model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID in the URL does not match the ID in the request body.");
        }

        var existingAuthor = await _libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

        if (existingAuthor == null)
        {
            return NotFound();
        }

        existingAuthor.FirstName = model.FirstName;
        existingAuthor.LastName = model.LastName;
        existingAuthor.Gender = model.Gender;
        existingAuthor.Email = model.Email;
        existingAuthor.DateOfBirth = model.DateOfBirth;
        existingAuthor.PlaceOfBirthId = model.PlaceOfBirthId;

        await _libraryDbContext.SaveChangesAsync();

        // Check if the author still exists after saving changes
        if (!AuthorExists(id))
        {
            return NotFound();
        }

        return Ok(existingAuthor);
    }

    private bool AuthorExists(int id)
    {
        return _libraryDbContext.Authors.Any(e => e.Id == id);
    }

    // DELETE: api/author/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _libraryDbContext.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _libraryDbContext.Authors.Remove(author);
        await _libraryDbContext.SaveChangesAsync();

        return Ok(new { message = "Author deleted successfully." });
    }

}