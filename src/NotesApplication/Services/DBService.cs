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

    /// <summary>
    /// An abstract class for database services
    /// </summary>
    public interface IDBService
    {

        Task<T> CreateItem<T>(T undatabasedObject);
        Task<T> ReadItem<T>(string id);
        Task<ItemResponse<T>> ReplaceItem<T>(T update, string id);
        Task<ItemResponse<T>> DeleteItem<T>(string id);
        Task<FeedIterator<T>> GetItemQueryIterator<T>(QueryDefinition query);

    }

    /// <summary>
    /// An implementation of IDBService to interface with Azure Cosmos DB
    /// </summary>
    public class DBService : PlatformBaseService, IDBService
    {
        private Container _container;
        private PartitionKey _partKey = new PartitionKey("/notes");

        public DBService(IHttpClientFactory clientFactory) : base(clientFactory) { }

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

            return await _container.CreateItemAsync<T>(undatabasedObject, _partKey);

        }

        /// <summary>
        /// Returns item of type T from this database container
        /// </summary>
        /// <param name="id">The id of the item being grabbed</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns>The item grabbed from the database</returns>
        public async Task<T> ReadItem<T>(string id)
        {

            return await _container.ReadItemAsync<T>(id, _partKey);

        }

        /// <summary>
        /// Update an item of type T stored in the database
        /// </summary>
        /// <param name="update">The new version of the item</param>
        /// <param name="id">The id of the item to be updated</param>
        /// <typeparam name="T">The type of the item</typeparam>
        public async Task<ItemResponse<T>> ReplaceItem<T>(T update, string id)
        {

            return await _container.ReplaceItemAsync<T>(update, id, _partKey);

        }

        /// <summary>
        /// Deletes an item of type T from the database
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <typeparam name="T">The type of the item</typeparam>
        public async Task<ItemResponse<T>> DeleteItem<T>(string id)
        {

            return await _container.DeleteItemAsync<T>(id, _partKey);

        }

        /// <summary>
        /// Returns iterator of items given a database query
        /// </summary>
        /// <param name="query">The query definition</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns></returns>
        public async Task<FeedIterator<T>> GetItemQueryIterator<T>(QueryDefinition query)
        {

            // create query request options with our partition key
            QueryRequestOptions requestOptions = new QueryRequestOptions() { PartitionKey = _partKey };
            return _container.GetItemQueryIterator<T>(query, null, requestOptions);

        }

    }
}
