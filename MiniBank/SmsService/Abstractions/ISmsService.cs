namespace MiniBank.SmsService.Abstractions;

public interface ISmsService
{
    void Send(string message, string phoneNumber);
    void Dispose();
}
