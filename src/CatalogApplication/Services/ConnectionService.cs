using ACMTTU.NoteSharing.Shared.SDK.Clients;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;
using System.Net.Http;

namespace CatalogApplication.Services
{
    public abstract class ConnectionService
    {
        protected CosmosClient dbClient;
        protected CloudBlobClient storageClient;

        public ConnectionService(IHttpClientFactory clientFactory)
        {
            this.ConnectionServiceAsync(clientFactory).Wait();
        }

        public async Task ConnectionServiceAsync(IHttpClientFactory clientFactory)
        {
            DatabaseClientFactory dbFactory = new DatabaseClientFactory(clientFactory);
            string dbConnectionString = await dbFactory.GetConnectionStringForClient();
            this.dbClient = await dbFactory.GetClient(dbConnectionString);

            StorageClientFactory storageFactory = new StorageClientFactory(clientFactory);
            string storageConnectionString = await storageFactory.GetConnectionStringForClient();
            this.storageClient = await storageFactory.GetClient(storageConnectionString);
            this.CreateAllTheStuff().Wait();
        }

        public async Task CreateAllTheStuff()
        {
            // This is where you probably want to create your database and your other containers or whatever
        }
    }
}