using Abstractions.MiniBank;
using Abstractions.Repository;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.Core.Services;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Services;
using InMemoryDataBase.DataSanitizers.Services;
using InMemoryDataBase.Validators.Abstractions;
using InMemoryDataBase.Validators.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.AppSettings.Abstractions;
using MiniBank.AppSettings.Services;
using MiniBank.Communication.Abstractions;
using MiniBank.Communication.Services;
using MiniBank.Entities.Classes;
using MiniBank.Handlers.Abstractions;
using MiniBank.Handlers.Services;
using MiniBank.Validators.Abstractions;
using MiniBank.Validators.Services;
using Repository.Abstractions;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.DaoToEntity.Services;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.EntityToDao.Services;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Services;
using Repository.Data;
using Repository.InMemoryRepository;
using Repository.InMemoryRepository.Services;

namespace ServiceCollection;

public static class ServiceCollection
{
    private static ServiceProvider? _serviceProvider;

    public static ServiceProvider ServiceProvider => _serviceProvider ??= ConfigureServices();

    private static ServiceProvider ConfigureServices()
    {
        var serviceCollector = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

        // InMemoryDatabase project
        serviceCollector.AddSingleton<IShafaghDB, ShafaghDB>();

        serviceCollector.AddSingleton<IReferenceHandler, ReferenceHandler>();
        serviceCollector.AddSingleton<IReferenceInsertHandler, ReferenceInsertHandler>();
        serviceCollector.AddSingleton<IReferenceUpdateHandler, ReferenceUpdateHandler>();
        serviceCollector.AddSingleton<IReferenceDeleteHandler, ReferenceDeleteHandler>();

        serviceCollector.AddSingleton<IDefaultValueSetter, DefaultValueSetter>();

        serviceCollector.AddSingleton<IPrimaryKeyValidator, PrimaryKeyValidator>();
        serviceCollector.AddSingleton<IForeignKeyValidator, ForeignKeyValidator>();
        serviceCollector.AddSingleton<IDeletionIntegrityValidator, DeletionIntegrityValidator>();
        serviceCollector.AddSingleton<INullablePropertyValidator, NullablePropertyValidator>();
        serviceCollector.AddSingleton<IAttributeValidator, AttributeValidator>();
        serviceCollector.AddSingleton<IValidator, Validator>();

        // MiniBank project
        serviceCollector.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build());

        serviceCollector.AddSingleton<IAppSettings>(sp =>
        {
            var appSettings = sp.GetRequiredService<IConfiguration>().GetSection("AppSettings").Get<AppSettings>();
            ArgumentNullException.ThrowIfNull(appSettings);
            return appSettings;
        });

        serviceCollector.AddTransient<IValidator<User>, UserValidator>();
        serviceCollector.AddTransient<IValidator<Account>, AccountValidator>();
        serviceCollector.AddTransient<IValidator<Card>, CardValidator>();
        serviceCollector.AddTransient<IValidator<Deposit>, DepositValidator>();
        serviceCollector.AddTransient<IValidator<Withdrawal>, WithdrawalValidator>();
        serviceCollector.AddTransient<IValidator<DynamicPassword>, DynamicPasswordValidator>();
        serviceCollector.AddTransient<IValidator<Transaction>, TransactionValidator>();

        serviceCollector.AddTransient<IAccountHandler, AccountHandler>();
        serviceCollector.AddTransient<ICardHandler, CardHandler>();
        serviceCollector.AddTransient<IDepositHandler, DepositHandler>();
        serviceCollector.AddTransient<ITransactionHandler, TransactionHandler>();
        serviceCollector.AddTransient<IUserHandler, UserHandler>();
        serviceCollector.AddTransient<IWithdrawalHandler, WithdrawalHandler>();

        serviceCollector.AddSingleton<ISmsService, SmsService>();

        // Abstractions project
        serviceCollector.AddSingleton<IMainHandler, MainHandler>();
        serviceCollector.AddSingleton<IRepository, InMemoryRepository>();

        // Repository project
        serviceCollector.AddSingleton<IEntityRepository<Account>, AccountInMemoryRepository>();
        serviceCollector.AddSingleton<IEntityRepository<User>, UserInMemoryRepository>();
        serviceCollector.AddSingleton<IEntityRepository<Card>, CardInMemoryRepository>();
        serviceCollector.AddSingleton<IEntityRepository<Transaction>, TransactionInMemoryRepository>();
        serviceCollector.AddSingleton<IEntityRepository<Deposit>, DepositInMemoryRepository>();
        serviceCollector.AddSingleton<IEntityRepository<Withdrawal>, WithdrawalInMemoryRepository>();
        serviceCollector.AddSingleton<IEntityRepository<DynamicPassword>, DynamicPasswordInMemoryRepository>();

        serviceCollector.AddSingleton<IDaoToEntity<AccountDao,Account>, DaoToAccount>();
        serviceCollector.AddSingleton<IDaoToEntity<UserDao,User>, DaoToUser>();
        serviceCollector.AddSingleton<IDaoToEntity<CardDao, Card>, DaoToCard>();
        serviceCollector.AddSingleton<IDaoToEntity<TransactionDao, Transaction>, DaoToTransaction>();
        serviceCollector.AddSingleton<IDaoToEntity<DepositDao, Deposit>, DaoToDeposit>();
        serviceCollector.AddSingleton<IDaoToEntity<WithdrawalDao, Withdrawal>, DaoToWithdrawal>();
        serviceCollector.AddSingleton<IDaoToEntity<DynamicPasswordDao, DynamicPassword>, DaoToDynamicPassword>();

        serviceCollector.AddSingleton<IEntityUpdateFromDao<AccountDao,Account>, AccountUpdater>();
        serviceCollector.AddSingleton<IEntityUpdateFromDao<UserDao,User>, UserUpdater>();
        serviceCollector.AddSingleton<IEntityUpdateFromDao<CardDao, Card>, CardUpdater>();
        serviceCollector.AddSingleton<IEntityUpdateFromDao<TransactionDao, Transaction>, TransactionUpdater>();
        serviceCollector.AddSingleton<IEntityUpdateFromDao<DepositDao, Deposit>, DepositUpdater>();
        serviceCollector.AddSingleton<IEntityUpdateFromDao<WithdrawalDao, Withdrawal>, WithdrawalUpdater>();
        serviceCollector.AddSingleton<IEntityUpdateFromDao<DynamicPasswordDao, DynamicPassword>, DynamicPasswordUpdater>();

        serviceCollector.AddSingleton<IEntityToDao<Account, AccountDao>, AccountToDao>();
        serviceCollector.AddSingleton<IEntityToDao<User, UserDao>, UserToDao>();
        serviceCollector.AddSingleton<IEntityToDao<Card, CardDao>, CardToDao>();
        serviceCollector.AddSingleton<IEntityToDao<Transaction, TransactionDao>, TransactionToDao>();
        serviceCollector.AddSingleton<IEntityToDao<Deposit, DepositDao>, DepositToDao>();
        serviceCollector.AddSingleton<IEntityToDao<Withdrawal, WithdrawalDao>, WithdrawalToDao>();
        serviceCollector.AddSingleton<IEntityToDao<DynamicPassword, DynamicPasswordDao>, DynamicPasswordToDao>();


        return serviceCollector.BuildServiceProvider();
    }
}
