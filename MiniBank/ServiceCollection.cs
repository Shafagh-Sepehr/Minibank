using Microsoft.Extensions.DependencyInjection;

namespace MiniBank;

public static class ServiceCollection
{
    private static ServiceProvider? _serviceProvider;
    public static ServiceProvider ServiceProvider
    {
        get => _serviceProvider ?? throw new ArgumentNullException(nameof(ServiceProvider));
        set => _serviceProvider ??= value;
    }
}