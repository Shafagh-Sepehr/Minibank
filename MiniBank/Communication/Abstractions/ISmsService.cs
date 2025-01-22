namespace MiniBank.Communication.Abstractions;

public interface ISmsService
{
    void Send(string message, string accountNumber, string phoneNumber);
}
