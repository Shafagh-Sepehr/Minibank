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
                if (user == null)
                {
                    LoginOrSingup();
                }

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

    private void LoginOrSingup()
    {
        Console.WriteLine("1-Login");
        Console.WriteLine("2-SignUp");
        Console.WriteLine("3-Exit");
        var input = ReadLine();
        Console.Clear();

        switch (input)
        {
            case "1":
                Login();
                return;

            case "2":
                SignUp();
                return;

            case "3":
                Environment.Exit(0);
                return;

            default:
                throw new Exception("Invalid input");
        }

    }

    private void SignUp()
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
        user = userHandler.Login(newUsername, newPassword);
    }

    private void Login()
    {
        Console.Write("UserName: ");
        var username = ReadLine();
        Console.Write("Password: ");
        var password = ReadLine();

        user = userHandler.Login(username, password);
        if (user == null)
        {
            Console.WriteLine("Invalid username or password");
            Thread.Sleep(1000);
        }
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