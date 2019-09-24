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
    public class ClientFactory : IClientFactory
    {
        // Name of the Kubernetes Service associated with the Secrets Service
        private const string secretServiceName = "secretsservice";

        // Name of the Kubernetes namespace the Secrets Service is running on
        private const string secretServiceNamespace = "secrets";

        private readonly IHttpClientFactory factory;

        public ClientFactory(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Gets the Database Client for that namespace
        /// </summary>
        /// <returns></returns>
        public CosmosClient GetDatabaseClient(string connectionString)
        {
            return new CosmosClient(connectionString);
        }

        /// <summary>
        /// Gets the Storage Client for that namespace
        /// </summary>
        /// <returns></returns>
        public CloudBlobClient GetStorageClient(string connectionString)
        {
            // Get the storage account
            CloudStorageAccount storageAccount;
            if (!CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                throw new Exception($"Could not parse connection string: {connectionString}");
            }

            // Create the client for the storage account
            return storageAccount.CreateCloudBlobClient();
        }

        /// <summary>
        /// Calls the Secrets Service to grab the specified connection strings
        /// </summary>
        /// <param name="option">Your choice of whether you want the Database or the Storage Client</param>
        /// <returns></returns>
        public async Task<string> GetConnectionStringForClient(ClientOptions option)
        {
            string connectionString;

            HttpClient client = this.factory.CreateClient();
            // Make the API call to the Secrets Service
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri($"http://{secretServiceName}.{secretServiceNamespace}.svc.cluster.local/api/connection/{Enum.GetName(typeof(ClientOptions), option)}");
            request.Method = HttpMethod.Get;
            HttpResponseMessage response = await client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            EnvironmentVariablePayload payload = JsonConvert.DeserializeObject<EnvironmentVariablePayload>(content);

            // Determine which connection string to use
            connectionString = this.DetermineCorrectConnectionString(payload);

            return connectionString;
        }

        /// <summary>
        /// Checks which namespace the service is running in to determine the
        /// correct connection string to use
        /// </summary>
        /// <returns>The connection string for that namespaces state</returns>
        private string DetermineCorrectConnectionString(EnvironmentVariablePayload payload)
        {
            // Check the NAMESPACE env
            string currentNamespace = Environment.GetEnvironmentVariable("NAMESPACE");

            // Check the Namespace env
            if (String.IsNullOrEmpty(currentNamespace))
            {
                currentNamespace = Environment.GetEnvironmentVariable("Namespace");
            }

            // Check the namespace env
            if (String.IsNullOrEmpty(currentNamespace))
            {
                currentNamespace = Environment.GetEnvironmentVariable("namespace");
            }

            if (String.IsNullOrEmpty(currentNamespace))
            {
                throw new Exception("Could not find current namespace. Please inject namespace into running deployment");
            }

            if (currentNamespace.ToLower().Contains("prod"))
            {
                return payload.Production;
            }
            else if (currentNamespace.ToLower().Contains("staging"))
            {
                return payload.Staging;
            }
            else
            {
                return payload.Development;
            }
        }
    }
}