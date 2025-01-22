using MiniBank.Entities.Classes;
using MiniBank.Exceptions;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

internal class MainHandler(IUserHandler userHandler, IAccountHandler accountHandler, ICardHandler cardHandler,
    IDepositHandler depositHandler, IWithdrawalHandler withdrawalHandler, ITransactionHandler transactionHandler)
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
                        Console.WriteLine("enter account number: ");
                        var accountNumber = ReadLine();
                        AccountManager(accountNumber);
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

    private void AccountManager(string accountNumber)
    {
        ArgumentNullException.ThrowIfNull(user);
        var correctAccount = accountHandler.AccountExistsAndBelongsToUser(accountNumber, user.Id);

        if (correctAccount == false)
        {
            throw new OperationFailedException("entered account number is wrong");
        }

        try
        {
            Console.WriteLine("1-Show Balance");
            Console.WriteLine("2-Deposit Money");
            Console.WriteLine("3-Withdraw Money");
            Console.WriteLine("4-Create Account To Account Transaction");
            Console.WriteLine("5-Create Card To Card Transaction");
            Console.WriteLine("6-See All Transactions");
            Console.WriteLine("7-See All Deposits");
            Console.WriteLine("8-See All Withdrawals");
            Console.WriteLine("9-Go Back");

            var input = ReadLine();
            decimal amount;
            switch (input)
            {
                case "1":
                    var balance = accountHandler.GetAccountBalance(accountNumber);
                    Console.WriteLine($"your balance: {balance}");
                    break;

                case "2":
                    Console.Write("How much do you want to deposit? =: ");
                    amount = decimal.Parse(ReadLine());
                    depositHandler.Deposit(accountNumber, amount);
                    break;

                case "3":
                    Console.Write("How much do you want to withdraw? =: ");
                    amount = decimal.Parse(ReadLine());
                    withdrawalHandler.Withdraw(accountNumber, amount);
                    break;

                case "4":
                    AccountToAccountTransaction(accountNumber);
                    break;

                case "5":
                    CardToCardTransaction(accountNumber);
                    break;

                case "6":
                    ShowAllTransactions(accountNumber);
                    break;

                case "7":
                    ShowAllDeposits(accountNumber);
                    break;

                case "8":
                    ShowAllWithdrawals(accountNumber);
                    break;

                case "9":
                    return;

                default:
                    throw new Exception("invalid input");
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

    private void CardToCardTransaction(string accountNumber)
    {
        var originCardNumber = cardHandler.GetCard(accountNumber).CardNumber;

        Console.Write("enter the destination card number: ");
        var destinationCardNumber = ReadLine();

        Console.Write("How much do you want to send? =: ");
        var amount = decimal.Parse(ReadLine());

        Console.Write("enter transaction description (or leave empty): ");
        var description = Console.ReadLine();

        Console.WriteLine("enter second password or enter '0' to send dynamic password: ");
        var input = ReadLine();

        Console.WriteLine("enter cvv2: ");
        var cvv2 = ReadLine();

        Console.WriteLine("enter expiry date(e.g. 2027/5): ");
        var expiryDate = ReadLine();
        var expiryDateTime = DateTime.Parse(expiryDate);

        string password;
        if (input == "0")
        {
            cardHandler.RequestDynamicPassword(amount, originCardNumber, destinationCardNumber, cvv2, expiryDateTime);
            Console.WriteLine("enter dynamic password: ");
            password = ReadLine();
        }
        else
        {
            password = input;
        }

        transactionHandler.CreateCardToCardTransaction(originCardNumber, destinationCardNumber, amount, password, description);
    }

    private void AccountToAccountTransaction(string accountNumber)
    {
        Console.Write("enter the destination account number: ");
        var destinationAccountNumber = ReadLine();

        Console.Write("How much do you want to send? =: ");
        var amount = decimal.Parse(ReadLine());

        Console.Write("enter transaction description (or leave empty): ");
        var description = Console.ReadLine();

        transactionHandler.CreateAccountToAccountTransaction(accountNumber, destinationAccountNumber, amount, description);
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
            throw new Exception("invalid input");
        }
        return input;
    }
    private void ShowAllTransactions(string accountNumber)
    {
        var transactions = transactionHandler.GetAllTransactions(accountNumber);

        foreach (var transaction in transactions)
        {
            Console.WriteLine($"origin account number: {transaction.OriginAccountNumber}");
            Console.WriteLine($"destination account number: {transaction.DestinationAccountNumber}");
            Console.WriteLine($"amount: {transaction.Amount}");
            Console.WriteLine($"date: {transaction.Date}");
            Console.WriteLine($"description: {transaction.Description}");
            Console.WriteLine($"type: {transaction.Type}");
            Console.WriteLine($"status: {transaction.Status}");
            Console.WriteLine("-------------------------------");
        }
    }

    private void ShowAllDeposits(string accountNumber)
    {
        var deposits = depositHandler.GetAllDeposits(accountNumber);

        foreach (var deposit in deposits)
        {
            Console.WriteLine($"amount: {deposit.Amount}");
            Console.WriteLine($"date: {deposit.Date}");
            Console.WriteLine($"status: {deposit.Status}");
            Console.WriteLine("-------------------------------");
        }
    }

    private void ShowAllWithdrawals(string accountNumber)
    {
        var withdrawals = withdrawalHandler.GetAllWithdrawals(accountNumber);

        foreach (var withdrawal in withdrawals)
        {
            Console.WriteLine($"amount: {withdrawal.Amount}");
            Console.WriteLine($"date: {withdrawal.Date}");
            Console.WriteLine($"status: {withdrawal.Status}");
            Console.WriteLine("-------------------------------");
        }
    }
}