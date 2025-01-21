namespace MiniBank.Entities.Enums;

public enum TransactionType
{
    Unknown,
    AccountToAccount,
    StaticCardToCard,
    DynamicCardToCard,
    FailedCardToCard,
}
