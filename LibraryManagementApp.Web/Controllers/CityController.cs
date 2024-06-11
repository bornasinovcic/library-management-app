using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
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
	public async Task<IActionResult> Index()
	{
		var cities = await _libraryDbContext
			.Cities
			.ToListAsync();
		return View(cities);
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

	[HttpPost, ActionName("Edit")]
	public async Task<IActionResult> EditCity(City model)
	{
		if (ModelState.IsValid)
		{
			var existingCity = await _libraryDbContext.Cities.FirstOrDefaultAsync(c => c.Id == model.Id);

			if (existingCity != null)
			{
				existingCity.Name = model.Name;
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
		var cityToDelete = await _libraryDbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);

		if (cityToDelete == null)
		{
			return NotFound();
		}

		_libraryDbContext.Cities.Remove(cityToDelete);
		await _libraryDbContext.SaveChangesAsync();

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
