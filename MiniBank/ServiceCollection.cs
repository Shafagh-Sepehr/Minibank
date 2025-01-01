using DB.Data.Abstractions;
using DB.Data.Services;
using DB.Validators.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.AppSettings.Abstractions;
using MiniBank.Communication.Abstractions;
using MiniBank.Communication.Services;
using MiniBank.Entities.Classes;
using MiniBank.Handlers.Abstractions;
using MiniBank.Handlers.Services;
using MiniBank.Validators.Services;

namespace MiniBank;

internal static class ServiceCollection
{
    private static ServiceProvider? _serviceProvider;
    
    public static ServiceProvider ServiceProvider => _serviceProvider ??= ConfigureServices();
    
    private static ServiceProvider ConfigureServices()
    {
        var serviceCollector = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        
        
        serviceCollector.AddSingleton<IDataBase, DataBase>();
        
        serviceCollector.AddTransient<IValidator<User>, UserValidator>();
        serviceCollector.AddTransient<IValidator<Account>, AccountValidator>();
        serviceCollector.AddTransient<IValidator<Card>, CardValidator>();
        
        serviceCollector.AddTransient<IAccountHandler, AccountHandler>();
        serviceCollector.AddTransient<ICardHandler, CardHandler>();
        serviceCollector.AddTransient<IDepositHandler, DepositHandler>();
        serviceCollector.AddTransient<ITransactionHandler, TransactionHandler>();
        serviceCollector.AddTransient<IUserHandler, UserHandler>();
        serviceCollector.AddTransient<IWithdrawalHandler, WithdrawalHandler>();
        
        serviceCollector.AddSingleton<ISmsService, SmsService>();
        
        
        serviceCollector.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build());
        
        serviceCollector.AddSingleton<IAppSettings>(sp =>
        {
            var appSettings = sp.GetRequiredService<IConfiguration>().GetSection("AppSettings").Get<AppSettings.Services.AppSettings>();
            ArgumentNullException.ThrowIfNull(appSettings);
            return appSettings;
        });
        
        
        return serviceCollector.BuildServiceProvider();
    }
}
