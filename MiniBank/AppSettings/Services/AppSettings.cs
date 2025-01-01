using MiniBank.AppSettings.Abstractions;

namespace MiniBank.AppSettings.Services;

internal class AppSettings : IAppSettings
{
    public required decimal MaximumStaticPasswordPurchaseLimit { get; init; }
}
