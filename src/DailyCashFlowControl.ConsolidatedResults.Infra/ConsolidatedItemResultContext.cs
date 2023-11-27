using DailyCashFlowControl.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DailyCashFlowControl.ConsolidatedResults.Infra
{
    public interface IConsolidatedItemResultContext
    {
        IMongoCollection<ConsolidatedItemResult> ConsolidatedItems { get; }
    }

    public class ConsolidatedItemResultContext : IConsolidatedItemResultContext
    {
        public ConsolidatedItemResultContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["ConsolidatedItemResultDatabaseSettings:connectionString"]);
            var database = client.GetDatabase(configuration["ConsolidatedItemResultDatabaseSettings:databaseName"]);
            ConsolidatedItems = database.GetCollection<ConsolidatedItemResult>("ConsolidatedItemResultDatabaseSettings:collectionName");

           
        }

        public IMongoCollection<ConsolidatedItemResult> ConsolidatedItems { get; }
    }
}
