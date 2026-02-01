using ComputerStoreApplication.Account;
using ComputerStoreApplication.MongoModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    public class MongoConnection
    {
        private static readonly MongoClient _client = new MongoClient(MongoClientSettings.FromUrl(new MongoUrl(
            "mongodb+srv://oscardbuser:Oscar1234@cluster0.pb6h2cm.mongodb.net/")
    )
);
        //singelton create once and use many times over later, aka don't open a connection every times
        private static MongoClient GetClient() => _client;
       
        public static async Task CustomerLoginAttempt(string email, int customerId, bool success)
        {
            var client = GetClient();

            var db = client.GetDatabase("Logins");

            var collection = db.GetCollection<CustomerLoginAttempt>("CustomerLogins");

            var newAttempt = new CustomerLoginAttempt
            {
                Email = email,
                CustomerId = customerId,
                Success = success,
                AttemptTime = DateTime.UtcNow
            };
            await collection.InsertOneAsync(newAttempt);
        }
        public static async Task AdminLoginAttempt(string username, int adminId, bool success)
        {
            var client = GetClient();

            var db = client.GetDatabase("Logins");

            var collection = db.GetCollection<AdminLoginAttempt>("AdminLogins");
            var newAttempt = new AdminLoginAttempt
            {
                Username = username,
                AdminId = adminId,
                Success = success,
                AttemptTime = DateTime.UtcNow
            };

            await collection.InsertOneAsync(newAttempt);
        }
        public static async Task CustomerPurchase(int customerId, decimal price)
        {
            var client = GetClient();
            var db = client.GetDatabase("Purchases");

            var collection = db.GetCollection<CustomerPurchase>("Purchases");
            var newPurchase = new CustomerPurchase
            {
                CustomerId = customerId,
                Price = price,
                AttemptTime = DateTime.UtcNow
            };
            await collection.InsertOneAsync(newPurchase);
        }
        public static async Task CustomerRegistration(string email, int id)
        {
            var client = GetClient();
            var db = client.GetDatabase("CustomerAccounts");

            var collection = db.GetCollection<CustomerRegistration>("Accounts");

            var newAccount = new CustomerRegistration
            {
                Email = email,
                CustomerId = id,
                CreationDate = DateTime.UtcNow
            };
            await collection.InsertOneAsync(newAccount);
        }
        public static async Task AdminCreateProduct(string name, int adminId)
        {
            var client = GetClient();

            var db = client.GetDatabase("Products");
            var collection = db.GetCollection<AdminRegisterProduct>("NewlyRegisteredProducts");

            var newProduct = new AdminRegisterProduct
            {
                ProductName = name,
                AdminId = adminId,
                CreationDate = DateTime.UtcNow
            };
            await collection.InsertOneAsync(newProduct);
        }
        public static IMongoCollection<CustomerAccount> GetPersonCollection()
        {
            var client = GetClient();

            var database = client.GetDatabase("MyComputer");

            var people = database.GetCollection<CustomerAccount>("CollectionOfPeople");

            return people;
        }
    }
}
