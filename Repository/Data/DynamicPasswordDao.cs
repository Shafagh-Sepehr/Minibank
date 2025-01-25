using Abstractions.Repository;

namespace Repository.Data;

public class DynamicPasswordDao : DataBaseEntity
{
    public required decimal Amount { get; init; }

    public required string OriginCardNumber { get; init; }

    public required string DestinationCardNumber { get; init; }

    public required string DynamicPasswordHash { get; init; }

    public required DateTime ExpiryDate { get; init; }
}
