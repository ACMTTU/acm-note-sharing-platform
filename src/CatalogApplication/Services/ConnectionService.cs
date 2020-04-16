using ACMTTU.NoteSharing.Shared.SDK.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace ACMTTU.NoteSharing.Platform.CatalogApplication.Services
{
    public class ConnectionService : PlatformBaseService
    {

        public Container tagContainer { get; private set; }
        public Container ratingContainer { get; private set; }

        public ConnectionService(IHttpClientFactory clientFactory) : base(clientFactory) { }

        public override Task Setup()
        {
            this._setUpDatabase().Wait();

            return Task.CompletedTask;
        }

        private async Task _setUpDatabase()
        {
            var databaseReference = await this.dbClient.CreateDatabaseIfNotExistsAsync("Catalog");

            // You can make multiple containers per database so have fun
            var tagContainerReference = await databaseReference.Database.CreateContainerIfNotExistsAsync("Tags", "/noteId");
            var ratingContainerReference = await databaseReference.Database.CreateContainerIfNotExistsAsync("Ratings", "/noteId");

            this.tagContainer = tagContainerReference.Container;
            this.ratingContainer = ratingContainerReference.Container;
        }
    }
}