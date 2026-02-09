namespace restoranApp.Models;

public class Table
{
    public int Id { get; set; }
    public bool IsReserved { get; set; }

    public Table(int id)
    {
        Id = id;
        IsReserved = false;
    }
}