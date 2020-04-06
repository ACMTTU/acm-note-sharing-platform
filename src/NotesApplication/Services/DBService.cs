using ACMTTU.NoteSharing.Shared.SDK.Services;
using Microsoft.Azure.Cosmos;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using ACMTTU.NoteSharing.Shared.SDK.Controllers;
using ACMTTU.NoteSharing.Platform.NotesApplication.Services;
using NotesApplication;

namespace ACMTTU.NoteSharing.Platform.NotesApplication.Services
{
    public class DBService : PlatformBaseService
    {
        private Container _container;
        private PartitionKey _partKey = new PartitionKey("notes");

        public DBService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async override Task Setup()
        {
            var client = await this.dbClient.CreateDatabaseIfNotExistsAsync("NoteDatabase");
            ContainerResponse containerResp = await client.Database.CreateContainerIfNotExistsAsync("NoteContainer", "/");

            _container = containerResp.Container;
        }

        /// <summary>
        /// Creates item of type T in the database
        /// </summary>
        /// <param name="undatabasedObject">The item before it is databased, really a JSON object</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns>The new databased item</returns>
        public async Task<T> CreateItem<T>(T undatabasedObject)
        {

            T databasedObject = await _container.CreateItemAsync<T>(undatabasedObject);
            return databasedObject;

        }

        /// <summary>
        /// Returns item of type T from this database container
        /// </summary>
        /// <param name="id">The id of the item being grabbed</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns>The item grabbed from the database</returns>
        public async Task<T> ReadItem<T>(string id)
        {

            T item = await _container.ReadItemAsync<T>(id, _partKey);
            return item;

        }

        /// <summary>
        /// Update an item of type T stored in the database
        /// </summary>
        /// <param name="update">The new version of the item</param>
        /// <param name="id">The id of the item to be updated</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns></returns>
        public async void ReplaceItem<T>(T update, string id)
        {

            await _container.ReplaceItemAsync<T>(update, id);

        }

        /// <summary>
        /// Deletes an item of type T from the database
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns></returns>
        public async void DeleteItem<T>(string id)
        {

            await _container.DeleteItemAsync<T>(id, _partKey);

        }

    }
}
