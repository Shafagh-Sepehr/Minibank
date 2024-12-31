namespace MiniBank.Entities.Classes;

public class DynamicPassword : DataBaseEntity
{
    public required decimal  Amount                   { get; init; }
    public required string   OriginCardNumber         { get; init; }
    public          string  DestinationCardNumber    { get; init; }
    public required string   DynamicPasswordHash      { get; init; }
    public          DateTime ExpiryDate               { get; } = DateTime.Now.AddMinutes(2);
}
