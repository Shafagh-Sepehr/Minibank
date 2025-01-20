namespace MiniBank.AppSettings.Abstractions;

public interface IAppSettings
{
    decimal MaximumStaticPasswordPurchaseLimit { get; init; }
    string  SmsServiceFilePath                 { get; init; }
}
