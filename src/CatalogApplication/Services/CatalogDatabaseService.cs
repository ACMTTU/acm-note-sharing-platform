using ACMTTU.NoteSharing.Shared.SDK.Clients;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;
using System.Net.Http;

namespace CatalogApplication.Services
{
    public class CatalogDatabaseService : ConnectionService
    {
        public CatalogDatabaseService(IHttpClientFactory clientFactory) : base(clientFactory)
        { }
    }
}