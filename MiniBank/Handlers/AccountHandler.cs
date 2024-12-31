using System.Text;
using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class AccountHandler(IDataBase dataBase)
{
    public void CreateAccount(long userRef, string password, string secondPassword)
    {
        var newAccount = new Account
        {
            AccountNumber = Helper.GenerateRandomNumberAsString(20),
            Card = new()
            {
                CardNumber = Helper.GenerateRandomNumberAsString(16),
                Cvv2 = Helper.GenerateRandomNumberAsString(3),
                ExpiryDate = DateTime.Now.AddYears(5),
                PasswordHash = Helper.ComputeSha256Hash(password),
                SecondPasswordHash = Helper.ComputeSha256Hash(secondPassword),
            },
            UserRef = userRef,
            Balance = 0,
            Status = AccountStatus.Active,
        };
        
        dataBase.Save(newAccount);
    }
    
    
    
}
