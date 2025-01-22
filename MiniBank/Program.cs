using Microsoft.Extensions.DependencyInjection;
using MiniBank.Handlers.Abstractions;

namespace MiniBank;

internal class Program
{
    private static void Main(string[] args)
    {
        var mainHandler = ServiceCollection.ServiceProvider.GetRequiredService<IMainHandler>();
        mainHandler.Run();
    }
}
