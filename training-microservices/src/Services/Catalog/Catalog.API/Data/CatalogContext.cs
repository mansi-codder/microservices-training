using Catalog.API.Config;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IOptions<MongoDBConfig> _mongoDBConfigOptions;
        private readonly IMongoDatabase _db;
        public CatalogContext(IConfiguration configuration, IOptions<MongoDBConfig> mongoDBConfigOptions)
               {
            _mongoDBConfigOptions = mongoDBConfigOptions;

            //var connectionString = "mongodb://root:example@mongo:27017";
            //var client = new MongoClient(connectionString);
            ////var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            //var db = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            //var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));


            var client = new MongoClient(_mongoDBConfigOptions.Value.ConnectionString);
            MongoDBConnectionString = _mongoDBConfigOptions.Value.ConnectionString;
            Console.WriteLine("ConnectionString-mongo" + _mongoDBConfigOptions.Value.ConnectionString);
            _db = client.GetDatabase(_mongoDBConfigOptions.Value.DatabaseName);


            Products = _db.GetCollection<Product>(_mongoDBConfigOptions.Value.CollectionName);

            Products.InsertOneAsync(new Product()
            {
                Id = "1",
                Name = "IPhone X",
                Summary = _mongoDBConfigOptions.Value.ConnectionString,
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            }) ;

                var exist = Products.Find(p => true).Any();
            CatalogContextSeed.SeedData(Products);
        }

        private bool changemethod(Product arg)
        {
            arg.Summary = _mongoDBConfigOptions.Value.ConnectionString;
            return true;
        }

        public IMongoCollection<Product> Products { get; }
        public  string MongoDBConnectionString { get; }
    }
}
