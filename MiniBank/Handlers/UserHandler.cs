using MiniBank.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Handlers;

public class UserHandler(IDataBase dataBase)
{
    public void CreateUser(string username, string password, string firstName, string lastName, string phoneNumber, string nationalId)
    {
        var newUser = new User
        {
            Username = username,
            PasswordHash = Helper.ComputeSha256Hash(password),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            NationalId = nationalId,
        };
        
        dataBase.Save(newUser);
    }
}
