using System.Security.Cryptography;
using System.Text;
using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class UserHandler(IDataBase dataBase)
{
    public void CreateUser(string username, string password, string firstName, string lastName, string phoneNumber, string nationalId)
    {
        var newUser = new User
        {
            Username = username,
            PasswordHash = ComputeSha256Hash(password),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            NationalId = nationalId,
        };
        
        dataBase.Save(newUser);
    }
    
    public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256
        // ComputeHash - returns byte array
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

        // Convert byte array to a string
        var builder = new StringBuilder();
        
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        
        return builder.ToString();
    }
}
