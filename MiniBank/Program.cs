using InMemoryDataBase.Attributes;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MiniBank;

internal class Program
{
    private static void Main(string[] args)
    {
        var database = ServiceCollection.ServiceProvider.GetRequiredService<IShafaghDB>();
        
        var user = new user
        {
            Id = "0",
            Name = "E0",
            LastName = "1",
        };
        
        var user2 = new user
        {
            Id = "1",
            Name = "E0",
            LastName = "1",
        };
        
        var product = new Product
        {
            Id = "1",
            UserRef1 = "0",
            Name = "P0",
        };
        
        var product2 = new Product
        {
            Id = "2",
            UserRef2 = "0",
            Name = "P0",
        };
        
        var product3 = new Product
        {
            Id = "3",
            Name = "P0",
        };
        
        var product4 = new Product
        {
            Id = "4",
            Name = "P0",
        };
        
        database.Insert(user);
        database.Insert(user2);
        database.Insert(product);
        database.Insert(product2);
        database.Insert(product3);
        database.Insert(product4);
        
        database.Delete<Product>(product.Id);
        database.Delete<Product>(product2.Id);
        database.Delete<user>(user.Id);
        database.Delete<Product>(product3.Id);
        var prod = database.FetchById<Product>(product4.Id);
        Console.WriteLine(prod.Id);
        Console.WriteLine(prod.Name);
        Console.WriteLine(prod.UserRef1 == null);
        Console.WriteLine(prod.UserRef2 == null);
        prod.UserRef1 = user2.Id;
        database.Update(prod);
        var prod2 = database.FetchById<Product>(product4.Id);
        prod2.UserRef1 = null;
        database.Update(prod2);
        database.Delete<user>(user2.Id);
        database.Delete<Product>(product4.Id);
    }
    
    public class Product : IVersionable
    {
        [PrimaryKey]
        public string Id { get; set; }
        
        [ForeignKey(typeof(user))]
        [Nullable]
        public string UserRef1 { get; set; }
        
        [ForeignKey(typeof(user))]
        [Nullable]
        public string UserRef2 { get; set; }
        
        [Nullable]
        public string? Name { get; set; }
        
        int IVersionable.Version { get; set; }
    }
    
    public class user : IVersionable
    {
        [PrimaryKey]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        int IVersionable.Version { get; set; }
    }
}
