namespace Library.WebApp.Models;

public class CreateBookDto
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string Isbn { get; set; } = "";
    public int Stock { get; set; }
}
