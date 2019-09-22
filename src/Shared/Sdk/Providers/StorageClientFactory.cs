using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public class StorageClientFactory : ClientFactory<CloudBlobClient>
    {
        public async override Task<CloudBlobClient> GetClientAsync()
        {
            string storageConnectionString;

            using(HttpClient client = new HttpClient())
            {
                // Make the API call to the Secrets Service
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri($"http://secretsservice.secrets.svc.cluster.local/api/connection/{Enum.GetName(typeof(ClientOptions), ClientOptions.BlobStorage)}");
                request.Method = HttpMethod.Get;
                HttpResponseMessage response = await client.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                EnvironmentVariablePayload payload = JsonConvert.DeserializeObject<EnvironmentVariablePayload>(content);

                // Determine which connection string to use
                storageConnectionString = this.DetermineCorrectConnectionString(payload);
            }

            // Get the storage account
            CloudStorageAccount storageAccount;
            if (!CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                throw new Exception($"Could not parse connection string: {storageConnectionString}");
            }

            // Create the client for the storage account
            return storageAccount.CreateCloudBlobClient();
        }
    }
}