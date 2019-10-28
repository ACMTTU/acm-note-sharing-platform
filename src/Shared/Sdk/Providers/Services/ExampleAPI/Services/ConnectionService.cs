using ACMTTU.NoteSharing.Shared.SDK.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;

namespace ExampleAPI.Services {
    public class ConnectionService: PlatformBaseService {

        public Container serviceDatabaseContainer { get; private set; }

        public CloudBlobContainer serviceStorageContainer {get; private set; }

        public ConnectionService(IHttpClientFactory clientFactory) : base(clientFactory) { }

        public override Task Setup() {
            this._setUpBlobStorage().Wait();
            this._setUpDatabase().Wait();

            return Task.CompletedTask;
        }

        private async Task _setUpDatabase() {
            var databaseReference = await this.dbClient.CreateDatabaseAsync("DatabaseNameGoesHere");

            // You can make multiple containers per database so have fun
            var containerReference = await databaseReference.Database.CreateContainerAsync("ContainerNameGoesHere", "/PartitionKeyPathGoesHere");
            this.serviceDatabaseContainer = containerReference.Container;
        }

        private async Task _setUpBlobStorage() {
            var storageReference = this.storageClient.GetContainerReference("servicestoragecontainer");
            await storageReference.CreateIfNotExistsAsync();

            this.serviceStorageContainer = storageReference;
        }
    }
}