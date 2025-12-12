using System.Net.Http.Json;
using Library.WebApp.Models;

namespace Library.WebApp.Services.ApiClients;

public class LoanApiClient
{
    private readonly HttpClient _http;
    public LoanApiClient(HttpClient http) => _http = http;

    public async Task<List<LoanDto>> GetActiveAsync()
        => await _http.GetFromJsonAsync<List<LoanDto>>("/api/loans/active") ?? [];

    public async Task CreateAsync(CreateLoanDto dto)
    {
        var res = await _http.PostAsJsonAsync("/api/loans", dto);
        await ThrowIfNotOk(res);
    }

    public async Task ReturnAsync(int loanId)
    {
        var res = await _http.PutAsync($"/api/loans/{loanId}/return", null);
        await ThrowIfNotOk(res);
    }

    private static async Task ThrowIfNotOk(HttpResponseMessage res)
    {
        if (res.IsSuccessStatusCode) return;

        var msg = await res.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(msg) ? "Error al consumir la API" : msg);
    }
}
