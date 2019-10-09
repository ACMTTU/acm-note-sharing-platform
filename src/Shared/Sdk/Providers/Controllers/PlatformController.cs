using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMTTU.NoteSharing.Shared.SDK.Clients;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;


namespace ACMTTU.NoteSharing.Platform.Sdk.Controllers
{
    public abstract class PlatformBaseController : ControllerBase
    {
        protected readonly DatabaseClientFactory dbClientFactory;
        protected readonly StorageClientFactory storageClientFactory;
        protected string dbConnectionString;
        protected string storageConnectionString;

        public PlatformBaseController(IHttpClientFactory factory)
        {
            this.dbClientFactory = new DatabaseClientFactory(factory);
            this.storageClientFactory = new StorageClientFactory(factory);
            this.PlatformBaseControllerAsync().Wait();
        }

        private async Task PlatformBaseControllerAsync()
        {
            this.dbConnectionString = await this.dbClientFactory.GetConnectionStringForClient();
            this.storageConnectionString = await this.storageClientFactory.GetConnectionStringForClient();
        }

        protected async Task<CosmosClient> GetDatatbaseClient()
        {
            return await this.dbClientFactory.GetClient(this.dbConnectionString);
        }

        protected async Task<CloudBlobClient> GetStorageClient()
        {
            return await this.storageClientFactory.GetClient(this.storageConnectionString);
        }
    }
}