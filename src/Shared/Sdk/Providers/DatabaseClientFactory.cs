using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.DataContracts;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public class DatabaseClientFactory<T> : ClientFactory<CosmosClient>
    {
        public async override Task<CosmosClient> GetClientAsync()
        {
            string databaseConnectionString;

            using(HttpClient client = new HttpClient())
            {
                // Make the API call to the Secrets Service
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri($"http://secretsservice.secrets.svc.cluster.local/api/connection/{Enum.GetName(typeof(ClientOptions), ClientOptions.Database)}");
                request.Method = HttpMethod.Get;
                HttpResponseMessage response = await client.SendAsync(request);

                EnvironmentVariablePayload payload = JsonConvert.DeserializeObject<EnvironmentVariablePayload>(response.Content.ToString());

                // Determine which connection string to use
                databaseConnectionString = this.DetermineCorrectConnectionString(payload);
            }

            // Get and return the Cosmos Client
            return new CosmosClient(databaseConnectionString);
        }
    }
}