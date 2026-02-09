

using restoranApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace restoranApp.Core;

public class RestaurantSystem
{
    private readonly List<User> _users = new();
    private readonly List<Employee> _employees = new();
    private readonly List<Table> _tables = new();
    private decimal _adminBalance;

    public RestaurantSystem()
    {
      
        _users.Add(new Admin("admin", "admin123"));
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Restoran Sistemi ===\n");
            Console.WriteLine("Rol seçin:");
            Console.WriteLine("  1 → Admin");
            Console.WriteLine("  2 → Manager");
            Console.WriteLine("  3 → User");
            Console.WriteLine("  0 → Çıxış");
            Console.Write("\nSeçim: ");

            string? choice = Console.ReadLine();

            if (choice == "0") break;

            string? role = choice switch
            {
                "1" => "Admin",
                "2" => "Manager",
                "3" => "User",
                _ => null
            };

            if (role is null)
            {
                Console.WriteLine("Yanlış seçim.");
                Console.ReadKey();
                continue;
            }

            Console.Write("İstifadəçi adı: ");
            string? username = Console.ReadLine();

            Console.Write("Şifrə: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Məlumatlar boş ola bilməz.");
                Console.ReadKey();
                continue;
            }

            User? currentUser = Login(username, password, role);

            if (currentUser is null)
            {
                Console.WriteLine("Yanlış istifadəçi adı, şifrə və ya rol.");
                Console.ReadKey();
                continue;
            }

            switch (role)
            {
                case "Admin":   AdminMenu();   break;
                case "Manager": ManagerMenu(); break;
                case "User":    UserMenu();    break;
            }
        }
    }

    private User? Login(string username, string password, string role)
    {
        return _users.FirstOrDefault(u =>
            u.Username == username &&
            u.Password == password &&
            u.Role == role);
    }

    // ──────────────────────────────────────────────
    //                  ADMIN MENU
    // ──────────────────────────────────────────────

    private void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("ADMIN PANEL\n");
            Console.WriteLine("1. Manager əlavə et");
            Console.WriteLine("2. Manager sil");
            Console.WriteLine("3. Bütün managerlərə bax");
            Console.WriteLine("4. Manager sayı");
            Console.WriteLine("5. İşçi əlavə et");
            Console.WriteLine("6. İşçilərə bax");
            Console.WriteLine("7. İşçi sayı");
            Console.WriteLine("8. Admin balansına bax");
            Console.WriteLine("0. Çıxış");
            Console.Write("\nSeçim: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddManager();          break;
                case "2": RemoveManager();       break;
                case "3": ViewManagers();        break;
                case "4": ShowManagerCount();    break;
                case "5": AddEmployee();         break;
                case "6": ViewEmployees();       break;
                case "7": ShowEmployeeCount();   break;
                case "8": ShowAdminBalance();    break;
                case "0": return;
                default:  Console.WriteLine("Yanlış seçim"); break;
            }

            Console.WriteLine("\nDavam etmək üçün Enter...");
            Console.ReadKey();
        }
    }

    private void AddManager()
    {
        Console.Write("Yeni manager istifadəçi adı: ");
        string? username = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(username) || _users.Any(u => u.Username == username))
        {
            Console.WriteLine("Bu istifadəçi adı artıq mövcuddur və ya boşdur.");
            return;
        }

        Console.Write("Şifrə: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Şifrə boş ola bilməz.");
            return;
        }

        _users.Add(new Manager(username, password));
        Console.WriteLine("Manager əlavə olundu.");
    }

    private void RemoveManager()
    {
        Console.Write("Silinəcək managerin istifadəçi adı: ");
        string? username = Console.ReadLine();

        var manager = _users.FirstOrDefault(u => u.Username == username && u.Role == "Manager");

        if (manager is not null)
        {
            _users.Remove(manager);
            Console.WriteLine("Manager silindi.");
        }
        else
        {
            Console.WriteLine("Belə manager tapılmadı.");
        }
    }

    private void ViewManagers()
    {
        var managers = _users.Where(u => u.Role == "Manager").ToList();
        if (!managers.Any())
        {
            Console.WriteLine("Heç bir manager yoxdur.");
            return;
        }

        foreach (var m in managers)
            Console.WriteLine($" • {m.Username}");
    }

    private void ShowManagerCount() =>
        Console.WriteLine($"Manager sayı: {_users.Count(u => u.Role == "Manager")}");

    private void AddEmployee()
    {
        Console.Write("İşçinin adı: ");
        string? name = Console.ReadLine();

        Console.Write("Vəzifəsi: ");
        string? position = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(position))
        {
            Console.WriteLine("Məlumatlar tam doldurulmayıb.");
            return;
        }

        _employees.Add(new Employee(name, position));
        Console.WriteLine("İşçi əlavə olundu.");
    }

    private void ViewEmployees()
    {
        if (!_employees.Any())
        {
            Console.WriteLine("Heç bir işçi yoxdur.");
            return;
        }

        foreach (var e in _employees)
            Console.WriteLine($" • {e.Name}  ({e.Position})");
    }

    private void ShowEmployeeCount() =>
        Console.WriteLine($"İşçi sayı: {_employees.Count}");

    private void ShowAdminBalance() =>
        Console.WriteLine($"Admin balans: {_adminBalance:C}");

    // ──────────────────────────────────────────────
    //                MANAGER MENU
    // ──────────────────────────────────────────────

    private void ManagerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("MANAGER PANEL\n");
            Console.WriteLine("1. Yeni masa əlavə et");
            Console.WriteLine("2. Bütün masalara bax");
            Console.WriteLine("3. Boş masalara bax");
            Console.WriteLine("0. Çıxış");
            Console.Write("\nSeçim: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddTable();             break;
                case "2": ViewAllTables();        break;
                case "3": ViewAvailableTables();  break;
                case "0": return;
                default:  Console.WriteLine("Yanlış seçim"); break;
            }

            Console.ReadKey();
        }
    }

    private void AddTable()
    {
        Console.Write("Masa nömrəsi (ID): ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Düzgün rəqəm daxil edin.");
            return;
        }

        if (_tables.Any(t => t.Id == id))
        {
            Console.WriteLine("Bu nömrəli masa artıq mövcuddur.");
            return;
        }

        _tables.Add(new Table(id));
        Console.WriteLine($"Masa {id} əlavə olundu.");
    }

    private void ViewAllTables()
    {
        if (!_tables.Any())
        {
            Console.WriteLine("Hələ heç bir masa əlavə olunmayıb.");
            return;
        }

        foreach (var t in _tables)
            Console.WriteLine($"Masa {t.Id,-3} → {(t.IsReserved ? "REZERV OLUNUB" : "BOŞ")}");
    }

    private void ViewAvailableTables()
    {
        var available = _tables.Where(t => !t.IsReserved).ToList();

        if (!available.Any())
        {
            Console.WriteLine("Hazırda boş masa yoxdur.");
            return;
        }

        foreach (var t in available)
            Console.WriteLine($"Masa {t.Id}");
    }

    // ──────────────────────────────────────────────
    //                  USER MENU
    // ──────────────────────────────────────────────

    private void UserMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("MÜŞTƏRİ MENYUSU\n");
            Console.WriteLine("1. Boş masalara bax");
            Console.WriteLine("2. Masa rezerv et");
            Console.WriteLine("0. Çıxış");
            Console.Write("\nSeçim: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ViewAvailableTables();  break;
                case "2": ReserveTable();         break;
                case "0": return;
                default:  Console.WriteLine("Yanlış seçim"); break;
            }

            Console.ReadKey();
        }
    }

    private void ReserveTable()
    {
        ViewAvailableTables();

        Console.Write("\nRezerv etmək istədiyiniz masa nömrəsi: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Düzgün nömrə daxil edin.");
            return;
        }

        var table = _tables.FirstOrDefault(t => t.Id == id && !t.IsReserved);

        if (table is null)
        {
            Console.WriteLine("Belə boş masa tapılmadı.");
            return;
        }

        table.IsReserved = true;
        
        decimal payment = 50m;
        _adminBalance += payment;

        Console.WriteLine($"Masa {id} sizin üçün rezerv olundu.");
        Console.WriteLine($"{payment:C} məbləğ admin balansına əlavə edildi.");
    }
}
