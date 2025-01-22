using System.Text;
using MiniBank.AppSettings.Abstractions;
using MiniBank.Communication.Abstractions;

namespace MiniBank.Communication.Services;

public class SmsService(IAppSettings appSettings) : ISmsService
{
    public void Send(string message, string accountNumber, string phoneNumber)
    {
        File.AppendAllText(appSettings.SmsServiceFilePath, $"{DateTime.Now:g} - phone number:{phoneNumber}, account number:{accountNumber}, message:{message}\n");
    }
}
