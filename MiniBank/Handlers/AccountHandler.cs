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
            UserRef = userRef,
            Balance = 0,
            Status = AccountStatus.Active,
        };
        
        dataBase.Save(newAccount);
    }
    
    
    
}
