using Library.WebApp.Models;
using Library.WebApp.Services.ApiClients;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApp.Controllers;

public class LoansController : Controller
{
    private readonly LoanApiClient _loans;
    private readonly BookApiClient _books;

    public LoansController(LoanApiClient loans, BookApiClient books)
    {
        _loans = loans;
        _books = books;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Error = TempData["Error"];
        ViewBag.Ok = TempData["Ok"];

        var active = await _loans.GetActiveAsync();
        var books = await _books.GetAllAsync();

        ViewBag.Books = books; // para dropdown
        return View(active);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanDto dto)
    {
        try
        {
            await _loans.CreateAsync(dto);
            TempData["Ok"] = "Préstamo registrado.";
        }
        catch (Exception ex)
        {
            // sin stck
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Return(int id)
    {
        try
        {
            await _loans.ReturnAsync(id);
            TempData["Ok"] = "Préstamo devuelto.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
}
