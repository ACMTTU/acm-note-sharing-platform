using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACMTTU.NoteSharing.Shared.SDK.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage.Blob;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Services {
    public class ConnectionService : PlatformBaseService {
        public Container notesContainer { get; private set; }
        public CloudBlobContainer notesBlobContainer { get; private set; }
        public ConnectionService (IHttpClientFactory clientFactory) : base (clientFactory) { }

        /// <summary>
        /// Let's create a NotesApplication Database
        /// </summary>
        /// <returns></returns>
        public override async Task Setup () {
            DatabaseResponse dbRef = await this.dbClient.CreateDatabaseIfNotExistsAsync ("NotesApplication");
            ContainerResponse containerRef = await dbRef.Database.CreateContainerIfNotExistsAsync ("NotesMetadata", "/Name");

            this.notesBlobContainer = this.storageClient.GetContainerReference ("notepreviews");
            this.notesBlobContainer.CreateIfNotExistsAsync ().Wait ();

            this.notesContainer = containerRef.Container;
        }
    }
}