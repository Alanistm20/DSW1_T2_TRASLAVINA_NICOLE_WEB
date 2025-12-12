using Library.WebApp.Models;
using Library.WebApp.Services.ApiClients;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApp.Controllers;

public class BooksController : Controller
{
    private readonly BookApiClient _api;
    public BooksController(BookApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var books = await _api.GetAllAsync();
        ViewBag.Error = TempData["Error"];
        ViewBag.Ok = TempData["Ok"];
        return View(books);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        try
        {
            await _api.CreateAsync(dto);
            TempData["Ok"] = "Libro registrado.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _api.DeleteAsync(id);
            TempData["Ok"] = "Libro eliminado.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }

    // vista
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _api.GetByIdAsync(id);
        if (book is null) return NotFound();

        ViewBag.BookId = id;
        return View(new CreateBookDto
        {
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Stock = book.Stock
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CreateBookDto dto)
    {
        try
        {
            await _api.UpdateAsync(id, dto);
            TempData["Ok"] = "Libro actualizado.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
}
