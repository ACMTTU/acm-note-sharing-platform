using ACMTTU.NoteSharing.Shared.SDK.Clients;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;
using System.Net.Http;

namespace ACMTTU.NoteSharing.Shared.SDK.Services {
    public abstract class PlatformBaseService {
        protected CosmosClient dbClient;
        protected CloudBlobClient storageClient;

        public PlatformBaseService(IHttpClientFactory clientFactory) {
            this.PlatformBaseServiceAsync(clientFactory).Wait();
        }

        public async Task PlatformBaseServiceAsync(IHttpClientFactory clientFactory) {
            DatabaseClientFactory dbFactory = new DatabaseClientFactory(clientFactory);
            string dbConnectionString = await dbFactory.GetConnectionStringForClient();
            this.dbClient = await dbFactory.GetClient(dbConnectionString);

            StorageClientFactory storageFactory = new StorageClientFactory(clientFactory);
            string storageConnectionString = await storageFactory.GetConnectionStringForClient();
            this.storageClient = await storageFactory.GetClient(storageConnectionString);
            this.Setup().Wait();
        }

        /// <summary>
        /// This is where you will set up your database names and other stuff like that
        /// </summary>
        /// <returns></returns>
        public abstract Task Setup();
    }

    
}