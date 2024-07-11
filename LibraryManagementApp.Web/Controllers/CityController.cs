using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers;

public class CityController : Controller
{
	private readonly LibraryDbContext _libraryDbContext;

	public CityController( LibraryDbContext libraryDbContext)
	{
		_libraryDbContext = libraryDbContext;
	}

	[HttpGet]
	public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(CityFilterModel filter = null)
    {
        filter ??= new CityFilterModel();

        var cityQuery = _libraryDbContext.Cities.AsQueryable();

        // Add filtering conditions
        if (!string.IsNullOrWhiteSpace(filter.Name))
            cityQuery = cityQuery.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.Country))
            cityQuery = cityQuery.Where(c => c.Country.ToLower().Contains(filter.Country.ToLower()));

        var model = await cityQuery.ToListAsync();
        return PartialView("_IndexTable", model);
    }

    [HttpGet]
	public IActionResult Create() => View();

	[HttpPost]
	public async Task<IActionResult> Create(City model)
	{
		if (ModelState.IsValid)
		{
			var newCity = new City
			{
				Name = model.Name,
				Country = model.Country
			};

			_libraryDbContext.Cities.Add(newCity);
			await _libraryDbContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var city = await _libraryDbContext
			.Cities
			.FirstOrDefaultAsync(c => c.Id == id);

		if (city == null)
		{
			return NotFound();
		}

		return View(city);
	}

	//[HttpPost, ActionName("Edit")]
	//public async Task<IActionResult> EditCity(City model)
	[HttpPost]
	public async Task<IActionResult> Edit(City model)
	{
		if (ModelState.IsValid)
		{
			var existingCity = await _libraryDbContext.Cities.FirstOrDefaultAsync(c => c.Id == model.Id);

			if (existingCity != null)
			{
				existingCity.Name = model.Name;
				existingCity.Country = model.Country;
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
        var city = await _libraryDbContext.Cities.FindAsync(id);
        if (city != null)
        {
            _libraryDbContext.Cities.Remove(city);
            await _libraryDbContext.SaveChangesAsync();
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

    [HttpGet]
	public async Task<IActionResult> Details(int? id = null)
	{
		City? model = null;

		if (id != null)
		{
			model = await _libraryDbContext
				.Cities
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync();
		}

		return View(model);
	}
}
