using restoranApp.Core;

namespace restoranApp;

class Program
{
    static void Main(string[] args)
    {
        var system = new RestaurantSystem();
        system.Run();
    }
}