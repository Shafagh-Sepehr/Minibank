namespace MiniBank.Entities;

public class Transaction : ComparableDataBaseEntity
{
    private readonly decimal _amount;
    
    public required string OriginAccountNumber      { get; init; }
    public required string DestinationAccountNumber { get; init; }
    
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
    
    public required DateTime Date { get; init; } = DateTime.Now;
}
