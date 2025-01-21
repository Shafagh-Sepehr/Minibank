using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Services;

internal class MainHandler(UserHandler userHandler)
{
    private User? user;

    public void Run()
    {
        while (true)
        {
            try
            {
                user ??= LoginOrSingup();

            }
            catch (Exception e)
            {
                Console.WriteLine("-Error-");
                Console.WriteLine(e.GetType().Name + " : " + e.Message);
                Console.WriteLine("-ErrorEnd-");
                Thread.Sleep(1000);
            }
        }
    }

    private User LoginOrSingup()
    {
        Console.WriteLine("1-Login");
        Console.WriteLine("2-SignUp");
        Console.WriteLine("3-Exit");
        var input = ReadLine();
        Console.Clear();

        switch (input)
        {
            case "1":
                return Login();

            case "2":
                return SignUp();

            case "3":
                Environment.Exit(0);
                return null;

            default:
                throw new Exception("Invalid input");
        }

    }

    private User SignUp()
    {
        Console.Write("UserName: ");
        var newUsername = ReadLine();
        Console.Write("Password: ");
        var newPassword = ReadLine();
        Console.Write("FirstName: ");
        var firstName = ReadLine();
        Console.WriteLine("LastName: ");
        var lastName = ReadLine();
        Console.WriteLine("PhoneNumber: ");
        var phoneNumber = ReadLine();
        Console.WriteLine("NationalId: ");
        var nationalId = ReadLine();

        userHandler.CreateUser(newUsername, newPassword, firstName, lastName, phoneNumber, nationalId);
        var user = userHandler.Login(newUsername, newPassword);

        return user ?? throw new Exception("Invalid username or password");
    }

    private User Login()
    {
        Console.Write("UserName: ");
        var username = ReadLine();
        Console.Write("Password: ");
        var password = ReadLine();

        var user = userHandler.Login(username, password);
        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }
        return user;
    }

    static string ReadLine()
    {
        var input = ReadLine();
        if (input == null || string.IsNullOrWhiteSpace(input))
        {
            throw new Exception($"invalid input");
        }
        return input;
    }
}