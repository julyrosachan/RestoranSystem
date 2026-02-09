namespace restoranApp.Models;

public class RegularUser : User
{
    public RegularUser(string username, string password)
        : base(username, password, "User")
    {
    }
}