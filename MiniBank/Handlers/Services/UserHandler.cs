using Abstractions.Repository;
using MiniBank.Entities.Classes;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class UserHandler(IRepository repository) : IUserHandler
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

        repository.Insert(newUser);
    }

    public User? Login(string username, string password)
    {
        var users = repository.FetchAll<User>();
        return users.FirstOrDefault(x => x.Username == username && x.PasswordHash == Helper.ComputeSha256Hash(password));
    }
}
