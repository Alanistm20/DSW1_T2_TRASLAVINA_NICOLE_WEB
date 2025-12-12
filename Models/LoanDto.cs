namespace Library.WebApp.Models;

public class LoanDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = "";   // si tu API no lo manda, lo dejaremos vacío
    public string StudentName { get; set; } = "";
    public DateTime LoanDate { get; set; }
}
