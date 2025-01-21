using System.Security.Cryptography;
using System.Text;

namespace MiniBank;

public static class Helper
{
    public static string GenerateRandomNumberAsString(int length)
    {
        var random = new Random();
        return string.Concat(Enumerable.Range(0, length).Select(_ => random.Next(0, 10).ToString()));
    }
    
    public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256
        // ComputeHash - returns byte array
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

        // Convert byte array to a string
        return Convert.ToHexString(bytes).ToLower();
    }
}
