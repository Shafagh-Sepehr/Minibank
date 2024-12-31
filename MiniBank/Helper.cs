using System.Security.Cryptography;
using System.Text;

namespace MiniBank;

public static class Helper
{
    public static string GenerateRandomNumberAsString(int length)
    {
        var random = new Random();
        var stringBuilder = new StringBuilder(length);
        
        for (var i = 0; i < length; i++)
        {
            var digit = random.Next(0, 10);
            stringBuilder.Append(digit);
        }
        
        return stringBuilder.ToString();
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
