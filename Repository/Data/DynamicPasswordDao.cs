using Abstractions.Repository;
using InMemoryDataBase.Attributes;

namespace Repository.Data;

public class DynamicPasswordDao : DataBaseEntity
{
    [PrimaryKey]
    public new string Id { get; set; } = string.Empty;

    public required decimal Amount { get; init; }

    public required string OriginCardNumber { get; init; }

    public required string DestinationCardNumber { get; init; }

    public required string DynamicPasswordHash { get; init; }

    public required DateTime ExpiryDate { get; init; }
}
