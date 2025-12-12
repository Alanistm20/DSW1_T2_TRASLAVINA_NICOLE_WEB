namespace Library.WebApp.Models;

public class LoanDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = "";   
    public string StudentName { get; set; } = "";
    public DateTime LoanDate { get; set; }
}
