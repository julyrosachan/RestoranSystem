namespace restoranApp.Models;

public class Employee
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;

    public Employee(string name, string position)
    {
        Name = name;
        Position = position;
    }
}