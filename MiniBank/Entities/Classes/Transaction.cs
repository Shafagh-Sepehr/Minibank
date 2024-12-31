using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class Transaction : DataBaseEntity
{
    private readonly decimal _amount;
    
    public required TransactionStatus Status                { get; init; } = TransactionStatus.Success;
    public required long              OriginAccountRef      { get; init; }
    public required long              DestinationAccountRef { get; init; }
    
    public required decimal Amount
    {
        get => _amount;
        init
        {
            if (value < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }
            
            _amount = value;
        }
    }
    
    public DateTime Date { get; init; } = DateTime.Now;
}
