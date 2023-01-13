using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cosmos.Dao
{
    public static class CosmosDbServiceProvider
    {
        private static string DatabaseName = "Items";
        private const string ContainerName = "ToDo";
        private const string Account = "https://todoitems2022.documents.azure.com:443/";
        private const string Key = "JCrutlaf2h1zczzuNQSrHSoHroY31M44FJZtcqdz6MuhTp006lZkWyMcTfI9g3XSjiceKzdd8RWUACDbePWmQA==";


        private static ICosmosDbService cosmosDbService;
        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }
        public static async Task Init()
        {
            CosmosClient cosmosClient = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(cosmosClient, DatabaseName, ContainerName);
            DatabaseResponse databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);

            await databaseResponse.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }

    }
}