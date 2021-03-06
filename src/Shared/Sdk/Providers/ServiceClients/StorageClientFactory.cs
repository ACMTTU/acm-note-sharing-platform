using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public class StorageClientFactory : ClientFactory<CloudBlobClient>
    {
        public StorageClientFactory(IHttpClientFactory factory) : base(factory)
        {
            this.secretServiceUri = new Uri(this.secretServiceUri.AbsoluteUri + "BlobStorage");
        }
        public override async Task<CloudBlobClient> GetClient(string connectionString)
        {
            string storageConnectionString = await this.GetConnectionStringForClient();
            CloudStorageAccount storageAccount;
            if (!CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                throw new Exception($"Could not parse storage connection string: {storageConnectionString}");
            }

            return storageAccount.CreateCloudBlobClient();
        }

    }
}