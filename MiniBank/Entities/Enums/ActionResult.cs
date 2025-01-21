namespace MiniBank.Entities.Enums;

public enum ActionResult
{
    Unknown,
    Success,
    AccountNotFound,
    InsufficientBalance,
    IncorrectPassword,
    MaximumStaticPasswordPurchaseLimitExceeded,
}
