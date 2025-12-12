using Library.WebApp.Services.ApiClients;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// HttpClients para consumir la API
builder.Services.AddHttpClient<BookApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!);
});

builder.Services.AddHttpClient<LoanApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!);
});

var app = builder.Build();

// Pipeline (template MVC .NET 9)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();      // para css/js/img en wwwroot
app.MapStaticAssets();     // feature nueva del template .NET 9 (déjalo)

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto: Libros
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
