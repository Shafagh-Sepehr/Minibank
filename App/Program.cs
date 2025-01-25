using Abstractions.MiniBank;
using Microsoft.Extensions.DependencyInjection;

namespace App;

internal class Program
{
    static void Main()
    {
        var sp = ServiceCollection.ServiceCollection.ServiceProvider;
        MiniBank.ServiceCollection.ServiceProvider = sp;
        var mainHandler = sp.GetRequiredService<IMainHandler>();
        mainHandler.Run();
    }
}
