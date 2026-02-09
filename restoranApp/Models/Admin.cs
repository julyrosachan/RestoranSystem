namespace restoranApp.Models;

public class Admin : User
{
    public Admin(string username, string password)
        : base(username, password, "Admin")
    {
    }
}