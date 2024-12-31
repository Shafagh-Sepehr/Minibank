namespace MiniBank.Entities;

public class Deposit : ComparableDataBaseEntity
{
    private readonly decimal _amount;
    
    public required TransactionStatus Status     { get; init; } = TransactionStatus.Success;
    public required long              AccountRef { get; init; }
    
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
