using System.Net.Http.Json;
using Library.WebApp.Models;

namespace Library.WebApp.Services.ApiClients;

public class BookApiClient
{
    private readonly HttpClient _http;
    public BookApiClient(HttpClient http) => _http = http;

    public async Task<List<BookDto>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<BookDto>>("/api/books") ?? [];

    public async Task<BookDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<BookDto>($"/api/books/{id}");

    public async Task CreateAsync(CreateBookDto dto)
    {
        var res = await _http.PostAsJsonAsync("/api/books", dto);
        await ThrowIfNotOk(res);
    }

    public async Task UpdateAsync(int id, CreateBookDto dto)
    {
        var res = await _http.PutAsJsonAsync($"/api/books/{id}", dto);
        await ThrowIfNotOk(res);
    }

    public async Task DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"/api/books/{id}");
        await ThrowIfNotOk(res);
    }

    private static async Task ThrowIfNotOk(HttpResponseMessage res)
    {
        if (res.IsSuccessStatusCode) return;

        var msg = await res.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(msg) ? "Error al consumir la API" : msg);
    }
}
