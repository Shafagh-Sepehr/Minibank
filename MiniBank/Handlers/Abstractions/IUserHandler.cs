namespace MiniBank.Handlers.Abstractions;

public interface IUserHandler
{
    void CreateUser(string username, string password, string firstName, string lastName, string phoneNumber, string nationalId);
}
