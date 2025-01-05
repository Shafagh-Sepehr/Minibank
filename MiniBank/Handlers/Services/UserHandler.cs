using DB.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class UserHandler(IDataBase dataBase) : IUserHandler
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
    
    public User? Login(string username, string password)
    {
        var users = dataBase.FetchAll<User>();
        return users.FirstOrDefault(x=> x.Username == username && x.PasswordHash == Helper.ComputeSha256Hash(password));
    }
}
