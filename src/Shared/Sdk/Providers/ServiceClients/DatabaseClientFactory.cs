using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Shared.SDK.Clients
{
    public class DatabaseClientFactory : ClientFactory
    {
        public DatabaseClientFactory(IHttpClientFactory factory) : base(factory)
        {
            this.secretServiceUri = new Uri(this.secretServiceUri.AbsoluteUri + "Database");
        }

        public async Task<CosmosClient> GetClient(string connectionString)
        {
            string dbConnectionString = await this.GetConnectionStringForClient();

            return new CosmosClient(connectionString);
        }

    }
}