﻿using LibraryManagementApp.DAL;
using LibraryManagementApp.Model;
using LibraryManagementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers;

public class AuthorController : Controller
{
    private readonly LibraryDbContext _libraryDbContext;

    public AuthorController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> IndexAjax(AuthorFilterModel filter = null)
    {
        filter ??= new AuthorFilterModel();

        var authorQuery = _libraryDbContext.Authors.Include(p => p.PlaceOfBirth).AsQueryable();

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
    public async Task<IActionResult> Create(Author author)
    {
        if (ModelState.IsValid)
        {
            _libraryDbContext.Authors.Add(author);
            await _libraryDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        FillBooksDropdownValues();
        FillDropdownValues();
        return View(author);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var author = await _libraryDbContext.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        FillBooksDropdownValues();
        FillDropdownValues();
        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Author author)
    {
        if (ModelState.IsValid)
        {
            _libraryDbContext.Update(author);
            await _libraryDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        FillBooksDropdownValues();
        FillDropdownValues();
        return View(author);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var author = await _libraryDbContext.Authors
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
        var author = await _libraryDbContext.Authors.FindAsync(id);
        if (author != null)
        {
            _libraryDbContext.Authors.Remove(author);
            await _libraryDbContext.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void FillDropdownValues()
    {
        var selectItems = new List<SelectListItem>();

        var listItem = new SelectListItem();
        listItem.Text = "- choose -";
        listItem.Value = "";
        selectItems.Add(listItem);

        foreach (var category in _libraryDbContext.Cities)
        {
            listItem = new SelectListItem(category.Name, category.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.PossibleCities = selectItems;
    }

    private void FillBooksDropdownValues()
    {
        var selectItems = new List<SelectListItem>();


        foreach (var category in _libraryDbContext.Books)
        {
            var listItem = new SelectListItem(category.Title, category.Id.ToString());
            selectItems.Add(listItem);
        }

        ViewBag.AllBooks = selectItems;
    }
}
