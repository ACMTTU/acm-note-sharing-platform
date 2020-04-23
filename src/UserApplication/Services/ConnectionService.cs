using ACMTTU.NoteSharing.Shared.SDK.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Platform.CatalogApplication.Services
{
    public class ConnectionService : PlatformBaseService
    {

        public Container userContainer { get; private set; }

        public ConnectionService(IHttpClientFactory clientFactory) : base(clientFactory) { }

        public override Task Setup()
        {
            this._setUpDatabase().Wait();

            return Task.CompletedTask;
        }

        private async Task _setUpDatabase()
        {
            var databaseReference = await this.dbClient.CreateDatabaseIfNotExistsAsync("UserApplication");

            // You can make multiple containers per database so have fun
            var userContainerReference = await databaseReference.Database.CreateContainerIfNotExistsAsync("Users", "/userId");

            this.userContainer = userContainerReference.Container;
        }
    }
}