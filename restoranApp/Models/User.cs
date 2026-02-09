namespace restoranApp.Models;

public abstract class User
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; protected set; } = string.Empty;

    protected User(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }
}