namespace restoranApp.Models;

public class Manager : User
{
    public Manager(string username, string password)
        : base(username, password, "Manager")
    {
    }
}