using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniBank.Entities.Classes;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

internal class MainHandler(IUserHandler userHandler, IAccountHandler accountHandler, ICardHandler cardHandler)
{
    private User? user;

    public void Run()
    {
        while (true)
        {
            try
            {
                user ??= LoginOrSingup();

                Console.WriteLine("1-See All of My Accounts");
                Console.WriteLine("2-Create A new Bank Account");
                Console.WriteLine("3-Select A Bank Account");
                Console.WriteLine("4-Log out");
                var input = ReadLine();

                switch (input)
                {
                    case "1":
                        PrintAllAccounts();
                        break;

                    case "2":
                        CreateAccount();
                        break;

                    case "3":
                        break;

                    case "4":
                        user = null;
                        continue;

                    default:
                        throw new Exception("Invalid input");

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

    private void CreateAccount()
    {
        Console.WriteLine("card's first password: ");
        var firstPassword = ReadLine();
        Console.WriteLine("card's second(static) password: ");
        var secondPassword = ReadLine();

        var account = accountHandler.CreateAccount(user.Id);
        Console.WriteLine($"your new account's number: {account.AccountNumber}");

        var card = cardHandler.CreateCard(account.Id, firstPassword, secondPassword);
        Console.WriteLine($"your card number: {card.CardNumber}");
        Console.WriteLine($"your card Cvv2: {card.Cvv2}");
        Console.WriteLine($"your card ExpiryDate: {card.ExpiryDate}");
    }

    private void PrintAllAccounts()
    {
        var accounts = accountHandler.GetAllUserAccounts(user).ToList();
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"  {i}- {accounts[i].AccountNumber}");
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