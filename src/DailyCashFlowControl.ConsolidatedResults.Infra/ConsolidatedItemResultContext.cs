﻿using DailyCashFlowControl.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCashFlowControl.ConsolidatedResults.Infra
{
    public interface IConsolidatedItemResultContext
    {
        IMongoCollection<ConsolidatedItemResultDbModel> ConsolidatedItems { get; }
    }

    public class ConsolidatedItemResultContext : IConsolidatedItemResultContext
    {
        public ConsolidatedItemResultContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["ConsolidatedItemResultDatabaseSettings:connectionString"]);
            var database = client.GetDatabase(configuration["ConsolidatedItemResultDatabaseSettings:databaseName"]);
            ConsolidatedItems = database.GetCollection<ConsolidatedItemResultDbModel>("ConsolidatedItemResultDatabaseSettings:collectionName");
        }

        public IMongoCollection<ConsolidatedItemResultDbModel> ConsolidatedItems { get; }
    }
}
