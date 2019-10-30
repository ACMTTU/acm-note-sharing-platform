using ACMTTU.NoteSharing.Shared.SDK.Services;
using Microsoft.Azure.Cosmos;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Services
{
    public class DBService : PlatformBaseService
    {
        public Container newContainer;
        public DBService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }
        public async override Task Setup()
        {
            var client = await this.dbClient.CreateDatabaseIfNotExistsAsync("testName");

            var newContainer = await client.Database.CreateContainerIfNotExistsAsync("weewoo", "/testpath");


        }
    }
}
