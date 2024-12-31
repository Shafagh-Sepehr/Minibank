namespace MiniBank.Entities;

public class Withdrawal : ComparableDataBaseEntity
{
    private readonly decimal _amount;
    public required  string  AccountRef { get; init; }
    
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
