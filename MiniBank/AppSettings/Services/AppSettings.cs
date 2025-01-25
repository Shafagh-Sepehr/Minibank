using MiniBank.AppSettings.Abstractions;

namespace MiniBank.AppSettings.Services;

public class AppSettings : IAppSettings
{
    public required decimal MaximumStaticPasswordPurchaseLimit { get; init; }
    public required string SmsServiceFilePath { get; init; }
}
