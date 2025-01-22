using System.Security.Cryptography;
using System.Text;
using MiniBank.Entities.Enums;
using MiniBank.Exceptions;

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

    public static void ThrowExceptionIfActionFailed(ActionResult actionResult)
    {
        switch (actionResult)
        {
            case ActionResult.Unknown:
                throw new OperationFailedException("Unknown error occurred with action result");

            case ActionResult.Success:
                break;

            case ActionResult.AccountNotFound:
                throw new OperationFailedException("Destination account doesn't exist");

            case ActionResult.InsufficientBalance:
                throw new OperationFailedException("You don't have the sufficient balance");

            case ActionResult.IncorrectPassword:
                throw new OperationFailedException("The Provided Credentials are incorrect");

            case ActionResult.MaximumStaticPasswordPurchaseLimitExceeded:
                throw new OperationFailedException("Maximum static password purchase limit exceeded");

            default:
                break;
        }
    }
}
